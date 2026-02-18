using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel.Controls;

public partial class LoginDialog: ObservableObject {
    public event Action<Uri>? LoginUriCreated;

    [ObservableProperty]
    private string instance = string.Empty;

    [RelayCommand]
    private void Login() {
        Console.WriteLine("Logging in with domain: " + Instance);
        LoginUriCreated?.Invoke(new Uri($"https://{Instance}"));
    }
}
