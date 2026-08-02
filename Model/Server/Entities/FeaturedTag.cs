using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/FeaturedTag/">Mastodon API Documentation</see>
public sealed class FeaturedTag {
    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FeaturedTag/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FeaturedTag/#name"/>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FeaturedTag/#url"/>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FeaturedTag/#statuses_count"/>
    [JsonPropertyName("statuses_count")]
    public long StatusesCount { get; set; } = 0;

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FeaturedTag/#last_status_at"/>
    [JsonPropertyName("last_status_at")]
    public DateTime? LastStatusAt { get; set; } = null;
}
