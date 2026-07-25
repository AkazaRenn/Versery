using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace View.Controls;

internal sealed partial class Status: Grid {
    public ViewModel.Controls.Status? ViewModel {
        get;
        set {
            if (field != value) {
                field = value;
                Bindings.Update();
            }
        }
    }

    public Status() {
        InitializeComponent();
    }
}
