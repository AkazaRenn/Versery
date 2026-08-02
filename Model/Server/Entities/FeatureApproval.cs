using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/FeatureApproval/">Mastodon API Documentation</see>
public sealed class FeatureApproval {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FeatureApproval/#automatic"/>
    [JsonPropertyName("automatic")]
    public List<FeatureApprovalPolicy> Automatic { get; set; } = [];

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FeatureApproval/#manual"/>
    [JsonPropertyName("manual")]
    public List<FeatureApprovalPolicy> Manual { get; set; } = [];

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FeatureApproval/#current_user"/>
    [JsonPropertyName("current_user")]
    public UserFeatureOption CurrentUser { get; set; } = UserFeatureOption.Unknown;
}

public enum FeatureApprovalPolicy {
    [JsonStringEnumMemberName("public")]
    Public,

    [JsonStringEnumMemberName("followers")]
    Followers,

    [JsonStringEnumMemberName("following")]
    Following,

    [JsonStringEnumMemberName("unsupported_policy")]
    UnsupportedPolicy,
}

public enum UserFeatureOption {
    [JsonStringEnumMemberName("automatic")]
    Automatic,

    [JsonStringEnumMemberName("manual")]
    Manual,

    [JsonStringEnumMemberName("denied")]
    Denied,

    [JsonStringEnumMemberName("unknown")]
    Unknown,

    [JsonStringEnumMemberName("missing")]
    Missing,
}
