using LiteDB;
using Model.Server.Entities;

namespace Model.Entities;

public record Status() {
    [BsonId]
    public string Id { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
    public string? ReblogId { get; set; } = null;
    public Uri? Uri { get; set; } = null;
    public string Content { get; set; } = string.Empty;
    public string? Text { get; set; } = string.Empty;
    public string SpoilerText { get; set; } = string.Empty;
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
    public string? QuotedStatusId { get; set; } = null;
    public bool Sensitive { get; set; } = false;
    public List<Media> Images { get; set; } = [];
    public Media? Video { get; set; } = null;
    public Card? Card { get; set; } = null;

    internal Status(Model.Server.Entities.Status serverStatus) : this() {
        Id = serverStatus.Id;
        AccountId = serverStatus.Account.Id;
        CreatedAt = serverStatus.CreatedAt;
        if (serverStatus.Reblog != null) {
            ReblogId = serverStatus.Reblog.Id;
        } else {
            Content = serverStatus.Content;
            Text = serverStatus.Text;
            SpoilerText = serverStatus.SpoilerText;
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
            RepliedStatusId = serverStatus.InReplyToId;
            RepliedAccountId = serverStatus.InReplyToAccountId;
            QuotedStatusId = serverStatus.Quote?.QuotedStatus?.Id;
            Sensitive = serverStatus.Sensitive;

            foreach (var media in serverStatus.MediaAttachments) {
                if (media.Url is null) {
                    continue;
                }
                switch (media.Type) {
                case MediaAttachmentType.Image:
                    Images.Add(new(media));
                    break;
                case MediaAttachmentType.Video:
                    Video ??= new(media);
                    break;
                }
            }

            if (serverStatus.Card is not null) {
                Card = new(serverStatus.Card);
            }
        }
    }

    internal static IEnumerable<Status> FromServer(IEnumerable<Model.Server.Entities.Status> serverStatuses) {
        foreach (var serverStatus in serverStatuses) {
            yield return new Status(serverStatus);
        }
    }
}

public record Media() {
    public Uri? Source { get; set; } = null;
    public Uri? Preview { get; set; } = null;
    public double AspectRatio { get; set; } = 1;
    public string? BlurHash { get; set; } = null;
    public string? Description { get; set; } = null;

    public Media(Server.Entities.MediaAttachment media) : this() {
        Source = media.Url;
        Preview = media.PreviewUrl;
        AspectRatio = media.Meta?.Aspect ?? media.Meta?.Original?.Aspect ?? 1;
        BlurHash = media.Blurhash;
        Description = media.Description;
    }
}

public record Card() {
    public Uri? Url { get; set; } = null;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ProviderName { get; set; } = string.Empty;
    public Uri? Image { get; set; } = null;

    public Card(Server.Entities.PreviewCard card) : this() {
        Url = card.Url;
        Title = card.Title;
        Description = card.Description;
        ProviderName = card.ProviderName;
        Image = card.Image;
    }
}
