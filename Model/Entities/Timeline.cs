using Model.Interfaces;

namespace Model.Entities;

public record Timeline(): IDatabaseEntity {
    public string Id { get; init; } = string.Empty;
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.MinValue;
    public DateTimeOffset ExpiresAt { get; init; } = DateTimeOffset.UtcNow.AddMonths(3);

    internal Timeline(Server.Entities.Status serverStatus) : this() {
        Id = serverStatus.Id;
        CreatedAt = serverStatus.CreatedAt;
    }
}
