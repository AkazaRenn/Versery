using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.Pages;
public partial class Login : ObservableObject {
    public event Action<string>? LoginFinished;

    public void CheckLoginUri(string uri) {
        Console.WriteLine("Logging in with instance: " + uri);
        Model.Credentials.AddToken("xxx");
    }
}
