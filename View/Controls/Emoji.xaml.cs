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

namespace View.Controls;

internal sealed partial class Emoji: UserControl, IDisposable,
    IRecipient<Messages.WindowActivated>, IRecipient<Messages.WindowDeactivated> {
    private readonly Compositor compositor;
    private readonly SpriteVisual visual;
    private readonly CompositionSurfaceBrush brush;
    private readonly CanvasDevice canvasDevice = CanvasDevice.GetSharedDevice();
    private CompositionGraphicsDevice? graphicsDevice;
    private CompositionDrawingSurface? surface;

    private int currentFrame = 0;
    private uint loop = 0;
    private readonly DispatcherQueueTimer timer;
    private ImageData? data;

    private Uri? source;
    public Uri? Source {
        get => source;
        set {
            if (source == value) {
                return;
            }
            source = value;
            _ = LoadImage();
        }
    }

    public bool ShouldPlay => (data is not null) && (data.IsAnimated) && ((data.MaxLoop == 0) || (loop < data.MaxLoop));

    public Emoji() {
        InitializeComponent();

        compositor = ElementCompositionPreview.GetElementVisual(this).Compositor;

        brush = compositor.CreateSurfaceBrush();
        brush.Stretch = CompositionStretch.Uniform;

        visual = compositor.CreateSpriteVisual();
        visual.Brush = brush;
        ElementCompositionPreview.SetElementChildVisual(this, visual);

        timer = DispatcherQueue.GetForCurrentThread().CreateTimer();
        timer.Tick += Timer_Tick;
        timer.IsRepeating = false;

        canvasDevice.DeviceLost += CanvasDevice_DeviceLost;

        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    private void CanvasDevice_DeviceLost(CanvasDevice sender, object args) {
        _ = LoadImage();
    }

    private void Timer_Tick(object? sender, object e) {
        Draw();
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
        _ = LoadImage();
    }

    private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
        Dispose();
    }

    private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e) {
        _ = LoadImage();
    }

    private async Task LoadImage() {
        if (!IsLoaded) {
            return;
        }

        Reset();
        if (source is null) {
            return;
        }

        using var image = await SixLabors.ImageSharp.Image.LoadAsync<Rgba32>(source.LocalPath);
        data = new ImageData(image, canvasDevice);
        graphicsDevice = CanvasComposition.CreateCompositionGraphicsDevice(compositor, canvasDevice);
        surface = graphicsDevice.CreateDrawingSurface(
            new Windows.Foundation.Size(data.Width, data.Height),
            DirectXPixelFormat.R8G8B8A8UIntNormalized,
            DirectXAlphaMode.Premultiplied);
        brush.Surface = surface;
        visual.Size = new System.Numerics.Vector2((float)ActualWidth, (float)ActualHeight);

        Draw();
    }

    private void Draw() {
        if (data is null ||
            currentFrame < 0 ||
            currentFrame >= data.FrameCount) {
            return;
        }

        using (var ds = CanvasComposition.CreateDrawingSession(surface)) {
            ds.Clear(Colors.Transparent);
            ds.DrawImage(data.Frames[currentFrame]);
        }

        if (data.IsAnimated) {
            timer.Interval = TimeSpan.FromMilliseconds(data.FrameDelaysMs[currentFrame]);

            currentFrame = (currentFrame + 1) % data.FrameCount;
            if (currentFrame == 0) {
                loop++;
            }

            if (!ShouldPlay) {
                return;
            }

            timer.Start();
        }
    }

    private void Reset() {
        timer.Stop();
        surface?.Dispose();
        graphicsDevice?.Dispose();
        data?.Dispose();
        data = null;
        currentFrame = 0;
        loop = 0;
    }

    public void Dispose() {
        WeakReferenceMessenger.Default.UnregisterAll(this);
        canvasDevice.DeviceLost -= CanvasDevice_DeviceLost;
        Reset();
    }

    void IRecipient<Messages.WindowActivated>.Receive(Messages.WindowActivated message) {
        if ((!timer.IsRunning) && ShouldPlay) {
            timer.Start();
        }
    }

    void IRecipient<Messages.WindowDeactivated>.Receive(Messages.WindowDeactivated message) {
        if (timer.IsRunning) {
            timer.Stop();
        }
    }

    private partial class ImageData: IDisposable {
        public uint MaxLoop { get; init; } // 0 = infinite
        public IReadOnlyList<double> FrameDelaysMs { get; init; }
        public IReadOnlyList<CanvasBitmap> Frames { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public int FrameCount => Frames.Count;
        public bool IsAnimated => FrameCount > 1;

        public ImageData(Image<Rgba32> image, CanvasDevice device) {
            Width = image.Width;
            Height = image.Height;

            var frameCount = image.Frames.Count;
            var delays = new double[frameCount];
            var frames = new CanvasBitmap[frameCount];

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

            var pixels = new byte[Width * Height * 4];
            for (int i = 0; i < frameCount; i++) {
                // Enforce a minimum delay of 20ms to avoid excessively fast frames
                delays[i] = Math.Max(delays[i], 20);

                var frame = image.Frames[i];
                frame.CopyPixelDataTo(pixels);
                frames[i] = CanvasBitmap.CreateFromBytes(
                    device,
                    pixels,
                    frame.Width,
                    frame.Height,
                    Windows.Graphics.DirectX.DirectXPixelFormat.R8G8B8A8UIntNormalized
                );
            }

            FrameDelaysMs = delays;
            Frames = frames;
        }

        public void Dispose() {
            foreach (var f in Frames) {
                f.Dispose();
            }
            GC.SuppressFinalize(this);
        }
    }
}
