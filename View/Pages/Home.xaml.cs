using Microsoft.UI.Xaml.Controls;
using View.Interfaces;

namespace View.Pages;
internal sealed partial class Home: Page, INavigationPage {
    private readonly ViewModel.Pages.Home viewModel = new();

    public Home() {
        InitializeComponent();
    }

    public static Type Type => typeof(Home);

    public void OnNavigationReInvoke() {
        if (ScrollView.VerticalOffset == 0) {
            _ = viewModel.LoadLatestTimelines();
        } else {
            // ScrollView.ScrollTo(0, 0);
            // https://github.com/microsoft/microsoft-ui-xaml/issues/9368
            ScrollView.ScrollToVerticalOffset(0);
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
}
