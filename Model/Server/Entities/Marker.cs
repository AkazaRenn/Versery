using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Marker/">Mastodon API Documentation</see>
public sealed class Marker {
    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Marker/#last_read_id"/>
    [JsonPropertyName("last_read_id")]
    public string LastReadId { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Marker/#version"/>
    [JsonPropertyName("version")]
    public int Version { get; set; } = 0;

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Marker/#updated_at"/>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = default;
}
