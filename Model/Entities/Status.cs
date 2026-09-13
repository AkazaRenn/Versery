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

            foreach (var media in serverStatus.MediaAttachments) {
                switch (media.Type) {
                case MediaAttachmentType.Image:
                    Images.Add(new() {
                        Source = media.Url,
                        Preview = media.PreviewUrl,
                        Aspect = media.Meta?.Aspect ?? 1
                    });
                    break;
                case MediaAttachmentType.Video:
                    if (Video is null) {
                        Video = new() {
                            Source = media.Url,
                            Preview = media.PreviewUrl,
                            Aspect = media.Meta?.Aspect ?? 1
                        };
                    }
                    break;
                }
            }

            if (serverStatus.Card is not null) {
                Card = new() {
                    Url = serverStatus.Card.Url,
                    Title = serverStatus.Card.Title,
                    Description = serverStatus.Card.Description,
                    ProviderName = serverStatus.Card.ProviderName,
                    Image = serverStatus.Card.Image
                };
            }
        }
    }

    internal static IEnumerable<Status> FromServer(IEnumerable<Model.Server.Entities.Status> serverStatuses) {
        foreach (var serverStatus in serverStatuses) {
            yield return new Status(serverStatus);
        }
    }
}

public record Media {
    public Uri? Source { get; set; } = null;
    public Uri? Preview {  get; set; } = null;
    public double Aspect { get; set; } = 1;
}

public record Card {
    public Uri? Url { get; set; } = null;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ProviderName { get; set; } = string.Empty;
    public Uri? Image { get; set; } = null;
}
