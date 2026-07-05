using LiteDB;

namespace Model.Entities;
public record class Status() {
    [BsonId]
    public string Id { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    public string? ReblogId { get; set; } = null;
    public string Uri { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool? Favourited { get; set; } = null;
    public bool? Reblogged { get; set; } = null;
    public bool? Muted { get; set; } = null;
    public bool? Bookmarked { get; set; } = null;
    public bool? Pinned { get; set; } = null;
    public bool? FollowedByGap { get; set; } = null;

    internal Status(Mastonet.Entities.Status serverStatus): this() {
        Id = serverStatus.Id;
        AccountId = serverStatus.Account.Id;
        CreatedAt = serverStatus.CreatedAt;
        if (serverStatus.Reblog != null) {
            ReblogId = serverStatus.Reblog.Id;
        } else {
            Uri = serverStatus.Uri;
            Content = serverStatus.Content;
            Favourited = serverStatus.Favourited;
            Reblogged = serverStatus.Reblogged;
            Muted = serverStatus.Muted;
            Bookmarked = serverStatus.Bookmarked;
            Pinned = serverStatus.Pinned;
        }
    }
}
