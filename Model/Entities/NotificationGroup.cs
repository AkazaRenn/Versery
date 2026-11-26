using Model.Interfaces;
using System.Text.Json.Serialization;

namespace Model.Entities;

public record NotificationGroup: Server.Entities.NotificationGroup, IDatabaseEntity {
    [JsonIgnore]
    public string Id => GroupKey;
    public DateTimeOffset ExpiresAt { get; init; } = DateTimeOffset.UtcNow.AddMonths(3);

    public NotificationGroup(Server.Entities.NotificationGroup serverNotificationGroup) : base(serverNotificationGroup) {
    }

    private static readonly Server.Entities.NotificationGroup empty = (Server.Entities.NotificationGroup)System.Runtime.CompilerServices.RuntimeHelpers.GetUninitializedObject(typeof(Server.Entities.NotificationGroup));
    [JsonConstructor]
    public NotificationGroup() : base(empty) {
    }
}
