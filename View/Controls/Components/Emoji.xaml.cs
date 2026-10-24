using FFmpeg.AutoGen.Abstractions;
using FFmpeg.AutoGen.Bindings.DynamicallyLinked;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Composition;
using Microsoft.Graphics.DirectX;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using Nito.Disposables;
using System.Collections.Concurrent;
using View.Interfaces;

namespace View.Controls.Components;

internal sealed partial class Emoji: Panel {
    private static readonly CanvasDevice canvasDevice = CanvasDevice.GetSharedDevice();
    private static readonly ConcurrentDictionary<Uri, Task<IWeakReferenceCountedDisposable<FrameDataCollection>>> cache = [];

    private SpriteVisual? visual;
    private CompositionSurfaceBrush? brush;
    private CompositionDrawingSurface? surface;
    private Window? window;

    private readonly DispatcherQueueTimer timer;
    private bool isLoaded = false;
    private int currentFrame = 0;
    private IReferenceCountedDisposable<FrameDataCollection>? frameData = null;

    public Uri? Source {
        get;
        set {
            if (field != value) {
                field = value;
                _ = LoadImage();
            }
        }
    }

    public bool ShouldPlay =>
        isLoaded &&
        (frameData?.Target is not null) &&
        (frameData.Target.Count > 1);

    static Emoji() {
        DynamicallyLinkedBindings.Initialize();
    }

    public Emoji() {
        InitializeComponent();

        timer = DispatcherQueue.GetForCurrentThread().CreateTimer();
        timer.Tick += Timer_Tick;
        timer.IsRepeating = false;

        canvasDevice.DeviceLost += CanvasDevice_DeviceLost;
    }

    private void InitComposition(ICompositionGraphicsDeviceProvider provider) {
        var graphicsDevice = provider.CompositionGraphicsDevice;
        surface = graphicsDevice.CreateDrawingSurface(
            new Windows.Foundation.Size(0, 0),
            DirectXPixelFormat.R8G8B8A8UIntNormalized,
            DirectXAlphaMode.Premultiplied);

        var compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

        brush = compositor.CreateSurfaceBrush();
        brush.Stretch = CompositionStretch.Uniform;
        brush.Surface = surface;

        visual = compositor.CreateSpriteVisual();
        visual.Brush = brush;
        visual.Size = new System.Numerics.Vector2((float)ActualWidth, (float)ActualHeight);
        ElementCompositionPreview.SetElementChildVisual(this, visual);
    }

    private void CanvasDevice_DeviceLost(CanvasDevice sender, object args) {
        if (DispatcherQueue.HasThreadAccess) {
            _ = LoadImage();
        } else {
            DispatcherQueue.TryEnqueue(() => _ = LoadImage());
        }
    }

    private void Timer_Tick(object? sender, object e) {
        Draw();
    }

    private void Panel_Loaded(object sender, RoutedEventArgs e) {
        if (isLoaded) {
            return;
        }
        isLoaded = true;
        if ((window is null) && (Application.Current is IWindowHelper windowHelper)) {
            if (windowHelper.TryGetWindow(this, out window) && (window is ICompositionGraphicsDeviceProvider provider)) {
                InitComposition(provider);
            }
        }
        window?.Activated -= Window_Activated;
        window?.Activated += Window_Activated;
        if ((Source is not null) &&
            ((frameData?.Target is null) || (frameData.Target.Count == 0))) {
            _ = LoadImage();
        } else {
            TryPlay();
        }

    }

    private void Panel_Unloaded(object sender, RoutedEventArgs e) {
        // Workaround, Unloaded may be fired on resizing when inside a InlineUIContainer
        // https://github.com/microsoft/microsoft-ui-xaml/issues/5976#issuecomment-2174129763
        if (IsLoaded) {
            return;
        }
        if (!isLoaded) {
            return;
        }
        isLoaded = false;
        canvasDevice.DeviceLost -= CanvasDevice_DeviceLost;
        window?.Activated -= Window_Activated;
        Reset();
    }

    private void Panel_SizeChanged(object sender, SizeChangedEventArgs e) {
        visual?.Size = new System.Numerics.Vector2((float)e.NewSize.Width, (float)e.NewSize.Height);
    }

    private void TryPlay() {
        if (ShouldPlay) {
            timer.Start();
        } else {
            timer.Stop();
        }
    }

    private void Stop() {
        timer.Stop();
    }

    private void Reset() {
        Stop();
        currentFrame = 0;

        frameData?.Dispose();
        frameData = null;
    }

    private async Task LoadImage() {
        Reset();

        if ((Source is null) || (surface is null)) {
            return;
        }

        var loadTask = cache.GetOrAdd(Source, _ =>
            Task.Run(async () => {
                frameData = ReferenceCountedDisposable.Create(await Task.Run(() => CreateGpuFrames(Source)));
                return frameData.AddWeakReference();
            })
        );

        try {
            var cachedData = await loadTask;
            frameData ??= cachedData.TryAddReference();
        } catch {
            cache.TryRemove(Source, out _);
            throw;
        }

        if (frameData?.Target is not null) {
            surface.Resize(new Windows.Graphics.SizeInt32(frameData.Target.Width, frameData.Target.Height));
            Draw();
        }
    }

