using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents the approval state of a quote.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/FeatureApproval/">Mastodon API Documentation</see>
public sealed class FeatureApproval {
    /// <summary>
    /// Describes who is expected to be able to quote that status and have the quote automatically authorized.
    /// An empty list means that nobody is expected to be able to quote this post. Other values may be added in the future,
    /// so unknown values should be treated as unsupported_policy.
    /// </summary>
    [JsonPropertyName("automatic")]
    public IEnumerable<FeatureApprovalPolicy> Automatic { get; set; } = [];

    /// <summary>
    /// Describes who is expected to have their quotes of this status be manually reviewed by the author before being accepted.
    /// An empty list means that nobody is expected to be able to quote this post. Other values may be added in the future,
    /// so unknown values should be treated as unsupported_policy.
    /// </summary>
    [JsonPropertyName("manual")]
    public IEnumerable<FeatureApprovalPolicy> Manual { get; set; } = [];

    /// <summary>
    /// Describes how this status’ quote policy applies to the current user.
    /// </summary>
    [JsonPropertyName("current_user")]
    public UserFeatureOption CurrentUser { get; set; } = UserFeatureOption.Unknown;
}
