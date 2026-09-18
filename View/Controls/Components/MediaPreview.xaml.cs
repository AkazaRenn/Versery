using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace View.Controls.Components;

internal sealed partial class MediaPreview: UserControl {
    public Uri? UriSource {
        get => BitmapImage.UriSource;
        set => BitmapImage.UriSource = value;
    }
    public bool HideImage {
        get => ButtonWithImage.ImageVisibility == Visibility.Collapsed;
        set => ButtonWithImage.ImageVisibility = value ? Visibility.Collapsed : Visibility.Visible;
    }

    public string BackgroundBlurHash {
        get;
        set {
            if (field != value) {
                field = value;
            }
        }
    } = string.Empty;
    static public Brush GetBackground(string BackgroundBlurHash) {
        if (String.IsNullOrEmpty(BackgroundBlurHash)) {
            return new SolidColorBrush(Colors.Red);
        } else {
            return new SolidColorBrush(Colors.Blue);
        }
    }

    public MediaPreview() {
        InitializeComponent();
    }
}
