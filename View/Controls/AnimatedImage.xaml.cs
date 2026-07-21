using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Composition;
using Microsoft.Graphics.DirectX;
using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Hosting;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.Runtime.Caching;
using Utilities;

namespace View.Controls;
internal sealed partial class AnimatedImage: UserControl, IRecipient<Messages.WindowActivated>, IRecipient<Messages.WindowDeactivated> {
    private static readonly CanvasDevice canvasDevice = CanvasDevice.GetSharedDevice();
    private static readonly MemoryCache imageCache = new(nameof(AnimatedImage));
    private static readonly CacheItemPolicy imageCachePolicy = new() {
        SlidingExpiration = TimeSpan.FromMinutes(30),
    };

    private readonly CompositionGraphicsDevice graphicsDevice = Utilities.Services.Get<CompositionGraphicsDevice>();
    private readonly SpriteVisual visual;
    private readonly CompositionSurfaceBrush brush;
    private readonly CompositionDrawingSurface surface;

    private int currentFrame = 0;
    private uint loop = 0;
    private readonly DispatcherQueueTimer timer;
    private ImageData? data;
    private CanvasBitmap[] gpuFrames = [];

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
        (gpuFrames.Length > 1) &&
        IsLoaded &&
        data is not null &&
        data.IsAnimated &&
        ((data.MaxLoop == 0) || (loop < data.MaxLoop));

    public AnimatedImage() {
        InitializeComponent();

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
        ElementCompositionPreview.SetElementChildVisual(this, visual);

        timer = DispatcherQueue.GetForCurrentThread().CreateTimer();
        timer.Tick += Timer_Tick;
        timer.IsRepeating = false;
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

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
        canvasDevice.DeviceLost += CanvasDevice_DeviceLost;
        StrongReferenceMessenger.Default.RegisterAll(this);
        if (Source is not null && gpuFrames.Length == 0) {
            _ = LoadImage();
        } else {
            TryPlay();
        }

    }

    private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
        canvasDevice.DeviceLost -= CanvasDevice_DeviceLost;
        StrongReferenceMessenger.Default.UnregisterAll(this);
        Reset();
    }

    private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e) {
        visual.Size = new System.Numerics.Vector2((float)ActualWidth, (float)ActualHeight);
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

        foreach (var frame in gpuFrames) {
            frame.Dispose();
        }
        gpuFrames = [];
    }

    private async Task LoadImage() {
        Reset();

        if (Source is null) {
            data = null;
            return;
        }

        var cacheKey = Source.AbsoluteUri;

        if (imageCache.Get(cacheKey) is ImageData cachedData) {
            data = cachedData;
        } else {
            using var image = await SixLabors.ImageSharp.Image.LoadAsync<Rgba32>(Source.LocalPath);
            data = new ImageData(image);
            imageCache.Add(cacheKey, data, imageCachePolicy);
        }

        gpuFrames = data.CreateGpuFrames();
        surface.Resize(new Windows.Graphics.SizeInt32(data.Width, data.Height));
        Draw();
    }

    private void Draw() {
        if ((data is null) ||
            (gpuFrames.Length == 0)) {
            return;
        }

        if ((currentFrame < 0) ||
            (currentFrame >= data.FrameCount)) {
            currentFrame = 0;
        }

        using (var ds = CanvasComposition.CreateDrawingSession(surface)) {
            ds.Clear(Colors.Transparent);
            ds.DrawImage(gpuFrames[currentFrame]);
        }

        if (ShouldPlay) {
            timer.Interval = TimeSpan.FromMilliseconds(data.FrameDelaysMs[currentFrame]);

            currentFrame = (currentFrame + 1) % data.FrameCount;
            if (currentFrame == 0 && data.MaxLoop > 0) {
                loop++;
            }

            TryPlay();
        }
    }

    void IRecipient<Messages.WindowActivated>.Receive(Messages.WindowActivated message) {
        TryPlay();
    }

    void IRecipient<Messages.WindowDeactivated>.Receive(Messages.WindowDeactivated message) {
        Stop();
    }

    private class ImageData {
        public uint MaxLoop { get; } // 0 = infinite
        public IReadOnlyList<double> FrameDelaysMs { get; }
        public IReadOnlyList<byte[]> Frames { get; }
        public int Width { get; }
        public int Height { get; }
        public int FrameCount => Frames.Count;
        public bool IsAnimated => FrameCount > 1;

        public ImageData(Image<Rgba32> image) {
            Width = image.Width;
            Height = image.Height;

            var frameCount = image.Frames.Count;
            var delays = new double[frameCount];
            var frames = new byte[frameCount][];

            if (image.Metadata.TryGetGifMetadata(out var gifMeta)) {
                MaxLoop = gifMeta.RepeatCount;
                for (int i = 0; i < frameCount; i++) {
                    var frameMeta = image.Frames[i].Metadata.GetGifMetadata();
                    delays[i] = frameMeta.FrameDelay * 10; // GIF delay is in 10ms units
                }
            } else if (image.Metadata.TryGetWebpMetadata(out var webpMeta)) {
                MaxLoop = webpMeta.RepeatCount;
                for (int i = 0; i < frameCount; i++) {
                    var frameMeta = image.Frames[i].Metadata.GetWebpMetadata();
                    delays[i] = frameMeta.FrameDelay; // WebP delay is already in ms
                }
            } else if (image.Metadata.TryGetPngMetadata(out var pngMeta)) {
                MaxLoop = pngMeta.RepeatCount;
                for (int i = 0; i < frameCount; i++) {
                    var frameMeta = image.Frames[i].Metadata.GetPngMetadata();
                    delays[i] = frameMeta.FrameDelay.ToDouble() * 1000.0; // PNG delay is in seconds
                }
            } else {
                MaxLoop = 0;
            }

            for (int i = 0; i < frameCount; i++) {
                // Enforce a minimum delay of 20ms to avoid excessively fast frames
                delays[i] = Math.Max(delays[i], 20);

                var frame = image.Frames[i];
                var pixels = new byte[frame.Width * frame.Height * 4];
                frame.CopyPixelDataTo(pixels);
                PremultiplyAlphaInPlace(pixels);
                frames[i] = pixels;
            }

            FrameDelaysMs = delays;
            Frames = frames;
        }

        private static void PremultiplyAlphaInPlace(byte[] pixels) {
            for (int i = 0; i < pixels.Length; i += 4) {
                var a = pixels[i + 3];
                pixels[i + 0] = (byte)((pixels[i + 0] * a + 127) / 255); // R
                pixels[i + 1] = (byte)((pixels[i + 1] * a + 127) / 255); // G
                pixels[i + 2] = (byte)((pixels[i + 2] * a + 127) / 255); // B
            }
        }

        internal CanvasBitmap[] CreateGpuFrames() {
            var frames = new List<CanvasBitmap>(FrameCount);

            try {
                foreach (var frameData in Frames) {
                    frames.Add(CanvasBitmap.CreateFromBytes(
                        canvasDevice,
                        frameData,
                        Width,
                        Height,
                        Windows.Graphics.DirectX.DirectXPixelFormat.R8G8B8A8UIntNormalized
                    ));
                }

                return [.. frames];
            } catch {
                foreach (var frame in frames) {
                    frame.Dispose();
                }

                throw;
            }
        }
    }
}
