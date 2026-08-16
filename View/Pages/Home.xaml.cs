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

    private async void RefreshListView_RefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args) {
        var deferral = args.GetDeferral();
        await viewModel.LoadLatestTimelines();
        RefreshListView.ScrollToTop();
        deferral.Complete();
    }

    private void RefreshListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args) {
        if (args.InRecycleQueue)
            return;

        if (args.Phase == 0 && args.Item is ViewModel.Controls.Timeline status) {
            status.Index = args.ItemIndex;
            _ = viewModel.OnStatusRealized(args.ItemIndex);
        }
    }
}
