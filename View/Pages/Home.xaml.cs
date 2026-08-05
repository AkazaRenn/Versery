using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using View.Interfaces;

namespace View.Pages;

internal sealed partial class Home: Page, INavigationPage {
    private readonly ViewModel.Pages.Home viewModel = new();
    private ScrollViewer? scrollViewer = null;

    public Home() {
        InitializeComponent();
    }

    public static Type Type => typeof(Home);

    public async Task OnNavigationReInvoke() {
        if (scrollViewer?.VerticalOffset == 0) {
            RefreshContainer.RequestRefresh();
        } else {
            //ScrollView.ScrollTo(0, 0);
            // https://github.com/microsoft/microsoft-ui-xaml/issues/9368
            if ((ListView.ContainerFromIndex(0) is not null) &&
                (ListView.ContainerFromIndex(1) is not null)) {
                scrollViewer?.ChangeView(null, 0, null, false);
            } else {
                if (scrollViewer is not null) {
                    RefreshContainer.Opacity = 0;
                    await Task.Delay(RefreshContainer.OpacityTransition.Duration);
                    scrollViewer.ScrollToVerticalOffset(0);
                    RefreshContainer.Opacity = 1;
                }
            }
        }
    }

    private async void RefreshContainer_RefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args) {
        var deferral = args.GetDeferral();
        await viewModel.LoadLatestTimelines();
        scrollViewer?.ScrollToVerticalOffset(0);
        deferral.Complete();
    }

    private void ListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args) {
        if (args.InRecycleQueue)
            return;

        if (args.Phase == 0 && args.Item is ViewModel.Controls.Status status) {
            status.Index = args.ItemIndex;
        }
    }

    private void Page_Loaded(object sender, RoutedEventArgs e) {
        scrollViewer = ListView.FindDescendant<ScrollViewer>();
    }
}
