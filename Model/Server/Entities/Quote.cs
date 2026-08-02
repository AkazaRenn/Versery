using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Quote/">Mastodon API Documentation</see>
public sealed class Quote {
    [JsonPropertyName("state")]
    public QuoteState State { get; set; } = QuoteState.Unauthorized;

    [JsonPropertyName("quoted_status")]
    public Status? QuotedStatus { get; set; } = null;

    /// <see href="https://docs.joinmastodon.org/entities/ShallowQuote/">Mastodon API Documentation - ShallowQuote</see>
    [JsonPropertyName("quoted_status_id")]
    public string? QuotedStatusId { get; set; } = null;
}
