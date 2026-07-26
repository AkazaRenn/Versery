using LiteDB;

namespace Model.Entities;
public record class Emoji() {
    [BsonId]
    public string Id { get; set; } = String.Empty;
    public string Instance { get; set;  } = String.Empty;
    public string ShortCode { get; set; } = String.Empty;
    public Uri? Uri { get; set; }
    public Uri? StaticUri { get; set; }
    public bool VisibleInPicker { get; set; } = true;
    public string? Category { get; set; } = null;

    internal Emoji(Mastonet.Entities.Emoji serverEmoji, string instance): this() {
        Id = $"{instance}:{serverEmoji.Shortcode}";
        Instance = instance;
        ShortCode = serverEmoji.Shortcode;
        VisibleInPicker = serverEmoji.VisibleInPicker;
        Category = serverEmoji.Category;
        if (Uri.TryCreate(serverEmoji.Url, UriKind.Absolute, out var uri)) {
            Uri = uri;
        }
        if (Uri.TryCreate(serverEmoji.StaticUrl, UriKind.Absolute, out var staticUri)) {
            StaticUri = staticUri;
        }
    }
}
