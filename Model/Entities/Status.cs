using LiteDB;
using Model.Enumerations;

namespace Model.Entities;
public record class Status() {
    [BsonId]
    public string Id { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    public string? ReblogId { get; set; } = null;
    public Uri? Uri { get; set; } = null;
    public string Content { get; set; } = string.Empty;
    public Visibility Visibility { get; set; } = Visibility.Public;
    public long RepliesCount { get; set; } = 0;
    public bool? Favourited { get; set; } = null;
    public bool? Reblogged { get; set; } = null;
    public bool? Muted { get; set; } = null;
    public bool? Bookmarked { get; set; } = null;
    public bool? Pinned { get; set; } = null;
    public Dictionary<string, Uri> Emojis { get; set; } = [];
    public string? RepliedStatusId { get; set; } = null;
    public string? RepliedAccountId { get; set; } = null;

    internal Status(Mastonet.Entities.Status serverStatus): this() {
        Id = serverStatus.Id;
        AccountId = serverStatus.Account.Id;
        CreatedAt = serverStatus.CreatedAt;
        if (serverStatus.Reblog != null) {
            ReblogId = serverStatus.Reblog.Id;
        } else {
            Content = serverStatus.Content;
            RepliesCount = serverStatus.RepliesCount;
            Favourited = serverStatus.Favourited;
            Reblogged = serverStatus.Reblogged;
            Muted = serverStatus.Muted;
            Bookmarked = serverStatus.Bookmarked;
            Pinned = serverStatus.Pinned;
            if (Uri.TryCreate(serverStatus.Uri, UriKind.Absolute, out var uri)) {
                Uri = uri;
            }
            foreach (var emoji in serverStatus.Emojis) {
                if (Uri.TryCreate(emoji.Url, UriKind.Absolute, out var emojiUri)) {
                    Emojis[emoji.Shortcode] = emojiUri;
                }
            }
        }
    }

    internal static IEnumerable<Status> FromServer(IEnumerable<Mastonet.Entities.Status> serverStatuses) {
        foreach (var serverStatus in serverStatuses) {
            yield return new Status(serverStatus);
        }
    }
}
