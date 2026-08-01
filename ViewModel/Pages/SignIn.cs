using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.Access;

namespace ViewModel.Pages;

public sealed partial class SignIn: ObservableObject {
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
    private void CheckSignInUri(string uri) {
        if (Authentication is null) {
            return;
        }
        _ = Authentication.CheckSignInUrl(uri);
    }
}
