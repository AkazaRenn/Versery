using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Utilities.Interfaces;

namespace View.Pages;

internal sealed partial class SignIn: Page, INavigationPage {
    private readonly ViewModel.Pages.SignIn viewModel = Utilities.Services.Provider.GetRequiredService<ViewModel.Pages.SignIn>();

    public SignIn() {
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
        if (viewModel?.CheckSignInUriCommand.CanExecute(args.Uri) == true) {
            viewModel.CheckSignInUriCommand.Execute(args.Uri);
        }
    }

    public void OnNavigationReInvoke() { }

    public static Type Type => typeof(SignIn);

    private void WebView_Loading(FrameworkElement sender, object args) {
        ProgressBar.Visibility = Visibility.Visible;
    }

    private void WebView_Loaded(object sender, RoutedEventArgs e) {
        ProgressBar.Visibility= Visibility.Collapsed;
    }
}
