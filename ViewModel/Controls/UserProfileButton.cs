using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Model;

namespace ViewModel.Controls;

public sealed partial class UserProfileButton: ObservableObject {
    [RelayCommand]
    private void AddAccount() {
        StrongReferenceMessenger.Default.Send(new Messages.SignInRequested());
    }

    [RelayCommand]
    private void SignOut() {
        //WeakReferenceMessenger.Default.Send(new Messages.SignInRequested());
    }
}
