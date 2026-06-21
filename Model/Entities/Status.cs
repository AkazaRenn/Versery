using LiteDB;

namespace Model.Entities;
public record class Status {
    [BsonId]
    public string Id;
    public DateTime CreatedAt;
    public bool Favourited;
    public bool Reblogged;
    public bool Muted;
    public bool Bookmarked;
    public bool Pinned;
    public bool FollowedByGap;

    internal Status(Mastonet.Entities.Status apiStatus) {
        Id = apiStatus.Id;
        CreatedAt = apiStatus.CreatedAt;
        Favourited = apiStatus.Favourited ?? false;
        Reblogged = apiStatus.Reblogged ?? false;
        Muted = apiStatus.Muted ?? false;
        Bookmarked = apiStatus.Bookmarked ?? false;
        Pinned = apiStatus.Pinned ?? false;
        FollowedByGap = false;
    }
}
