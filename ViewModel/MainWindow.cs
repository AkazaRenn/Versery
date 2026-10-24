using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;

namespace ViewModel;

public sealed partial class MainWindow: ObservableObject, IRecipient<Messages.SignInRequested>, IRecipient<Messages.SignInCompleted> {
    private readonly Client client = Model.Services.Get<Client>();

    public event Action<Enumerations.Page, string?>? NavigationRequested;

    [ObservableProperty]
    public partial Uri? Avatar { get; set; }

    void IRecipient<Messages.SignInRequested>.Receive(Messages.SignInRequested _) {
        NavigationRequested?.Invoke(Enumerations.Page.SignIn, null);
    }

    void IRecipient<Messages.SignInCompleted>.Receive(Messages.SignInCompleted _) {
        NavigationRequested?.Invoke(Enumerations.Page.Home, null);
    }

    public MainWindow() {
        StrongReferenceMessenger.Default.Register<Messages.SignInRequested>(this);
        StrongReferenceMessenger.Default.Register<Messages.SignInCompleted>(this);
        _ = UpdateUserInfo();
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
