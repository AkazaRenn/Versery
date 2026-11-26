using Model.Interfaces;

namespace Model.Entities;

public record Timeline(): IDatabaseEntity {
    public string Id { get; init; } = string.Empty;
    public string? Reblog { get; init; } = null;
    public string? Reblogger { get; init; } = null;
    public DateTimeOffset CreatedAt { get; init; } = DateTimeOffset.MinValue;
    public DateTimeOffset ExpiresAt { get; init; } = DateTimeOffset.UtcNow.AddMonths(3);

    internal Timeline(Server.Entities.Status serverStatus) : this() {
        Id = serverStatus.Id;
        Reblog = serverStatus.Reblog?.Id;
        Reblogger = (serverStatus.Reblog is null) ? null : serverStatus.Account.Id;
        CreatedAt = serverStatus.CreatedAt;
    }
}
