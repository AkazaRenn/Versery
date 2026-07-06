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
    public ItemsRepeater? ParentItemsRepeater { get; set; }
    public int Index {
        get => ViewModel?.Index ?? 0;
        set {
            ViewModel?.Index = value;
        }
    }

    public Status() {
        InitializeComponent();
    }
}
