using Blurhash;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;

namespace View.Controls.Components;

internal sealed partial class MediaPreview: UserControl {
    public Uri? UriSource {
        get => BitmapImage.UriSource;
        set => BitmapImage.UriSource = value;
    }
    public bool HideImage {
        get => ImageButton.ImageVisibility == Visibility.Collapsed;
        set => ImageButton.ImageVisibility = value ? Visibility.Collapsed : Visibility.Visible;
    }

    internal static readonly SolidColorBrush DefaultBackground = Application.Current.Resources["CardBackgroundFillColorDefaultBrush"] as SolidColorBrush ?? new(Colors.LightGray);
    public Blur? BackgroundBlur {
        get;
        set {
            if (field != value) {
                field = value;
                _ = UpdateBackground();
            }
        }
    } = null;

    private async Task UpdateBackground() {
        if ((BackgroundBlur is null) ||
            String.IsNullOrWhiteSpace(BackgroundBlur.Hash) ||
            (BackgroundBlur.AspectRatio <= 0)) {
            ImageButton.Background = DefaultBackground;
        } else {
            int width = 32;
            int height = 32;
            var pixels = await Task.Run(() => {
                var decoded = new Pixel[width, height];
                Core.Decode(BackgroundBlur.Hash, decoded);
                var buffer = new byte[width * height * 4];
                for (var y = 0; y < height; y++) {
                    for (var x = 0; x < width; x++) {
                        var pixel = decoded[x, y];
                        var i = ((y * width) + x) * 4;
                        buffer[i + 0] = (byte)MathUtils.LinearTosRgb(pixel.Blue);
                        buffer[i + 1] = (byte)MathUtils.LinearTosRgb(pixel.Green);
                        buffer[i + 2] = (byte)MathUtils.LinearTosRgb(pixel.Red);
                        buffer[i + 3] = 255;
                    }
                }
                return buffer;
            });

            var bitmap = new WriteableBitmap(width, height);
            using (var stream = bitmap.PixelBuffer.AsStream()) {
                stream.Write(pixels);
            }
            bitmap.Invalidate();

            if (ImageButton.Background is ImageBrush imageBrush) {
                imageBrush.ImageSource = bitmap;
            } else {
                ImageButton.Background = new ImageBrush {
                    ImageSource = bitmap,
                    Stretch = Stretch.UniformToFill,
                    AlignmentX = AlignmentX.Center,
                    AlignmentY = AlignmentY.Center,
                };
            }
        }
    }

    public MediaPreview() {
        InitializeComponent();
    }

    public record Blur(string Hash, double AspectRatio);
}
