using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using View.Common;

namespace View.Pages;
internal partial class Home: Page, INavigationPage {
    private readonly ViewModel.Pages.Home? viewModel = Services.Provider?.GetService<ViewModel.Pages.Home>();

    public Home() {
        InitializeComponent();
    }

    public static Type Type => typeof(Home);

    public void OnNavigationReInvoke() {
        if (ScrollViewer.VerticalOffset == 0) {
            // Trigger refresh
        } else {
            ScrollViewer.ChangeView(null, 0, null);
        }
    }

    private readonly PostsFactory postsFactory = new();
}

public class PostsFactory: IElementFactory {
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
