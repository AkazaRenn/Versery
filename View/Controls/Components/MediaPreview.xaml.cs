using Blurhash.ImageSharp;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using SixLabors.ImageSharp.PixelFormats;
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
    public string BackgroundBlurHash { get; set; } = string.Empty;
    public double AspectRatio { get; set; } = 1;

    private async void ImageButton_Loaded(object sender, RoutedEventArgs e) {
        if (String.IsNullOrEmpty(BackgroundBlurHash)) {
            ImageButton.Background = DefaultBackground;
        } else {
            var (width, height, pixels) = await Task.Run(() => {
                int width, height;
                if (AspectRatio > 1) {
                    width = (int)(32 * AspectRatio);
                    height = 32;
                } else {
                    width = 32;
                    height = (int)(32 / AspectRatio);
                }
                using var image = Blurhasher.Decode(BackgroundBlurHash, width, height);
                var buffer = new byte[image.Width * image.Height * 4];
                image.CloneAs<Bgra32>().CopyPixelDataTo(buffer);
                return (image.Width, image.Height, buffer);
            });

            var bitmap = new WriteableBitmap(width, height);
            using (var stream = bitmap.PixelBuffer.AsStream()) {
                stream.Write(pixels);
            }
            bitmap.Invalidate();

            ImageButton.Background = new ImageBrush {
                ImageSource = bitmap,
                Stretch = Stretch.UniformToFill,
                AlignmentX = AlignmentX.Center,
                AlignmentY = AlignmentY.Center,
            };
        }
    }

    public MediaPreview() {
        InitializeComponent();
    }
}
