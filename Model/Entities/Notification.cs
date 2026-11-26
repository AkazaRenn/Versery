using Model.Interfaces;

namespace Model.Entities;

public record Notification(): IDatabaseEntity {
    public string Id { get; init; } = string.Empty;
    public string GroupKey { get; init; } = string.Empty;
    public string? PageMinId { get; init; } = null;
    public DateTimeOffset ExpiresAt { get; init; } = DateTimeOffset.UtcNow.AddMonths(3);

    internal Notification(Server.Entities.NotificationGroup serverNotificationGroup) : this() {
        Id = serverNotificationGroup.MostRecentNotificationId.ToString();
        GroupKey = serverNotificationGroup.GroupKey;
        PageMinId = serverNotificationGroup.PageMinId;
    }
}
