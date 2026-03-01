using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using View.Common;

namespace View.Pages;

internal partial class Login: Page, INavigationPage {
    private readonly ViewModel.Pages.Login? viewModel = Services.Provider?.GetService<ViewModel.Pages.Login>();

    public Login() {
        InitializeComponent();
    }

    private async void Page_Unloaded(object sender, RoutedEventArgs e) {
        var webView = WebView;
        if (webView.CoreWebView2 != null) {
            await webView.CoreWebView2.Profile.ClearBrowsingDataAsync();
        }
        webView.Close();
    }

    private void WebView_NavigationStarting(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs args) {
        if (viewModel?.CheckLoginUriCommand.CanExecute(args.Uri) == true) {
            viewModel.CheckLoginUriCommand.Execute(args.Uri);
        }
    }

    public void OnNavigationReInvoke() { }

    public static Type Type => typeof(Login);

    private void WebView_Loading(FrameworkElement sender, object args) {
        ProgressBar.Visibility = Visibility.Visible;
    }

    private void WebView_Loaded(object sender, RoutedEventArgs e) {
        ProgressBar.Visibility= Visibility.Collapsed;
    }
}
