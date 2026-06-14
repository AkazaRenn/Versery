using LiteDB;

namespace Model.Data.Entities;
public record class TimelineEntry {
    [BsonId]
    public UInt64 Id;
    [BsonField("z")]
    public bool FollowedByGap;
}
