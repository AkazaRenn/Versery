using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/FilterKeyword/">Mastodon API Documentation</see>
public sealed class FilterKeyword {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("keyword")]
    public string Keyword { get; set; } = string.Empty;

    [JsonPropertyName("whole_word")]
    public bool WholeWord { get; set; } = false;
}