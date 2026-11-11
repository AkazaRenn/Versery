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
        get => currentContainer == DefaultContainer;
        set {
            if (value) {
                if (!IsUsingDefaultContent) {
                    Switch();
                }
            } else {
                if (IsUsingDefaultContent) {
                    Switch();
                }
            }
        }
    }

    private Border currentContainer;
    private Border nextContainer;
    public void Switch() {
        currentContainer.Visibility = Visibility.Collapsed;
        nextContainer.Visibility = Visibility.Visible;
        (currentContainer, nextContainer) = (nextContainer, currentContainer);
    }
    public void Change(UIElement newContent) {
        nextContainer.Child = newContent;
        Switch();
    }

    public SwitchContentPanel() {
        InitializeComponent();
        currentContainer = DefaultContainer;
        nextContainer = AlternateContainer;
    }
}
