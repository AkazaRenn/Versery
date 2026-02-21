using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace View.Pages;

internal partial class Login: Page {
    private readonly ViewModel.Pages.Login viewModel = new();

    public Login() {
        InitializeComponent();

        DataContext = viewModel;
    }

    private void Page_Unloaded(object sender, RoutedEventArgs e) {
        WebView.Close();
    }

    private void WebView_NavigationStarting(WebView2 sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs args) {
        viewModel.CheckLoginUri(args.Uri);
        Console.WriteLine("New URL: " + args.Uri.ToString());
    }
}
