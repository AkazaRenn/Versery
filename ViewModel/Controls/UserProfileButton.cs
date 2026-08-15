using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Model;

namespace ViewModel.Controls;

public sealed partial class UserProfileButton: ObservableObject {
    public UserProfileButton() {
    }

    [RelayCommand]
    private void AddAccount() {
        WeakReferenceMessenger.Default.Send(new WeakMessages.SignInRequested());
    }

    [RelayCommand]
    private void SignOut() {
        //WeakReferenceMessenger.Default.Send(new Messages.SignInRequested());
    }
}
