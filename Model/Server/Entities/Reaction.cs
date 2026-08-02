using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Reaction/">Mastodon API Documentation</see>
public sealed class Reaction {
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("count")]
    public int Count { get; set; } = 0;

    [JsonPropertyName("me")]
    public bool Me { get; set; } = false;

    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    [JsonPropertyName("static_url")]
    public Uri? StaticUrl { get; set; } = null;
}