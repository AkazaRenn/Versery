using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/PreviewCardAuthor/">Mastodon API Documentation</see>
public sealed class PreviewCardAuthor {
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    [JsonPropertyName("account")]
    public Account? Account { get; set; } = null;
}