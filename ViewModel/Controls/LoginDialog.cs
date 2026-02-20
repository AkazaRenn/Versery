using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel.Controls;

public partial class LoginDialog: ObservableObject {
    public event Action<Uri>? LoginUriCreated;

    [ObservableProperty]
    private string instance = string.Empty;

    [RelayCommand]
    private void Login() {
        var input = Instance.Trim();
        if (!input.Contains("://")) {
            input = "https://" + input;
        }

        if (Uri.TryCreate(input, UriKind.Absolute, out var uri)) {
            LoginUriCreated?.Invoke(uri);
        } else {
            Console.WriteLine("Invalid URI: " + input);
            // optionally expose an error property for the UI
        }
    }
}
