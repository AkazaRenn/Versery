using CommunityToolkit.Mvvm.Messaging;
using Model;
using Model.Access;
using ViewModel.Abstractions;
using ViewModel.Controls;

namespace ViewModel.Pages;

public sealed partial class Notifications: PaginatedViewModelCollection<Notification, Model.Entities.Notification> {

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
}
