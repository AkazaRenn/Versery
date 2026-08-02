using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;

/// <see href="https://docs.joinmastodon.org/entities/FeatureApproval/#current_user">Mastodon API Documentation</see>
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
