using ViewModel.Abstractions;
using ViewModel.Controls;

namespace ViewModel.Pages;

public sealed partial class Notifications: PaginatedViewModelCollection<Notification, Model.Entities.Notification> {
    protected override Model.Entities.Notification[] GetFromDatabase(int count, string? afterId = null) {
        return client.GetNotificationFromDatabase(count, afterId);
    }

    protected override async Task<Model.Entities.Notification[]> GetFromServer(int count, string? afterId = null) {
        return await client.GetNotificationFromServer(count, afterId);
    }
}
