using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;

namespace ViewModel.Controls;

public partial class LoginDialog(Model.Client client): ObservableObject {
    public event Action<Uri>? LoginUriCreated;

    [ObservableProperty]
    private string instance = string.Empty;

    [RelayCommand]
    private async Task Login() {
        if (Uri.TryCreate(await client.OAuthUrl(Instance.Trim()), UriKind.Absolute, out var oAuthUri)) {
            LoginUriCreated?.Invoke(oAuthUri);
        }
    }
}
