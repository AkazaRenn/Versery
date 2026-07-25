using CommunityToolkit.Mvvm.Messaging;
using Model;

namespace ViewModel; 
public sealed partial class MainWindow: IRecipient<Messages.SignInRequested>, IRecipient<Messages.SignInCompleted> {
    public event Action<Enumerations.Page, string?>? NavigationRequested;

    void IRecipient<Messages.SignInRequested>.Receive(Messages.SignInRequested _) {
        NavigationRequested?.Invoke(Enumerations.Page.SignIn, null);
    }

    void IRecipient<Messages.SignInCompleted>.Receive(Messages.SignInCompleted _) {
        NavigationRequested?.Invoke(Enumerations.Page.Home, null);
    }

    public MainWindow() {
        WeakReferenceMessenger.Default.RegisterAll(this);
    }
}
