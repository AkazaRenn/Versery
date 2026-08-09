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
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Collections.Concurrent;
using View.Interfaces;

namespace View.Controls.RichTextRenderer;

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
    private uint loop = 0;
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
        (frameData.Target.Count > 1) &&
        ((frameData.Target.MaxLoop == 0) || (loop < frameData.Target.MaxLoop));

    public Emoji() {
        InitializeComponent();

        timer = DispatcherQueue.GetForCurrentThread().CreateTimer();
        timer.Tick += Timer_Tick;
        timer.IsRepeating = false;
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
        canvasDevice.DeviceLost -= CanvasDevice_DeviceLost;
        canvasDevice.DeviceLost += CanvasDevice_DeviceLost;
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
        visual?.Size = new System.Numerics.Vector2((float)ActualWidth, (float)ActualHeight);
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
        loop = 0;

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
        if ((currentFrame == 0) && (frameData.Target.MaxLoop > 0)) {
            loop++;
        }

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
        public uint MaxLoop { get; init; }

        public void Dispose() {
            cache.TryRemove(key, out _);
            foreach (var frame in this) {
                frame?.Bitmap?.Dispose();
            }
        }
    }

    private static FrameDataCollection CreateGpuFrames(Uri source) {
        using var image = SixLabors.ImageSharp.Image.Load<Rgba32>(source.LocalPath);

        var frameCount = image.Frames.Count;
        var Width = image.Width;
        var Height = image.Height;
        uint MaxLoop = 0;
        var collection = new FrameDataCollection(frameCount, source) {
            Width = Width,
            Height = Height,
            MaxLoop = MaxLoop,
        };

        double[] frameDelaysMs;
        if (image.Metadata.TryGetGifMetadata(out var gifMeta)) {
            MaxLoop = gifMeta.RepeatCount;
            frameDelaysMs = [.. image.Frames.Select(x => x.Metadata.GetGifMetadata().FrameDelay * 10.0)]; // GIF delay is in 10ms units
        } else if (image.Metadata.TryGetWebpMetadata(out var webpMeta)) {
            MaxLoop = webpMeta.RepeatCount;
            frameDelaysMs = [.. image.Frames.Select(x => x.Metadata.GetWebpMetadata().FrameDelay)]; // WebP delay is already in ms
        } else if (image.Metadata.TryGetPngMetadata(out var pngMeta)) {
            MaxLoop = pngMeta.RepeatCount;
            frameDelaysMs = [.. image.Frames.Select(x => x.Metadata.GetPngMetadata().FrameDelay.ToDouble() * 1000.0)]; // PNG delay is in seconds
        } else {
            frameDelaysMs = new double[frameCount];
        }

        try {
            for (int i = 0; i < frameCount; i++) {
                // Enforce a minimum delay of 20ms to avoid excessively fast frames
                if (double.IsNaN(frameDelaysMs[i]) || (frameDelaysMs[i] < 20)) {
                    frameDelaysMs[i] = 20;
                }

                var frame = image.Frames[i];
                var pixels = new byte[frame.Width * frame.Height * 4];
                frame.CopyPixelDataTo(pixels);

                collection.Add(new(
                    CanvasBitmap.CreateFromBytes(
                        canvasDevice,
                        PremultiplyAlphaInPlace(pixels),
                        Width,
                        Height,
                        Windows.Graphics.DirectX.DirectXPixelFormat.R8G8B8A8UIntNormalized),
                    frameDelaysMs[i]));
            }
            return collection;
        } catch {
            foreach (var frame in collection) {
                frame?.Bitmap?.Dispose();
            }
            throw;
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
