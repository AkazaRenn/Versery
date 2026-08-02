using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/FilterResult/">Mastodon API Documentation</see>
public sealed class FilterResult {
    [JsonPropertyName("filter")]
    public Filter Filter { get; set; } = new Filter();

    [JsonPropertyName("keyword_matches")]
    public IEnumerable<string>? KeywordMatches { get; set; } = null;

    [JsonPropertyName("status_matches")]
    public IEnumerable<string>? StatusMatches { get; set; } = null;
}