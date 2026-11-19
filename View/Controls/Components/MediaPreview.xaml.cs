using Blurhash;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Runtime.InteropServices.WindowsRuntime;
using View.Extensions;

namespace View.Controls.Components;

internal sealed partial class MediaPreview: ImageButton {
    public Uri? UriSource {
        get => BitmapImage.UriSource;
        set => BitmapImage.UriSource = value;
    }
    public double Aspect {
        get;
        set {
            if (field != value) {
                field = value;
                // MediaGrid sizes its tiles from this.
                (Parent as Microsoft.UI.Xaml.UIElement)?.InvalidateMeasure();
            }
        }
    } = 1;
    public string? Description {
        get => Tooltip;
        set => Tooltip = string.IsNullOrWhiteSpace(value) ? null : value;
    }
    public string? Blurhash {
        get;
        set {
            if (field != value) {
                field = value;
                _ = UpdateBlur();
            }
        }
    } = null;
    public bool BlurFadeVisibility {
        get => Blur.FadeVisibility;
        set => Blur.FadeVisibility = value;
    }
    public double BlurOpacity {
        get => Blur.Opacity;
        set => Blur.Opacity = value;
    }

    internal static readonly SolidColorBrush DefaultBlur = Application.Current.Resources["CardBackgroundFillColorDefaultBrush"] as SolidColorBrush ?? new(Colors.LightGray);
    private async Task UpdateBlur() {
        if (String.IsNullOrWhiteSpace(Blurhash) ||
            (Aspect <= 0)) {
            Blur.Fill= DefaultBlur;
        } else {
            int width = 32;
            int height = 32;
            var pixels = await Task.Run(() => {
                var decoded = new Pixel[width, height];
                Core.Decode(Blurhash, decoded);
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

            if (Blur.Fill is ImageBrush imageBrush) {
                imageBrush.ImageSource = bitmap;
            } else {
                Blur.Fill = new ImageBrush {
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
}
