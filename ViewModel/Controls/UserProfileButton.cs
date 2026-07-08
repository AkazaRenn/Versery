using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Model.Access;
using Utilities;

namespace ViewModel.Controls; 
public sealed partial class UserProfileButton: ObservableObject, IRecipient<Messages.SignInCompleted> {
    private readonly Client client = Utilities.Services.Get<Client>();

    [ObservableProperty]
    public partial Uri? Avatar { get; set; }

    public UserProfileButton() {
        _ = UpdateUserInfo();
    }

    [RelayCommand]
    private void Login() {
        WeakReferenceMessenger.Default.Send(new Messages.SignInRequested());
    }

    private async Task UpdateUserInfo() {
        var url = (await client.GetAccount())?.AvatarUrl;
        if (url is not null) {
            Avatar = await Cache.Get(url);
        }
    }

    public void Receive(Messages.SignInCompleted message) {
        _ = UpdateUserInfo();
    }
}
