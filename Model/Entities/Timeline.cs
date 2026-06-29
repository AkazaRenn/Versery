using LiteDB;

namespace Model.Entities;
public record class Timeline {
    [BsonId]
    public required string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    [BsonField("z")]
    public bool FollowedByGap { get; set; }
}
