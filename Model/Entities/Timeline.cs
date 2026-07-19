using LiteDB;
using Windows.Media.Capture;

namespace Model.Entities;
public record class Timeline {
    [BsonId]
    public string Id { get; set; }
    public DateTime CreatedAt { get; set; }
    [BsonField("z")]
    public bool FollowedByGap { get; set; }
}
