using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Shapes;
using View.Interfaces;

namespace View.Controls.Components;

internal partial class ImageButton: Grid {
    private Window? window;

    private readonly ImageBrush ImageBrush = new() {
        Stretch = Stretch.UniformToFill,
        AlignmentX = AlignmentX.Center,
        AlignmentY = AlignmentY.Center,
    };
    private readonly Rectangle ImageRectangle = new();
    private readonly Button Button = new() {
        VerticalAlignment = VerticalAlignment.Stretch,
        HorizontalAlignment = HorizontalAlignment.Stretch,
        CornerRadius = new(0),
        Padding = new(0),
        Style = (Style)Application.Current.Resources["SubtleButtonStyle"],
    };

    public ImageSource? ImageSource {
        get => ImageBrush.ImageSource;
        set => ImageBrush.ImageSource = value;
    }
    public Visibility ImageVisibility {
        get => ImageRectangle.Visibility;
        set => ImageRectangle.Visibility = value;
    }
    public FlyoutBase Flyout {
        get => Button.Flyout;
        set => Button.Flyout = value;
    }
    public bool IsEnabled {
        get => Button.IsEnabled;
        set => Button.IsEnabled = value;
    }

    public ImageButton() {
        ImageRectangle.Fill = ImageBrush;
        ImageRectangle.Loaded += ImageRectangle_Loaded;
        ImageRectangle.Unloaded += ImageRectangle_Unloaded;
        Children.Add(ImageRectangle);
        Children.Add(Button);
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
        if ((ImageBrush.ImageSource is BitmapImage bitmapImage) && bitmapImage.AutoPlay) {
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
