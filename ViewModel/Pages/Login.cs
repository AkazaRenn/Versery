using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Model;

namespace ViewModel.Pages;
public partial class Login : ObservableObject {
    [ObservableProperty]
    private string instance = string.Empty;

    [ObservableProperty]
    private Uri? oAuthUri = null;

    [ObservableProperty]
    private Model.Authentication? authentication = null;

    [RelayCommand]
    private async Task StartLogin() {
        Authentication = new Model.Authentication(Instance.Trim());
        await Authentication.Register();
        if (Uri.TryCreate(Authentication.OAuthUrl, UriKind.Absolute, out var oAuthUri)) {
            OAuthUri = oAuthUri;
        } else { 
            Authentication = null;
        }
    }

    public async void CheckLoginUri(string uri) {
        if(await Authentication!.CheckLoginUrl(uri)) {
            WeakReferenceMessenger.Default.Send(new Messages.LoginCompleted());
        }
    }
}
