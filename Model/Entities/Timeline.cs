using Model.Interfaces;

namespace Model.Entities;

public record Timeline(): IPaginatedEntity {
    public string Id { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    public bool FollowedByGap { get; set; } = false;

    internal Timeline(Model.Server.Entities.Status serverStatus) : this() {
        Id = serverStatus.Id;
        CreatedAt = serverStatus.CreatedAt.UtcDateTime;
    }
}
