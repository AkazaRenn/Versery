using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents an announcement set by an administrator.
/// Version: 3.1.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Announcement/">Mastodon API Documentation</see>
public class Announcement {
    /// <summary>
    /// The ID of the announcement in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The text of the announcement.
    /// </summary>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// When the announcement will start.
    /// </summary>
    [JsonPropertyName("starts_at")]
    public DateTime? StartsAt { get; set; } = null;

    /// <summary>
    /// When the announcement will end.
    /// </summary>
    [JsonPropertyName("ends_at")]
    public DateTime? EndsAt { get; set; } = null;

    /// <summary>
    /// Whether the announcement should start and end on dates only instead of datetimes. Will be false if there is no starts_at or ends_at time.
    /// </summary>
    [JsonPropertyName("all_day")]
    public bool AllDay { get; set; } = false;

    /// <summary>
    /// When the announcement was published.
    /// </summary>
    [JsonPropertyName("published_at")]
    public DateTime PublishedAt { get; set; } = DateTime.MinValue;

    /// <summary>
    /// When the announcement was last updated.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.MinValue;

    /// <summary>
    /// Whether the announcement has been read by the current user.
    /// </summary>
    [JsonPropertyName("read")]
    public bool? Read { get; set; } = null;

    /// <summary>
    /// Accounts mentioned in the announcement text.
    /// </summary>
    [JsonPropertyName("mentions")]
    public IEnumerable<AnnouncementAccount> Mentions { get; set; } = [];

    /// <summary>
    /// Statuses linked in the announcement text.
    /// </summary>
    [JsonPropertyName("statuses")]
    public IEnumerable<Status> Statuses { get; set; } = [];

    /// <summary>
    /// Tags linked in the announcement text.
    /// </summary>
    [JsonPropertyName("tags")]
    public IEnumerable<ShallowTag> Tags { get; set; } = [];

    /// <summary>
    /// Custom emoji used in the announcement text.
    /// </summary>
    [JsonPropertyName("emojis")]
    public IEnumerable<CustomEmoji> Emojis { get; set; } = [];

    /// <summary>
    /// Emoji reactions attached to the announcement.
    /// </summary>
    [JsonPropertyName("reactions")]
    public IEnumerable<Reaction> Reactions { get; set; } = [];
}

/// <summary>
/// Version: 3.1.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Announcement/#Account">Mastodon API Documentation</see>
public class AnnouncementAccount {
    /// <summary>
    /// The account ID of the mentioned user.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The username of the mentioned user.
    /// </summary>
    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;

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
/// Version: 3.1.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Announcement/#Status">Mastodon API Documentation</see>
public class AnnouncementStatus {
    /// <summary>
    /// The ID of an attached Status in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The URL of an attached Status.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;
}