    private void Draw() {
        if ((frameData is null) ||
            (frameData.IsDisposeStarted == true) ||
            (frameData.Target is null) ||
            (frameData.Target.Count == 0) ||
            (surface is null)) {
            return;
        }

        var data = frameData.Target.ElementAt(currentFrame);
        using (var ds = CanvasComposition.CreateDrawingSession(surface)) {
            ds.Clear(Colors.Transparent);
            ds.DrawImage(data.Bitmap);
        }

        timer.Interval = TimeSpan.FromMilliseconds(data.DelayMs);
        currentFrame = (currentFrame + 1) % frameData.Target.Count;

        TryPlay();
    }

    private void Window_Activated(object sender, WindowActivatedEventArgs args) {
        switch (args.WindowActivationState) {
        case WindowActivationState.Deactivated:
            Stop();
            break;
        default:
            TryPlay();
            break;
        }
    }

    private partial record FrameData(CanvasBitmap Bitmap, double DelayMs);
    private partial class FrameDataCollection(int capacity, Uri key): List<FrameData>(capacity), IDisposable {
        public int Width { get; init; }
        public int Height { get; init; }

        public void Dispose() {
            cache.TryRemove(key, out _);
            var copy = ToArray();
            Clear();
            Task.Run(() => {
                foreach (var frame in copy) {
                    frame?.Bitmap?.Dispose();
                }
            });
        }
    }

    private static unsafe FrameDataCollection CreateGpuFrames(Uri source) {
        var path = source.LocalPath;

        AVFormatContext* format = null;
        AVCodecContext* codecContext = null;
        var sws = ffmpeg.sws_alloc_context();
        var packet = ffmpeg.av_packet_alloc();
        var frame = ffmpeg.av_frame_alloc();
        var rgba = ffmpeg.av_frame_alloc();
        FrameDataCollection? collection = null;

        try {
            if ((ffmpeg.avformat_open_input(&format, path, null, null) < 0) ||
                (ffmpeg.avformat_find_stream_info(format, null) < 0)) {
                return new(0, source);
            }

            AVCodec* codec = null;
            var streamIndex = ffmpeg.av_find_best_stream(format, AVMediaType.AVMEDIA_TYPE_VIDEO, -1, -1, &codec, 0);
            if (streamIndex < 0) {
                return new(0, source);
            }
            var stream = format->streams[streamIndex];
            codecContext = ffmpeg.avcodec_alloc_context3(codec);
            if ((ffmpeg.avcodec_parameters_to_context(codecContext, stream->codecpar) < 0) ||
                (ffmpeg.avcodec_open2(codecContext, codec, null) < 0)) {
                return new(0, source);
            }

            var width = codecContext->width;
            var height = codecContext->height;
            var timeBase = ffmpeg.av_q2d(stream->time_base);
            collection = new FrameDataCollection(4, source) {
                Width = width,
                Height = height,
            };

            var pixels = new byte[width * height * 4];
            var flushing = false;
            while (true) {
                if (!flushing) {
                    if (ffmpeg.av_read_frame(format, packet) < 0) {
                        flushing = true;
                        ffmpeg.avcodec_send_packet(codecContext, null);
                    } else {
                        if (packet->stream_index == streamIndex) {
                            ffmpeg.avcodec_send_packet(codecContext, packet);
                        }
                        ffmpeg.av_packet_unref(packet);
                    }
                }

                while (ffmpeg.avcodec_receive_frame(codecContext, frame) == 0) {
                    rgba->format = (int)AVPixelFormat.AV_PIX_FMT_RGBA;
                    rgba->width = width;
                    rgba->height = height;
                    if (ffmpeg.sws_scale_frame(sws, rgba, frame) < 0) {
                        ffmpeg.av_frame_unref(rgba);
                        ffmpeg.av_frame_unref(frame);
                        continue;
                    }

                    var stride = rgba->linesize[0];
                    var rowBytes = width * 4;
                    for (var y = 0; y < height; y++) {
                        new ReadOnlySpan<byte>(rgba->data[0] + ((long)y * stride), rowBytes).CopyTo(pixels.AsSpan(y * rowBytes));
                    }

                    // Enforce a minimum delay of 100ms to avoid excessively fast frames
                    var delayMs = frame->duration * timeBase * 1000.0;
                    if (double.IsNaN(delayMs) || (delayMs < 100)) {
                        delayMs = 100;
                    }

                    collection.Add(new(
                        CanvasBitmap.CreateFromBytes(
                            canvasDevice,
                            PremultiplyAlphaInPlace(pixels),
                            width,
                            height,
                            Windows.Graphics.DirectX.DirectXPixelFormat.R8G8B8A8UIntNormalized),
                        delayMs));

                    ffmpeg.av_frame_unref(rgba);
                    ffmpeg.av_frame_unref(frame);
                }

                if (flushing) {
                    break;
                }
            }
            return collection;
        } catch {
            if (collection is not null) {
                foreach (var data in collection) {
                    data?.Bitmap?.Dispose();
                }
            }
            throw;
        } finally {
            ffmpeg.av_frame_free(&rgba);
            ffmpeg.av_frame_free(&frame);
            ffmpeg.av_packet_free(&packet);
            ffmpeg.sws_freeContext(sws);
            ffmpeg.avcodec_free_context(&codecContext);
            ffmpeg.avformat_close_input(&format);
        }
    }

    private static byte[] PremultiplyAlphaInPlace(byte[] pixels) {
        for (int i = 0; i < pixels.Length; i += 4) {
            var a = pixels[i + 3];
            pixels[i + 0] = (byte)((pixels[i + 0] * a + 127) / 255); // R
            pixels[i + 1] = (byte)((pixels[i + 1] * a + 127) / 255); // G
            pixels[i + 2] = (byte)((pixels[i + 2] * a + 127) / 255); // B
        }
        return pixels;
    }
}
