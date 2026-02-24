using CommunityToolkit.Mvvm.ComponentModel;
using System.Text.RegularExpressions;

namespace ViewModel.Pages;
public partial class Login : ObservableObject {
    private static readonly Regex authRegex = new Regex(@"/oauth/authorize/native\?code=([a-zA-Z0-9_-]+)", RegexOptions.Compiled);

    public event Action<string>? LoginFinished;

    public void CheckLoginUri(string uri) {
        if (authRegex.IsMatch(uri)) {
            Model.Credentials.AddToken("xxx");
        }
    }
}
