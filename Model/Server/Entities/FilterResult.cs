using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/FilterResult/">Mastodon API Documentation</see>
public sealed class FilterResult {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FilterResult/#filter"/>
    [JsonPropertyName("filter")]
    public Filter Filter { get; set; } = new();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FilterResult/#keyword_matches"/>
    [JsonPropertyName("keyword_matches")]
    public List<string>? KeywordMatches { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FilterResult/#status_matches"/>
    [JsonPropertyName("status_matches")]
    public List<string>? StatusMatches { get; set; } = null;
}