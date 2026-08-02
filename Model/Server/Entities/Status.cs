using Model.Server.Entities.Enumerations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a status posted by an account.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Status/">Mastodon API Documentation</see>
public sealed class Status {
    /// <summary>
    /// The ID of the status in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// URI of the status used for federation.
    /// </summary>
    [JsonPropertyName("uri")]
    public Uri? Uri { get; set; } = null;

    /// <summary>
    /// The date when this status was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    /// <summary>
    /// The account that authored this status.
    /// </summary>
    [JsonPropertyName("account")]
    public Account Account { get; set; } = new Account();

    /// <summary>
    /// HTML-encoded status content.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Visibility of this status.
    /// </summary>
    [JsonPropertyName("visibility")]
    public StatusVisibility Visibility { get; set; } = StatusVisibility.Public;

    /// <summary>
    /// Is this status marked as sensitive content?
    /// </summary>
    [JsonPropertyName("sensitive")]
    public bool Sensitive { get; set; } = false;

    /// <summary>
    /// Subject or summary line, below which status content is collapsed until expanded.
    /// </summary>
    [JsonPropertyName("spoiler_text")]
    public string SpoilerText { get; set; } = string.Empty;

    /// <summary>
    /// Media that is attached to this status.
    /// </summary>
    [JsonPropertyName("media_attachments")]
    public IEnumerable<MediaAttachment> MediaAttachments { get; set; } = [];

    /// <summary>
    /// The application used to post this status.
    /// </summary>
    [JsonPropertyName("application")]
    public Application? Application { get; set; } = null;

    /// <summary>
    /// Mentions of users within the status content.
    /// </summary>
    [JsonPropertyName("mentions")]
    public IEnumerable<Mention> Mentions { get; set; } = [];

    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Status/#Mention">Mastodon API Documentation</see>
    public sealed class Mention {
        /// <summary>
        /// The account ID of the mentioned user.
        /// </summary>
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The username of the mentioned user.
        /// </summary>
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// The location of the mentioned user’s profile.
        /// </summary>
        [JsonPropertyName("url")]
        public Uri? Url { get; set; } = null;

        /// <summary>
        /// The webfinger acct: URI of the mentioned user. Equivalent to username for local users, or username@domain for remote users.
        /// </summary>
        [JsonPropertyName("acct")]
        public string Acct { get; set; } = string.Empty;
    }

    /// <summary>
    /// Hashtags used within the status content.
    /// </summary>
    [JsonPropertyName("tags")]
    public IEnumerable<ShallowTag> Tags { get; set; } = [];

    /// <summary>
    /// Custom emoji to be used when rendering status content.
    /// </summary>
    [JsonPropertyName("emojis")]
    public IEnumerable<CustomEmoji> Emojis { get; set; } = [];

    /// <summary>
    /// How many boosts this status has received.
    /// </summary>
    [JsonPropertyName("reblogs_count")]
    public int ReblogsCount { get; set; } = 0;

    /// <summary>
    /// How many favourites this status has received.
    /// </summary>
    [JsonPropertyName("favourites_count")]
    public int FavouritesCount { get; set; } = 0;

    /// <summary>
    /// How many accepted quotes this status has.
    /// </summary>
    [JsonPropertyName("quotes_count")]
    public int QuotesCount { get; set; } = 0;

    /// <summary>
    /// How many replies this status has received.
    /// </summary>
    [JsonPropertyName("replies_count")]
    public int RepliesCount { get; set; } = 0;

    /// <summary>
    /// A link to the status’s HTML representation.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// ID of the status being replied to.
    /// </summary>
    [JsonPropertyName("in_reply_to_id")]
    public string? InReplyToId { get; set; } = null;

    /// <summary>
    /// Might be the ID of the account that authored the status being replied to. This sometimes skips over self-replies.
    /// If status A was posted by account 1, and account 2 posts statuses B, C, and D as a chain of replies to status A,
    /// statuses B, C, and D will all have in_reply_to_account_id = 1 (instead of C and D having in_reply_to_account_id = 2).
    /// However, if status A was posted by account 1, and account 1 posts status B as a direct reply to A, B will have
    /// in_reply_to_account_id = 1 (instead of null).
    /// </summary>
    [JsonPropertyName("in_reply_to_account_id")]
    public string? InReplyToAccountId { get; set; } = null;

    /// <summary>
    /// The status being reblogged.
    /// </summary>
    [JsonPropertyName("reblog")]
    public Status? Reblog { get; set; } = null;

    /// <summary>
    /// The poll attached to the status.
    /// </summary>
    [JsonPropertyName("poll")]
    public Poll? Poll { get; set; } = null;

    /// <summary>
    /// Preview card for links included within status content.
    /// </summary>
    [JsonPropertyName("card")]
    public PreviewCard? Card { get; set; } = null;

    /// <summary>
    /// Primary language of this status.
    /// </summary>
    [JsonPropertyName("language")]
    public CultureInfo? Language { get; set; } = null;

    /// <summary>
    /// Plain-text source of a status. Returned instead of content when status is deleted, so the user may redraft from the source text without the client having to reverse-engineer the original text from the HTML content.
    /// </summary>
    [JsonPropertyName("text")]
    public string? Text { get; set; } = null;

    /// <summary>
    /// Timestamp of when the status was last edited.
    /// </summary>
    [JsonPropertyName("edited_at")]
    public DateTime? EditedAt { get; set; } = null;

    /// <summary>
    /// Information about the status being quoted, if any
    /// </summary>
    [JsonPropertyName("quote")]
    public Quote? Quote { get; set; } = null;

    /// <summary>
    /// Summary of the post quote’s approval policy and how it applies to the user making the
    /// request, that is, whether the user can be expected to be allowed to quote that post
    /// </summary>
    [JsonPropertyName("quote_approval")]
    public QuoteApproval QuoteApproval { get; set; } = new();

    /// <summary>
    /// If the current token has an authorized user: Have you favourited this status?
    /// </summary>
    [JsonPropertyName("favourited")]
    public bool? Favourited { get; set; } = null;

    /// <summary>
    /// If the current token has an authorized user: Have you boosted this status?
    /// </summary>
    [JsonPropertyName("reblogged")]
    public bool? Reblogged { get; set; } = null;

    /// <summary>
    /// If the current token has an authorized user: Have you muted notifications for this status’s conversation?
    /// </summary>
    [JsonPropertyName("muted")]
    public bool? Muted { get; set; } = null;

    /// <summary>
    /// If the current token has an authorized user: Have you bookmarked this status?
    /// </summary>
    [JsonPropertyName("bookmarked")]
    public bool? Bookmarked { get; set; } = null;

    /// <summary>
    /// If the current token has an authorized user: Have you pinned this status? Only appears if the status is pinnable.
    /// </summary>
    [JsonPropertyName("pinned")]
    public bool? Pinned { get; set; } = null;

    /// <summary>
    /// If the current token has an authorized user: The filter and keywords that matched this status.
    /// </summary>
    [JsonPropertyName("filtered")]
    public IEnumerable<FilterResult>? Filtered { get; set; } = null;

    /// <summary>
    /// If content includes links to Collections that the server recognized, this includes the actual Collections.
    /// </summary>
    [JsonPropertyName("tagged_collections")]
    public IEnumerable<Collection> TaggedCollections { get; set; } = [];
}
