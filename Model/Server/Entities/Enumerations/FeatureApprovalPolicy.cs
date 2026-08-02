using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;

/// <see href="https://docs.joinmastodon.org/entities/FeatureApproval/#automatic">Mastodon API Documentation</see>
/// <see href="https://docs.joinmastodon.org/entities/FeatureApproval/#manual">Mastodon API Documentation</see>
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
