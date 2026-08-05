using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using View.Interfaces;

namespace View.Pages;

internal sealed partial class Home: Page, INavigationPage {
    private readonly ViewModel.Pages.Home viewModel = new();
    private ScrollViewer? scrollViewer = null;

    public Home() {
        InitializeComponent();
    }

    public static Type Type => typeof(Home);

    public void OnNavigationReInvoke() {
        if (scrollViewer?.VerticalOffset == 0) {
            RefreshContainer.RequestRefresh();
        } else {
            //ScrollView.ScrollTo(0, 0);
            // https://github.com/microsoft/microsoft-ui-xaml/issues/9368
            scrollViewer?.ChangeView(null, 0, null, false);
        }
    }

    private void ItemsRepeater_ElementIndexChanged(ItemsRepeater obj, ItemsRepeaterElementIndexChangedEventArgs args) {
        if (args.Element is Controls.Status status) {
            status.ViewModel?.Index = args.NewIndex;
        }
    }

    private void ItemsRepeater_ElementPrepared(ItemsRepeater obj, ItemsRepeaterElementPreparedEventArgs args) {
        if (args.Element is Controls.Status status) {
            status.ViewModel?.Index = args.Index;
            _ = viewModel.OnStatusRealized(args.Index);
        }
    }

    private async void RefreshContainer_RefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args) {
        var deferral = args.GetDeferral();
        await viewModel.LoadLatestTimelines();
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
        scrollViewer = FindScrollViewer(ListView);
    }

    private ScrollViewer? FindScrollViewer(DependencyObject obj) {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++) {
            var child = VisualTreeHelper.GetChild(obj, i);

            if (child is ScrollViewer sv) {
                return sv;
            }

            var result = FindScrollViewer(child);
            if (result != null) {
                return result;
            }
        }

        return null;
    }
}
