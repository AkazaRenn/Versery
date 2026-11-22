using Model.Interfaces;
using System.Text.Json.Serialization;

namespace Model.Entities;

public record Notification: Server.Entities.NotificationGroup, IPaginatedEntity {
    [JsonIgnore]
    public string Id => MostRecentNotificationId.ToString();
    public DateTimeOffset ExpiresAt { get; init; } = DateTimeOffset.UtcNow.AddMonths(3);
    public bool FollowedByGap { get; set; } = false;

    public Notification(Server.Entities.NotificationGroup serverNotificationGroup) : base(serverNotificationGroup) {
    }

    private static readonly Server.Entities.NotificationGroup empty = (Server.Entities.NotificationGroup)System.Runtime.CompilerServices.RuntimeHelpers.GetUninitializedObject(typeof(Server.Entities.NotificationGroup));
    [JsonConstructor]
    public Notification() : base(empty) {
    }
}
