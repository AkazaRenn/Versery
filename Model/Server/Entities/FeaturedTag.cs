using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents a hashtag that is featured on a profile.
/// Version: 3.3.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/FeaturedTag/">Mastodon API Documentation</see>
public class FeaturedTag {
    /// <summary>
    /// The ID of the featured tag.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The name of the hashtag being featured.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A link to all statuses by a user that contain this hashtag.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// The number of authored statuses containing this hashtag.
    /// </summary>
    [JsonPropertyName("statuses_count")]
    public long StatusesCount { get; set; } = 0;

    /// <summary>
    /// The timestamp of the last authored status containing this hashtag.
    /// </summary>
    [JsonPropertyName("last_status_at")]
    public DateTime? LastStatusAt { get; set; } = null;
}
