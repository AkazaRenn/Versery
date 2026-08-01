using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a filter whose keywords matched a given status.
/// Version: 4.0.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/FilterResult/">Mastodon API Documentation</see>
public class FilterResult {
    /// <summary>
    /// The filter that was matched.
    /// </summary>
    [JsonPropertyName("filter")]
    public Filter Filter { get; set; } = new Filter();

    /// <summary>
    /// The keyword within the filter that was matched.
    /// </summary>
    [JsonPropertyName("keyword_matches")]
    public IEnumerable<string>? KeywordMatches { get; set; } = null;

    /// <summary>
    /// The status ID within the filter that was matched.
    /// </summary>
    [JsonPropertyName("status_matches")]
    public IEnumerable<string>? StatusMatches { get; set; } = null;
}