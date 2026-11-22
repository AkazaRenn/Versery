using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;
using ViewModel.Abstractions;
using ViewModel.Controls;

namespace ViewModel.Pages;

public sealed partial class Notifications: PaginatedViewModelCollection<Notification, Model.Entities.Notification>, IRecipient<Messages.SignInCompleted> {
    private readonly Client client = Model.Services.Get<Client>();

    public Notifications() {
        StrongReferenceMessenger.Default.Register<Messages.SignInCompleted>(this);
    }

    protected override Model.Entities.Notification[] GetFromDatabase(string? afterId = null) {
        if (!client.SignedIn) {
            return [];
        }
        return client.GetNotificationFromDatabase(afterId);
    }

    protected override async Task<Model.Entities.Notification[]> GetFromServer(string? afterId = null) {
        if (!client.SignedIn) {
            return [];
        }
        return await client.GetNotificationFromServer(afterId);
    }

    public void Receive(Messages.SignInCompleted message) {
        ViewModels.Clear();
        _ = LoadInitialItems();
    }
}
