using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;

/// <summary>
/// Represents the state of a quote.
/// Current version: 4.5.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Quote/#state">Mastodon API Documentation</see>
public enum QuoteState {
    /// <summary>
    /// The quote has not been acknowledged by the quoted account yet, and requires authorization before being displayed.
    /// </summary>
    [JsonStringEnumMemberName("pending")]
    Pending,

    /// <summary>
    /// The quote has been accepted and can be displayed. quoted_status is non-null.
    /// </summary>
    [JsonStringEnumMemberName("accepted")]
    Accepted,

    /// <summary>
    /// The quote has been explicitly rejected by the quoted account, and cannot be displayed.
    /// </summary>
    [JsonStringEnumMemberName("rejected")]
    Rejected,

    /// <summary>
    /// The quote has been previously accepted, but is now revoked, and thus cannot be displayed.
    /// </summary>
    [JsonStringEnumMemberName("revoked")]
    Revoked,

    /// <summary>
    /// The quote has been approved, but the quoted post itself has now been deleted.
    /// </summary>
    [JsonStringEnumMemberName("deleted")]
    Deleted,

    /// <summary>
    /// The quote has been approved, but cannot be displayed because the user is not authorized to see it.
    /// </summary>
    [JsonStringEnumMemberName("unauthorized")]
    Unauthorized,

    /// <summary>
    /// The quote has been approved, but should not be displayed because the user has blocked the account being quoted. quoted_status is non-null.
    /// </summary>
    [JsonStringEnumMemberName("blocked_account")]
    BlockedAccount,

    /// <summary>
    /// The quote has been approved, but should not be displayed because the user has blocked the domain of the account being quoted. quoted_status is non-null.
    /// </summary>
    [JsonStringEnumMemberName("blocked_domain")]
    BlockedDomain,

    /// <summary>
    /// The quote has been approved, but should not be displayed because the user has muted the the account being quoted. quoted_status is non-null.
    /// </summary>
    [JsonStringEnumMemberName("muted_account")]
    MutedAccount,
}
