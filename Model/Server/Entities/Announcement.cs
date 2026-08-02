using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Announcement/">Mastodon API Documentation</see>
public sealed class Announcement {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("starts_at")]
    public DateTime? StartsAt { get; set; } = null;

    [JsonPropertyName("ends_at")]
    public DateTime? EndsAt { get; set; } = null;

    [JsonPropertyName("all_day")]
    public bool AllDay { get; set; } = false;

    [JsonPropertyName("published_at")]
    public DateTime PublishedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("read")]
    public bool? Read { get; set; } = null;

    [JsonPropertyName("mentions")]
    public IEnumerable<AnnouncementAccount> Mentions { get; set; } = [];

    [JsonPropertyName("statuses")]
    public IEnumerable<Status> Statuses { get; set; } = [];

    [JsonPropertyName("tags")]
    public IEnumerable<ShallowTag> Tags { get; set; } = [];

    [JsonPropertyName("emojis")]
    public IEnumerable<CustomEmoji> Emojis { get; set; } = [];

    [JsonPropertyName("reactions")]
    public IEnumerable<Reaction> Reactions { get; set; } = [];
}

/// <see href="https://docs.joinmastodon.org/entities/Announcement/#Account">Mastodon API Documentation</see>
public sealed class AnnouncementAccount {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    [JsonPropertyName("acct")]
    public string Acct { get; set; } = string.Empty;
}

/// <see href="https://docs.joinmastodon.org/entities/Announcement/#Status">Mastodon API Documentation</see>
public sealed class AnnouncementStatus {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;
}
