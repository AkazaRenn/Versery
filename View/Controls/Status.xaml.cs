using Microsoft.UI.Xaml.Controls;

namespace View.Controls; 
internal sealed partial class Status: Grid {
    public ViewModel.Controls.Status? ViewModel { get; set; }

    public Status() {
        InitializeComponent();
    }

    private void LoadMore() {
        ViewModel!.LoadMore(this.ElementIndex);
    }
}
