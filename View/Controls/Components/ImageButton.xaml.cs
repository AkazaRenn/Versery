using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using View.Interfaces;

namespace View.Controls.Components;

internal sealed partial class ImageButton: Grid {
    private Window? window;

    public ImageSource? ImageSource {
        get => ImageBrush.ImageSource;
        set => ImageBrush.ImageSource = value;
    }
    public Visibility ImageVisibility {
        get => ImageRectangle?.Visibility ?? Visibility.Collapsed;
        set => ImageRectangle?.Visibility = value;
    }
    public FlyoutBase Flyout {
        get => Button.Flyout;
        set => Button.Flyout = value;
    }

    public ImageButton() {
        InitializeComponent();
    }

    private void ImageRectangle_Loaded(object sender, RoutedEventArgs e) {
        if ((window is null) && (Application.Current is IWindowHelper windowHelper)) {
            if (windowHelper.TryGetWindow(this, out window)) {
                window?.Activated -= Window_Activated;
                window?.Activated += Window_Activated;
            }
        }
    }

    private void ImageRectangle_Unloaded(object sender, RoutedEventArgs e) {
        window?.Activated -= Window_Activated;
    }

    private void Window_Activated(object sender, WindowActivatedEventArgs args) {
        if (ImageBrush.ImageSource is BitmapImage bitmapImage && bitmapImage.AutoPlay) {
            switch (args.WindowActivationState) {
            case WindowActivationState.Deactivated:
                bitmapImage.Stop();
                break;
            default:
                bitmapImage.Play();
                break;
            }
        }
    }
}
