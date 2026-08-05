using Raiqub.Generators.EnumUtilities;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Status/">Mastodon API Documentation</see>
public sealed class Status {
    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#uri"/>
    [JsonPropertyName("uri")]
    public Uri? Uri { get; set; } = null;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#account"/>
    [JsonPropertyName("account")]
    public Account Account { get; set; } = new();

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#content"/>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Version: 0.9.9
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#visibility"/>
    [JsonPropertyName("visibility")]
    public StatusVisibility Visibility { get; set; } = StatusVisibility.Public;

    /// <summary>
    /// Version: 0.9.9
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#sensitive"/>
    [JsonPropertyName("sensitive")]
    public bool Sensitive { get; set; } = false;

    /// <summary>
    /// Version: 1.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#spoiler_text"/>
    [JsonPropertyName("spoiler_text")]
    public string SpoilerText { get; set; } = string.Empty;

    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#media_attachments"/>
    [JsonPropertyName("media_attachments")]
    public List<MediaAttachment> MediaAttachments { get; set; } = [];

    /// <summary>
    /// Version: 0.9.9
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#application"/>
    [JsonPropertyName("application")]
    public Application? Application { get; set; } = null;

    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#mentions"/>
    [JsonPropertyName("mentions")]
    public List<Mention> Mentions { get; set; } = [];

    public sealed class Mention {
        /// <summary>
        /// Version: 0.6.0
        /// </summary>
        /// <seealso href="https://docs.joinmastodon.org/entities/Status/#Mention-id"/>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Version: 0.6.0
        /// </summary>
        /// <seealso href="https://docs.joinmastodon.org/entities/Status/#Mention-username"/>
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Version: 0.6.0
        /// </summary>
        /// <seealso href="https://docs.joinmastodon.org/entities/Status/#Mention-url"/>
        [JsonPropertyName("url")]
        public Uri? Url { get; set; } = null;

        /// <summary>
        /// Version: 0.6.0
        /// </summary>
        /// <seealso href="https://docs.joinmastodon.org/entities/Status/#Mention-acct"/>
        [JsonPropertyName("acct")]
        public string Acct { get; set; } = string.Empty;
    }

    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#tags"/>
    [JsonPropertyName("tags")]
    public List<ShallowTag> Tags { get; set; } = [];

    /// <summary>
    /// Version: 2.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#emojis"/>
    [JsonPropertyName("emojis")]
    public List<CustomEmoji> Emojis { get; set; } = [];

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#reblogs_count"/>
    [JsonPropertyName("reblogs_count")]
    public int ReblogsCount { get; set; } = 0;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#favorites_count"/>
    [JsonPropertyName("favourites_count")]
    public int FavouritesCount { get; set; } = 0;

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#quotes_count"/>
    [JsonPropertyName("quotes_count")]
    public int QuotesCount { get; set; } = 0;

    /// <summary>
    /// Version: 2.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#replies_count"/>
    [JsonPropertyName("replies_count")]
    public int RepliesCount { get; set; } = 0;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#url"/>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#in_reply_to_id"/>
    [JsonPropertyName("in_reply_to_id")]
    public string? InReplyToId { get; set; } = null;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#in_reply_to_account_id"/>
    [JsonPropertyName("in_reply_to_account_id")]
    public string? InReplyToAccountId { get; set; } = null;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#reblog"/>
    [JsonPropertyName("reblog")]
    public Status? Reblog { get; set; } = null;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#poll"/>
    [JsonPropertyName("poll")]
    public Poll? Poll { get; set; } = null;

    /// <summary>
    /// Version: 2.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#card"/>
    [JsonPropertyName("card")]
    public PreviewCard? Card { get; set; } = null;

    /// <summary>
    /// Version: 1.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#language"/>
    [JsonPropertyName("language")]
    // Should be CultureInfo? but not supported by System.Text.Json
    public string? Language { get; set; } = null;

    /// <summary>
    /// Version: 2.9.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#text"/>
    [JsonPropertyName("text")]
    public string? Text { get; set; } = null;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#edited_at"/>
    [JsonPropertyName("edited_at")]
    public DateTime? EditedAt { get; set; } = null;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#quote"/>
    [JsonPropertyName("quote")]
    public Quote? Quote { get; set; } = null;

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#quote_approval"/>
    [JsonPropertyName("quote_approval")]
    public QuoteApproval QuoteApproval { get; set; } = new();

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#favourited"/>
    [JsonPropertyName("favourited")]
    public bool? Favourited { get; set; } = null;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#reblogged"/>
    [JsonPropertyName("reblogged")]
    public bool? Reblogged { get; set; } = null;

    /// <summary>
    /// Version: 1.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#muted"/>
    [JsonPropertyName("muted")]
    public bool? Muted { get; set; } = null;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#bookmarked"/>
    [JsonPropertyName("bookmarked")]
    public bool? Bookmarked { get; set; } = null;

    /// <summary>
    /// Version: 1.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#pinned"/>
    [JsonPropertyName("pinned")]
    public bool? Pinned { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#filtered"/>
    [JsonPropertyName("filtered")]
    public List<FilterResult>? Filtered { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Status/#tagged_collections"/>
    [JsonPropertyName("tagged_collections")]
    public List<Collection> TaggedCollections { get; set; } = [];
}

[JsonConverterGenerator]
public enum StatusVisibility {
    [JsonStringEnumMemberName("public")]
    Public,

    [JsonStringEnumMemberName("unlisted")]
    Unlisted,

    [JsonStringEnumMemberName("private")]
    Private,

    [JsonStringEnumMemberName("direct")]
    Direct
}
