using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents the last read position within a user's timelines.
/// Version: 3.0.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Marker/">Mastodon API Documentation</see>
public class Marker {
    /// <summary>
    /// The ID of the most recently viewed entity.
    /// </summary>
    [JsonPropertyName("last_read_id")]
    public string LastReadId { get; set; } = string.Empty;

    /// <summary>
    /// Used for locking to prevent write conflicts.
    /// </summary>
    [JsonPropertyName("version")]
    public int Version { get; set; } = 0;

    /// <summary>
    /// The timestamp of when the marker was set.
    /// </summary>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.MinValue;
}
