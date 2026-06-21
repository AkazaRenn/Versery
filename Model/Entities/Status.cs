using LiteDB;

namespace Model.Entities;
public record class Status {
    [BsonId]
    public string Id;
    public string AccountId;
    public DateTime CreatedAt;
    public string Uri;
    public string Content;
    public bool Favourited;
    public bool Reblogged;
    public bool Muted;
    public bool Bookmarked;
    public bool Pinned;
    public bool FollowedByGap;

    internal Status(Mastonet.Entities.Status serverStatus) {
        Id = serverStatus.Id;
        AccountId = serverStatus.Account.Id;
        CreatedAt = serverStatus.CreatedAt;
        Uri = serverStatus.Uri;
        Content = serverStatus.Content;
        Favourited = serverStatus.Favourited ?? false;
        Reblogged = serverStatus.Reblogged ?? false;
        Muted = serverStatus.Muted ?? false;
        Bookmarked = serverStatus.Bookmarked ?? false;
        Pinned = serverStatus.Pinned ?? false;
        FollowedByGap = false;
    }
}
