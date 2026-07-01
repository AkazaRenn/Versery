using Microsoft.UI.Xaml.Controls;

namespace View.Controls; 
internal sealed partial class Status: Grid {
    public ViewModel.Controls.Status? ViewModel { get; set; }
    public ItemsRepeater? ParentItemsRepeater { get; set; }
    public int Index { get; set; } = 0;

    public Status() {
        InitializeComponent();
    }
}
