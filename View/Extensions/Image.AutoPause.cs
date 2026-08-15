using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media.Imaging;
using View.Interfaces;

namespace View.Extensions;

public static partial class Image {
    private static readonly Dictionary<Window, HashSet<Microsoft.UI.Xaml.Controls.Image>> registeredImages = [];

    public static readonly DependencyProperty AutoPauseProperty = DependencyProperty.RegisterAttached(
        "AutoPause",
        typeof(bool),
        typeof(Image),
        new PropertyMetadata(false, OnAutoPauseChanged)
    );

    public static bool GetAutoPause(DependencyObject obj) {
        return (bool)obj.GetValue(AutoPauseProperty);
    }

    public static void SetAutoPause(DependencyObject obj, bool value) {
        obj.SetValue(AutoPauseProperty, value);
    }

    private static void OnAutoPauseChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if ((bool)e.OldValue == (bool)e.NewValue) {
            return;
        }

        if (d is not Microsoft.UI.Xaml.Controls.Image image) {
            return;
        }

        image.Loaded -= Image_Loaded;
        image.Unloaded -= Image_Unloaded;
        if ((bool)e.NewValue) {
            image.Loaded += Image_Loaded;
            image.Unloaded += Image_Unloaded;
        }
    }

    private static void Image_Loaded(object sender, RoutedEventArgs e) {
        if ((sender is Microsoft.UI.Xaml.Controls.Image image) && (Application.Current is IWindowHelper windowHelper)) {
            if (windowHelper.TryGetWindow(image, out var window) && (window is not null)) {
                if (!registeredImages.TryGetValue(window, out var images)) {
                    images = [];
                    registeredImages[window] = images;
                    window.Activated += Window_Activated;
                    window.Closed += Window_Closed;
                }

                _ = images.Add(image);
            }
        }
    }

    private static void Image_Unloaded(object sender, RoutedEventArgs e) {
        if ((sender is Microsoft.UI.Xaml.Controls.Image image) && (Application.Current is IWindowHelper windowHelper)) {
            if (windowHelper.TryGetWindow(image, out var window) && (window is not null)) {
                if (registeredImages.TryGetValue(window, out var images)) {
                    _ = images.Remove(image);
                    if (images.Count == 0) {
                        Unregister(window);
                    }
                }
            }
        }
    }

    private static void Window_Activated(object sender, WindowActivatedEventArgs args) {
        if ((sender is not Window window) || !registeredImages.TryGetValue(window, out var images)) {
            return;
        }

        bool deactivated = args.WindowActivationState == WindowActivationState.Deactivated;
        foreach (var image in images) {
            if (image.Source is BitmapImage bitmapImage) {
                if (deactivated) {
                    bitmapImage.Stop();
                } else if (bitmapImage.AutoPlay) {
                    bitmapImage.Play();
                }
            }
        }
    }

    private static void Window_Closed(object sender, WindowEventArgs args) {
        if (sender is Window window) {
            Unregister(window);
        }
    }

    private static void Unregister(Window window) {
        _ = registeredImages.Remove(window);
        window.Activated -= Window_Activated;
        window.Closed -= Window_Closed;
    }
}