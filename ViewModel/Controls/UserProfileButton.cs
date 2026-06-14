using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Utilities;

namespace ViewModel.Controls; 
public sealed partial class UserProfileButton {
    [RelayCommand]
    private void Login() {
        WeakReferenceMessenger.Default.Send(new Messages.SignInRequested());
    }
}
