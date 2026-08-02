using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/FeaturedTag/">Mastodon API Documentation</see>
public sealed class FeaturedTag {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    [JsonPropertyName("statuses_count")]
    public long StatusesCount { get; set; } = 0;

    [JsonPropertyName("last_status_at")]
    public DateTime? LastStatusAt { get; set; } = null;
}
