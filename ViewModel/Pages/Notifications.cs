using ViewModel.Abstractions;
using ViewModel.Controls;

namespace ViewModel.Pages;

public sealed partial class Notifications: PaginatedViewModelCollection<Notification, Model.Entities.Notification> {
    protected override Model.Entities.Notification[] GetFromDatabase(string? afterId = null) {
        return client.GetNotificationFromDatabase(afterId);
    }

    protected override async Task<Model.Entities.Notification[]> GetFromServer(string? afterId = null) {
        return await client.GetNotificationFromServer(afterId);
    }
}
