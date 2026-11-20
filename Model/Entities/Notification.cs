using Model.Interfaces;

namespace Model.Entities;

public record Notification(): IPaginatedEntity, IDatabaseEntity {
    public string Id { get; init; } = string.Empty;
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.MinValue;
    public bool FollowedByGap { get; internal set; } = false;
    public DateTimeOffset ExpiresAt { get; init; } = DateTimeOffset.UtcNow.AddMonths(3);

    internal Notification(Server.Entities.Status serverStatus) : this() {
        Id = serverStatus.Id;
        CreatedAt = serverStatus.CreatedAt;
    }
}
