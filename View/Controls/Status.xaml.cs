using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace View.Controls;

internal sealed partial class Status: Grid {
    public ViewModel.Controls.Status? ViewModel {
        get;
        set {
            if (field != value) {
                field = value;
                UpdateAdditionalElement();
                Bindings.Update();
            }
        }
    }

    private UIElement? additionalElement = null;

    public Status() {
        InitializeComponent();
    }

    private void UpdateAdditionalElement() {
        additionalElement = null;
    }
}
