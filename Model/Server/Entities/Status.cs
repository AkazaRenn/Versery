using Model.Server.Entities.Enumerations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Status/">Mastodon API Documentation</see>
public sealed class Status {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("uri")]
    public Uri? Uri { get; set; } = null;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("account")]
    public Account Account { get; set; } = new Account();

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("visibility")]
    public StatusVisibility Visibility { get; set; } = StatusVisibility.Public;

    [JsonPropertyName("sensitive")]
    public bool Sensitive { get; set; } = false;

    [JsonPropertyName("spoiler_text")]
    public string SpoilerText { get; set; } = string.Empty;

    [JsonPropertyName("media_attachments")]
    public IEnumerable<MediaAttachment> MediaAttachments { get; set; } = [];

    [JsonPropertyName("application")]
    public Application? Application { get; set; } = null;

    [JsonPropertyName("mentions")]
    public IEnumerable<Mention> Mentions { get; set; } = [];

    /// <see href="https://docs.joinmastodon.org/entities/Status/#Mention">Mastodon API Documentation</see>
    public sealed class Mention {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("url")]
        public Uri? Url { get; set; } = null;

        [JsonPropertyName("acct")]
        public string Acct { get; set; } = string.Empty;
    }

    [JsonPropertyName("tags")]
    public IEnumerable<ShallowTag> Tags { get; set; } = [];

    [JsonPropertyName("emojis")]
    public IEnumerable<CustomEmoji> Emojis { get; set; } = [];

    [JsonPropertyName("reblogs_count")]
    public int ReblogsCount { get; set; } = 0;

    [JsonPropertyName("favourites_count")]
    public int FavouritesCount { get; set; } = 0;

    [JsonPropertyName("quotes_count")]
    public int QuotesCount { get; set; } = 0;

    [JsonPropertyName("replies_count")]
    public int RepliesCount { get; set; } = 0;

    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    [JsonPropertyName("in_reply_to_id")]
    public string? InReplyToId { get; set; } = null;

    [JsonPropertyName("in_reply_to_account_id")]
    public string? InReplyToAccountId { get; set; } = null;

    [JsonPropertyName("reblog")]
    public Status? Reblog { get; set; } = null;

    [JsonPropertyName("poll")]
    public Poll? Poll { get; set; } = null;

    [JsonPropertyName("card")]
    public PreviewCard? Card { get; set; } = null;

    [JsonPropertyName("language")]
    public CultureInfo? Language { get; set; } = null;

    [JsonPropertyName("text")]
    public string? Text { get; set; } = null;

    [JsonPropertyName("edited_at")]
    public DateTime? EditedAt { get; set; } = null;

    [JsonPropertyName("quote")]
    public Quote? Quote { get; set; } = null;

    [JsonPropertyName("quote_approval")]
    public QuoteApproval QuoteApproval { get; set; } = new();

    [JsonPropertyName("favourited")]
    public bool? Favourited { get; set; } = null;

    [JsonPropertyName("reblogged")]
    public bool? Reblogged { get; set; } = null;

    [JsonPropertyName("muted")]
    public bool? Muted { get; set; } = null;

    [JsonPropertyName("bookmarked")]
    public bool? Bookmarked { get; set; } = null;

    [JsonPropertyName("pinned")]
    public bool? Pinned { get; set; } = null;

    [JsonPropertyName("filtered")]
    public IEnumerable<FilterResult>? Filtered { get; set; } = null;

    [JsonPropertyName("tagged_collections")]
    public IEnumerable<Collection> TaggedCollections { get; set; } = [];
}
