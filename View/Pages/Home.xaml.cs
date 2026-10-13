using Microsoft.UI.Xaml.Controls;
using View.Interfaces;

namespace View.Pages;

internal sealed partial class Home: Page, INavigationPage {
    private readonly ViewModel.Pages.Home viewModel = new();

    public Home() {
        InitializeComponent();
    }

    public static Type Type => typeof(Home);

    public async Task OnNavigationReInvoke() {
        if (RefreshListView.IsOnTop) {
            RefreshListView.RequestRefresh();
        } else {
            RefreshListView.ScrollToTop();
        }
    }

    private void RefreshListView_OnItemRealized(int index) {
        _ = viewModel.OnItemRealized(index);
    }
}
