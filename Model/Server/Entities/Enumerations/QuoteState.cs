using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;

/// <see href="https://docs.joinmastodon.org/entities/Quote/#state">Mastodon API Documentation</see>
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
