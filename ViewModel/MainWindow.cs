using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;

namespace ViewModel;

public sealed partial class MainWindow: ObservableObject, IRecipient<WeakMessages.SignInRequested>, IRecipient<WeakMessages.SignInCompleted> {
    private readonly Client client = Model.Services.Get<Client>();

    public event Action<Enumerations.Page, string?>? NavigationRequested;

    [ObservableProperty]
    public partial Uri? Avatar { get; set; }

    void IRecipient<WeakMessages.SignInRequested>.Receive(WeakMessages.SignInRequested _) {
        NavigationRequested?.Invoke(Enumerations.Page.SignIn, null);
    }

    void IRecipient<WeakMessages.SignInCompleted>.Receive(WeakMessages.SignInCompleted _) {
        NavigationRequested?.Invoke(Enumerations.Page.Home, null);
    }

    public MainWindow() {
        WeakReferenceMessenger.Default.RegisterAll(this);
        _ = UpdateUserInfo();
    }

    private async Task UpdateUserInfo() {
        var uri = (await client.GetAccount())?.Avatar;
        if (uri is not null) {
            Avatar = await Cache.Get(uri);
        }
    }

    public void Receive(WeakMessages.SignInCompleted message) {
        _ = UpdateUserInfo();
    }
}
