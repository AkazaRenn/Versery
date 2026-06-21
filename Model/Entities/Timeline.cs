using LiteDB;

namespace Model.Entities;
public record class Timeline {
    [BsonId]
    public UInt64 Id;
    [BsonField("z")]
    public bool FollowedByGap;
}
