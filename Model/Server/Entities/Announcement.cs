using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Announcement/">Mastodon API Documentation</see>
public sealed class Announcement {
    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#content"/>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#starts_at"/>
    [JsonPropertyName("strts_at")]
    public DateTime? StartsAt { get; set; } = null;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#ends_at"/>
    [JsonPropertyName("ends_at")]
    public DateTime? EndsAt { get; set; } = null;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#all_day"/>
    [JsonPropertyName("all_day")]
    public bool AllDay { get; set; } = false;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#created_at"/>
    [JsonPropertyName("published_at")]
    public DateTime PublishedAt { get; set; } = default;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#updated_at"/>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = default;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#read"/>
    [JsonPropertyName("read")]
    public bool? Read { get; set; } = null;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#mentions"/>
    [JsonPropertyName("mentions")]
    public List<AnnouncementAccount> Mentions { get; set; } = [];

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#statuses"/>
    [JsonPropertyName("statuses")]
    public List<AnnouncementStatus> Statuses { get; set; } = [];

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#tags"/>
    [JsonPropertyName("tags")]
    public List<ShallowTag> Tags { get; set; } = [];

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#emojis"/>
    [JsonPropertyName("emojis")]
    public List<CustomEmoji> Emojis { get; set; } = [];

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#reactions"/>
    [JsonPropertyName("reactions")]
    public List<Reaction> Reactions { get; set; } = [];
}

public sealed class AnnouncementAccount {
    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#Account-id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#Account-username"/>
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#Account-url"/>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#Account-acct"/>
    [JsonPropertyName("acct")]
    public string Acct { get; set; } = string.Empty;
}

public sealed class AnnouncementStatus {
    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#Status-id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Announcement/#Status-url"/>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;
}
