using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace ViewModel.Controls; 
public partial class UserProfileButton {
    [RelayCommand]
    private void Login() {
        WeakReferenceMessenger.Default.Send(new Messages.LoginRequested());
    }
}
