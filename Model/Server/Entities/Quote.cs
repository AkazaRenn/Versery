using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Quote/">Mastodon API Documentation</see>
public sealed class Quote {
    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Quote/#state"/>
    [JsonPropertyName("state")]
    public QuoteState State { get; set; } = QuoteState.Unauthorized;

    /// <summary>
    /// Version: 4.5.7
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Quote/#quoted_status"/>
    [JsonPropertyName("quoted_status")]
    public Status? QuotedStatus { get; set; } = null;
}

public enum QuoteState {
    [JsonStringEnumMemberName("pending")]
    Pending,

    [JsonStringEnumMemberName("accepted")]
    Accepted,

    [JsonStringEnumMemberName("rejected")]
    Rejected,

    [JsonStringEnumMemberName("revoked")]
    Revoked,

    [JsonStringEnumMemberName("deleted")]
    Deleted,

    [JsonStringEnumMemberName("unauthorized")]
    Unauthorized,

    [JsonStringEnumMemberName("blocked_account")]
    BlockedAccount,

    [JsonStringEnumMemberName("blocked_domain")]
    BlockedDomain,

    [JsonStringEnumMemberName("muted_account")]
    MutedAccount,
}
