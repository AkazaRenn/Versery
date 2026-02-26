using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel.Controls;

public partial class LoginDialog(): ObservableObject {
    public event Action<Uri>? LoginUriCreated;

    private Model.Authentication? authentication = null;

    [ObservableProperty]
    private string instance = string.Empty;

    [RelayCommand]
    private async Task Login() {
        authentication = new Model.Authentication(Instance.Trim());
        if (authentication != null && Uri.TryCreate(authentication.OAuthUrl, UriKind.Absolute, out var oAuthUri)) {
            LoginUriCreated?.Invoke(oAuthUri);
        }
    }
}
