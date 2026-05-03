using Microsoft.UI.Xaml.Controls;

namespace View.Controls; 
public sealed partial class Status: Grid {
    private readonly ViewModel.Controls.Status viewModel;

    public Status(ViewModel.Controls.Status _viewModel) {
        InitializeComponent();

        viewModel = _viewModel;
    }
}
