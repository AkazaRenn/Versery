using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Model.Api;
using Utilities;

namespace ViewModel.Pages;
public sealed partial class SignIn : ObservableObject {
    [ObservableProperty]
    public partial string Instance { set; get; } = string.Empty;
    [ObservableProperty]
    public partial Uri? OAuthUri { set; get; } = null;
    [ObservableProperty]
    public partial Authentication? Authentication { set; get; } = null;

    [RelayCommand]
    private async Task StartSignIn() {
        Authentication = new Authentication(Instance.Trim());
        var oAuthUrlString = await Authentication.OAuthUrl();
        if (Uri.TryCreate(oAuthUrlString, UriKind.Absolute, out var uri)) {
            OAuthUri = uri;
        } else {
            Authentication = null;
        }
    }

    [RelayCommand]
    private async Task CheckSignInUri(string uri) {
        if (Authentication is null) {
            return;
        }
        if (await Authentication.CheckSignInUrl(uri)) {
            WeakReferenceMessenger.Default.Send(new Messages.SignInCompleted());
        }
    }
}
