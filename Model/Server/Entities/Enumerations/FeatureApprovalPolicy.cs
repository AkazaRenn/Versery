using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;

/// <summary>
/// Represents the automatic quote approval policy of an account.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/FeatureApproval/#automatic">Mastodon API Documentation</see>
/// <see href="https://docs.joinmastodon.org/entities/FeatureApproval/#manual">Mastodon API Documentation</see>
public enum FeatureApprovalPolicy {
    /// <summary>
    /// Anybody is expected to be able to quote this status.
    /// </summary>
    [JsonStringEnumMemberName("public")]
    Public,

    /// <summary>
    /// Followers are expected to be able to quote this status.
    /// </summary>
    [JsonStringEnumMemberName("followers")]
    Followers,

    /// <summary>
    /// People followed by the author are expected to be able to quote this status.
    /// </summary>
    [JsonStringEnumMemberName("following")]
    Following,

    /// <summary>
    /// The underlying quote policy is not supported by Mastodon. Some accounts that do not fit in the above categories may be able to quote.
    /// </summary>
    [JsonStringEnumMemberName("unsupported_policy")]
    UnsupportedPolicy,
}
