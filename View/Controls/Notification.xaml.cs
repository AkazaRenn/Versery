using Microsoft.UI.Xaml.Controls;

namespace View.Controls;

internal sealed partial class Notification: Grid {
    public ViewModel.Controls.Notification? ViewModel {
        get;
        set {
            if (field != value) {
                field = value;
                // Bindings.Update();
            }
        }
    }

    public Notification() {
        InitializeComponent();
    }
}
