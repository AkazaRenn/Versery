using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/ShallowQuote/">Mastodon API Documentation</see>
public sealed class ShallowQuote {
    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ShallowQuote/#state"/>
    [JsonPropertyName("state")]
    public QuoteState State { get; set; } = QuoteState.Unauthorized;

    /// <summary>
    /// Version: 4.5.7
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ShallowQuote/#quoted_status_id"/>
    [JsonPropertyName("quoted_status_id")]
    public string? QuotedStatusId { get; set; } = null;
}
