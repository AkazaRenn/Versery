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
using View.Interfaces;

namespace View.Controls.RichTextRenderer;

internal sealed partial class Emoji: Panel {
    private static readonly CanvasDevice canvasDevice = CanvasDevice.GetSharedDevice();
    private static readonly MemoryCache imageCache = new(nameof(Emoji));
    private static readonly CacheItemPolicy imageCachePolicy = new() {
        SlidingExpiration = TimeSpan.FromMinutes(30),
    };

    private SpriteVisual? visual;
    private CompositionSurfaceBrush? brush;
    private CompositionDrawingSurface? surface;
    private Window? window;

    private bool isLoaded = false;
    private int currentFrame = 0;
    private uint loop = 0;
    private uint maxLoop = 0;
    private readonly DispatcherQueueTimer timer;
    private CanvasBitmap[] gpuFrames = [];
    private IReadOnlyList<double> frameDelaysMs = [];

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
        isLoaded &&
        ((maxLoop == 0) || (loop < maxLoop));

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
        if (Source is not null && gpuFrames.Length == 0) {
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
        maxLoop = 0;

        foreach (var frame in gpuFrames) {
            frame.Dispose();
        }
        gpuFrames = [];
    }

    private async Task LoadImage() {
        Reset();

        if ((Source is null) || (surface is null)) {
            return;
        }

        var cacheKey = Source.AbsoluteUri;

        ImageData data;
        if (imageCache.Get(cacheKey) is ImageData cachedData) {
            data = cachedData;
        } else {
            using var image = await SixLabors.ImageSharp.Image.LoadAsync<Rgba32>(Source.LocalPath);
            data = await Task.Run(() => new ImageData(image));
            imageCache.Add(cacheKey, data, imageCachePolicy);
        }

        gpuFrames = data.CreateGpuFrames();
        frameDelaysMs = data.FrameDelaysMs;
        maxLoop = data.MaxLoop;
        surface.Resize(new Windows.Graphics.SizeInt32(data.Width, data.Height));
        Draw();
    }

    private void Draw() {
        if ((gpuFrames.Length == 0) || (surface is null)) {
            return;
        }

        using (var ds = CanvasComposition.CreateDrawingSession(surface)) {
            ds.Clear(Colors.Transparent);
            ds.DrawImage(gpuFrames[currentFrame]);
        }

        timer.Interval = TimeSpan.FromMilliseconds(frameDelaysMs[currentFrame]);
        currentFrame = (currentFrame + 1) % gpuFrames.Length;
        if ((currentFrame == 0) && (maxLoop > 0)) {
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

    private class ImageData {
        public uint MaxLoop { get; } // 0 = infinite
        public IReadOnlyList<double> FrameDelaysMs { get; }
        public IReadOnlyList<byte[]> Frames { get; }
        public int Width { get; }
        public int Height { get; }

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
                if (double.IsNaN(delays[i])) {
                    delays[i] = 20;
                } else if (delays[i] < 20) {
                    delays[i] = 20;
                }

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
            var frames = new CanvasBitmap[Frames.Count];

            try {
                for (int i = 0; i < Frames.Count; i++) {
                    frames[i] = CanvasBitmap.CreateFromBytes(
                        canvasDevice,
                        Frames[i],
                        Width,
                        Height,
                        Windows.Graphics.DirectX.DirectXPixelFormat.R8G8B8A8UIntNormalized
                    );
                }

                return frames;
            } catch {
                foreach (var frame in frames) {
                    frame?.Dispose();
                }

                throw;
            }
        }
    }
}
