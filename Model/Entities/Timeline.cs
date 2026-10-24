using Model.Interfaces;

namespace Model.Entities;

public record Timeline(): IPaginatedEntity {
    public string Id { get; set; } = String.Empty;
    public string GetId() {
        return Id;
    }
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    public bool FollowedByGap { get; set; } = false;
    public bool IsFollowedByGap() {
        return FollowedByGap;
    }

    internal Timeline(Model.Server.Entities.Status serverStatus) : this() {
        Id = serverStatus.Id;
        CreatedAt = serverStatus.CreatedAt.UtcDateTime;
    }
}
