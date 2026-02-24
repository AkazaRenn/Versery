using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using static View.Messages;

namespace View.Controls; 
public sealed partial class LoginDialog: ContentDialog {
    private readonly ViewModel.Controls.LoginDialog? viewModel = Services.Provider?.GetService<ViewModel.Controls.LoginDialog>();

    public LoginDialog() {
        InitializeComponent();

        viewModel?.LoginUriCreated += url =>
            WeakReferenceMessenger.Default.Send(new NavigationRequest(typeof(Pages.Login), url));
        DataContext = viewModel;
    }

    public void Show(XamlRoot xamlRoot) {
        XamlRoot = xamlRoot;
        _ = ShowAsync();
    }
}
