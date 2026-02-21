using CommunityToolkit.Mvvm.Messaging;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using static View.Messages;

namespace View.Controls; 
public sealed partial class LoginDialog: ContentDialog {
    private readonly ViewModel.Controls.LoginDialog viewModel;

    public LoginDialog(XamlRoot xamlRoot) {
        InitializeComponent();

        viewModel = new ViewModel.Controls.LoginDialog();
        viewModel.LoginUriCreated += uri => 
            WeakReferenceMessenger.Default.Send(new NavigationRequest(typeof(Pages.Login), uri));
        DataContext = viewModel;
        XamlRoot = xamlRoot;

        _ = ShowAsync();
    }
}
