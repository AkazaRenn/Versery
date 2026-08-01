using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;

/// <summary>
/// The default quote policy to be used for new statuses.
/// Version: 4.5.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Account/#source-quote_policy">Mastodon API Documentation</see>
public enum QuotePolicy {
    /// <summary>
    /// Anyone (except blocked accounts) can quote.
    /// </summary>
    [JsonStringEnumMemberName("public")]
    Public,

    /// <summary>
    /// Only followers and author can quote.
    /// </summary>
    [JsonStringEnumMemberName("followers")]
    Followers,

    /// <summary>
    /// Only author can quote.
    /// </summary>
    [JsonStringEnumMemberName("nobody")]
    Nobody,
}
