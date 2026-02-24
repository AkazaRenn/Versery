using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;

namespace View.Pages;

internal partial class Login: Page {
    private readonly ViewModel.Pages.Login? viewModel = Services.Provider?.GetService<ViewModel.Pages.Login>();

    public Login() {
        InitializeComponent();

        DataContext = viewModel;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e) {
        base.OnNavigatedTo(e);

        if (e.Parameter is Uri uri) {
            WebView.Source = uri;
        }
    }

    private void Page_Unloaded(object sender, RoutedEventArgs e) {
        WebView.Close();
    }

    private void WebView_NavigationStarting(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs args) {
        viewModel?.CheckLoginUri(args.Uri);
        Console.WriteLine("New URL: " + args.Uri.ToString());
    }
}
