using Microsoft.UI.Xaml.Controls;
using Utilities.Interfaces;

namespace View.Pages;
internal sealed partial class Home: Page, INavigationPage {
    private readonly ViewModel.Pages.Home viewModel = new();

    public Home() {
        InitializeComponent();
    }

    public static Type Type => typeof(Home);

    public void OnNavigationReInvoke() {
        if (ScrollView.VerticalOffset == 0) {
            // Trigger refresh
        } else {
            ScrollView.ScrollTo(0, 0);
        }
    }

    private void ItemsRepeater_ElementIndexChanged(ItemsRepeater _, ItemsRepeaterElementIndexChangedEventArgs args) {
        if (args.Element is Controls.Status status) {
            status.ViewModel?.Index = args.NewIndex;
        }
    }

    private void ItemsRepeater_ElementPrepared(ItemsRepeater _, ItemsRepeaterElementPreparedEventArgs args) {
        if (args.Element is Controls.Status status) {
            status.ViewModel?.Index = args.Index;
        }
    }
}
