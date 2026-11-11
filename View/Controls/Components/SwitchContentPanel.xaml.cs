using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace View.Controls.Components;

internal sealed partial class SwitchContentPanel: Grid {
    public UIElement DefaultContent {
        get => DefaultContainer.Child;
        set => DefaultContainer.Child = value;
    }
    public UIElement AlternateContent {
        get => AlternateContainer.Child;
        set => AlternateContainer.Child = value;
    }
    public UIElement CurrentContent {
        get => currentContainer.Child;
        set => Change(value);
    }
    public bool IsUsingDefaultContent {
        set {
            if (value) {
                SwitchToDefault();
            } else {
                SwitchToAlternate();
            }
        }
    }

    public Border currentContainer;
    public Border nextContainer;
    public void Switch() {
        currentContainer.Visibility = Visibility.Collapsed;
        nextContainer.Visibility = Visibility.Visible;
        (currentContainer, nextContainer) = (nextContainer, currentContainer);
    }
    public void Change(UIElement newContent) {
        nextContainer.Child = newContent;
        Switch();
    }
    public void SwitchToDefault() {
        if (currentContainer != DefaultContainer) {
            Switch();
        }
    }
    public void SwitchToAlternate() {
        if (currentContainer != AlternateContainer) {
            Switch();
        }
    }

    public SwitchContentPanel() {
        InitializeComponent();
        currentContainer = DefaultContainer;
        nextContainer = AlternateContainer;
    }
}
