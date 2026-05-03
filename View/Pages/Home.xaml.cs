using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using View.Common;

namespace View.Pages;
internal partial class Home: Page, INavigationPage {
    private readonly ViewModel.Pages.Home viewModel = Services.Provider.GetRequiredService<ViewModel.Pages.Home>();

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

    private readonly StatusesFactory statusesFactory = new();
}

public class StatusesFactory: IElementFactory {
    public UIElement GetElement(ElementFactoryGetArgs args) {
        return new TextBlock {
            Text = args.Data as string,
            TextWrapping = TextWrapping.WrapWholeWords,
        };
    }

    public void RecycleElement(ElementFactoryRecycleArgs args) {
        //throw new NotImplementedException();
    }
}
