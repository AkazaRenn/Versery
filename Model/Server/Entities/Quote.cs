using Model.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents a quote of a status.
/// Version: 4.5.7
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Quote/">Mastodon API Documentation</see>
public class Quote {
    /// <summary>
    /// The state of the quote.
    /// </summary>
    [JsonPropertyName("state")]
    public QuoteState State { get; set; } = QuoteState.Unauthorized;

    /// <summary>
    /// The status that is being quoted.
    /// </summary>
    [JsonPropertyName("quoted_status")]
    public Status? QuotedStatus { get; set; } = null;

    /// <summary>
    /// The identifier of the status being quoted. This will be null, unless the state attribute is one of accepted, blocked_account, blocked_domain or muted_account, or the wrapping Status entity has been obtained by calling DELETE /api/v1/statuses/:id.
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/ShallowQuote/">Mastodon API Documentation - ShallowQuote</see>
    [JsonPropertyName("quoted_status_id")]
    public string? QuotedStatusId { get; set; } = null;
}
