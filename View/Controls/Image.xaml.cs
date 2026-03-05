using CommunityToolkit.WinUI;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Xaml;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Windows.Graphics.DirectX;

namespace View.Controls;

internal sealed partial class Image: UserControl {
    private int currentFrame = 0;
    private uint loop = 0;
    private DispatcherQueueTimer timer;
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

    public Image() {
        InitializeComponent();

        var dispatcherQueue = DispatcherQueue.GetForCurrentThread();
        timer = dispatcherQueue.CreateTimer();
        timer.Tick += Timer_Tick;
        timer.IsRepeating = false;
    }

    private void Timer_Tick(object? sender, object e) {
        if (data is null) {
            return;
        }

        currentFrame = (currentFrame + 1) % data.FrameCount;
        if (currentFrame == 0) {
            loop++;
        }

        if ((data.MaxLoop > 0) && (loop >= data.MaxLoop)) {
            return;
        }

        timer.Interval = TimeSpan.FromMilliseconds(data.FrameDelaysMs[currentFrame]);

        Canvas.Invalidate();
    }

    private void Canvas_Draw(CanvasControl sender, CanvasDrawEventArgs args) {
        if (data is null) {
            return;
        }

        if (currentFrame < data.FrameCount) {
            args.DrawingSession.DrawImage(
                data.Frames[currentFrame],
                0, 0,
                sender.Size.ToRect(),
                1.0f,
                CanvasImageInterpolation.Linear
            );
        }
    }

    private void UserControl_Loaded(object sender, RoutedEventArgs e) {
        if ((data?.IsAnimated == true) && (!timer.IsRunning)) {
            timer.Start();
        }
    }

    private void UserControl_Unloaded(object sender, RoutedEventArgs e) {
        Reset();
    }

    private void Canvas_CreateResources(CanvasControl sender, Microsoft.Graphics.Canvas.UI.CanvasCreateResourcesEventArgs args) {
        _ = LoadImage();
    }

    private void Reset() {
        timer.Stop();
        data?.Dispose();
        data = null;
        currentFrame = 0;
        loop = 0;
    }

    public async Task LoadImage() {
        if (!IsLoaded) {
            return;
        }

        Reset();
        if (source is null) {
            return;
        }

        using var image = await SixLabors.ImageSharp.Image.LoadAsync<Rgba32>(source.LocalPath);
        data = new ImageData(image, Canvas.Device);
        if (data.IsAnimated) {
            timer.Interval = TimeSpan.FromMilliseconds(data.FrameDelaysMs[0]);
            timer.Start();
        }
        Canvas.Invalidate();
    }
}

internal partial class ImageData: IDisposable {
    public uint MaxLoop { get; init; } // 0 = infinite
    public IReadOnlyList<double> FrameDelaysMs { get; init; } = [];
    public IReadOnlyList<CanvasBitmap> Frames { get; init; } = [];
    public int FrameCount => Frames.Count;
    public bool IsAnimated => FrameCount > 1;

    public ImageData(Image<Rgba32> image, CanvasDevice device) {
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

        var pixels = new byte[image.Width * image.Height * 4];
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
                DirectXPixelFormat.R8G8B8A8UIntNormalized
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
