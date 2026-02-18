using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ViewModel.Pages;
public partial class Login : ObservableObject {
    [ObservableProperty]
    private string instance = string.Empty;

    [RelayCommand]
    private void StartLogin() {
        Console.WriteLine("Logging in with instance: " + Instance);
    }
}
