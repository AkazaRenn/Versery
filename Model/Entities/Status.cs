using LiteDB;
using Model.Server.Entities.Enumerations;

namespace Model.Entities;

public record class Status() {
    [BsonId]
    public string Id { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    public string? ReblogId { get; set; } = null;
    public Uri? Uri { get; set; } = null;
    public string Content { get; set; } = string.Empty;
    public StatusVisibility Visibility { get; set; } = StatusVisibility.Public;
    public long RepliesCount { get; set; } = 0;
    public bool Favourited { get; set; } = false;
    public bool Reblogged { get; set; } = false;
    public bool Muted { get; set; } = false;
    public bool Bookmarked { get; set; } = false;
    public bool Pinned { get; set; } = false;
    public Dictionary<string, Uri> Emojis { get; set; } = [];
    public string? RepliedStatusId { get; set; } = null;
    public string? RepliedAccountId { get; set; } = null;

    internal Status(Model.Server.Entities.Status serverStatus) : this() {
        Id = serverStatus.Id;
        AccountId = serverStatus.Account.Id;
        CreatedAt = serverStatus.CreatedAt;
        if (serverStatus.Reblog != null) {
            ReblogId = serverStatus.Reblog.Id;
        } else {
            Content = serverStatus.Content;
            RepliesCount = serverStatus.RepliesCount;
            Favourited = serverStatus.Favourited ?? false;
            Reblogged = serverStatus.Reblogged ?? false;
            Muted = serverStatus.Muted ?? false;
            Bookmarked = serverStatus.Bookmarked ?? false;
            Pinned = serverStatus.Pinned ?? false;
            Uri = serverStatus.Uri;
            foreach (var emoji in serverStatus.Emojis) {
                if (emoji.Url is not null) {
                    Emojis[emoji.Shortcode] = emoji.Url;
                }
            }
        }
    }

    internal static IEnumerable<Status> FromServer(IEnumerable<Model.Server.Entities.Status> serverStatuses) {
        foreach (var serverStatus in serverStatuses) {
            yield return new Status(serverStatus);
        }
    }
}
