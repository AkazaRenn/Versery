using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;

namespace ViewModel.Controls;

public sealed partial class UserProfileButton: ObservableObject, IRecipient<Messages.SignInCompleted> {
    private readonly Client client = Model.Services.Get<Client>();

    [ObservableProperty]
    public partial Uri? Avatar { get; set; }

    public UserProfileButton() {
        _ = UpdateUserInfo();
    }

    [RelayCommand]
    private void AddAccount() {
        WeakReferenceMessenger.Default.Send(new Messages.SignInRequested());
    }

    [RelayCommand]
    private void SignOut() {
        //WeakReferenceMessenger.Default.Send(new Messages.SignInRequested());
    }

    private async Task UpdateUserInfo() {
        var uri = (await client.GetAccount())?.Avatar;
        if (uri is not null) {
            Avatar = await Cache.Get(uri);
        }
    }

    public void Receive(Messages.SignInCompleted message) {
        _ = UpdateUserInfo();
    }
}
