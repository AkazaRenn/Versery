using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Marker/">Mastodon API Documentation</see>
public sealed class Marker {
    [JsonPropertyName("last_read_id")]
    public string LastReadId { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    public int Version { get; set; } = 0;

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.MinValue;
}
