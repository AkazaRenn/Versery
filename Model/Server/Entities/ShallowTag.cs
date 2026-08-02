using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/ShallowTag/">Mastodon API Documentation</see>
public class ShallowTag {
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;
}