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
                using var image = Blurhasher.Decode(BackgroundBlur.Hash, width, height);
                var buffer = new byte[image.Width * image.Height * 4];
                image.CloneAs<Bgra32>().CopyPixelDataTo(buffer);
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
