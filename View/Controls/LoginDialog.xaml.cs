using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace View.Controls; 
public sealed partial class LoginDialog: ContentDialog {
    public LoginDialog(Frame? frame, XamlRoot xamlRoot) {
        InitializeComponent();

        var viewModel = new ViewModel.Controls.LoginDialog();
        viewModel.LoginUriCreated += uri => frame?.Navigate(typeof(View.Pages.Login), uri);
        DataContext = viewModel;
        XamlRoot = xamlRoot;

        _ = ShowAsync();
    }
}
