using LiteDB;
using Windows.Media.Capture;

namespace Model.Entities;
public record class Timeline {
    [BsonId]
    public string Id { get; set; } = String.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    [BsonField("z")]
    public bool FollowedByGap { get; set; } = false;
}
