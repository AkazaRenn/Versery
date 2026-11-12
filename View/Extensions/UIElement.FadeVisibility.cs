using CommunityToolkit.WinUI.Animations;
using Microsoft.UI.Xaml;
using View.Utilities;

namespace View.Extensions;

public static partial class UIElement {
    public static readonly DependencyProperty FadeVisibilityProperty = DependencyProperty.RegisterAttached(
        "FadeVisibility",
        typeof(bool),
        typeof(UIElement),
        new PropertyMetadata(false, OnFadeVisibilityChanged)
    );

    public static bool GetFadeVisibility(DependencyObject obj) {
        return (bool)obj.GetValue(FadeVisibilityProperty);
    }

    public static void SetFadeVisibility(DependencyObject obj, bool value) {
        obj.SetValue(FadeVisibilityProperty, value);
    }

    private static void OnFadeVisibilityChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if ((bool)e.OldValue == (bool)e.NewValue) {
            return;
        }

        if (d is not Microsoft.UI.Xaml.UIElement element) {
            return;
        }

        if ((bool)e.NewValue) {
            // The toolkit requires separate animation instances per element.
            Implicit.SetShowAnimations(element, [
                new OpacityAnimation {
                    From = 0,
                    To = 1,
                    Duration = Transitions.DefaultDuration,
                },
            ]);
            Implicit.SetHideAnimations(element, [
                new OpacityAnimation {
                    From = 1,
                    To = 0,
                    Duration = Transitions.DefaultDuration,
                },
            ]);
        } else {
            element.ClearValue(Implicit.ShowAnimationsProperty);
            element.ClearValue(Implicit.HideAnimationsProperty);
        }
    }
}
