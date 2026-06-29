using LiteDB;

namespace Model.Entities;
public record class Status {
    [BsonId]
    public string Id { get; set; }
    public string AccountId { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Uri { get; set; }
    public string Content { get; set; }
    public bool Favourited { get; set; }
    public bool Reblogged { get; set; }
    public bool Muted { get; set; }
    public bool Bookmarked { get; set; }
    public bool Pinned { get; set; }
    public bool FollowedByGap { get; set; }

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
