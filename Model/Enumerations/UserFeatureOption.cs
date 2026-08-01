using System.Text.Json.Serialization;

namespace Model.Enumerations;

/// <summary>
/// Describes how this account’s feature approval policy applies to the current user.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/FeatureApproval/#current_user">Mastodon API Documentation</see>
public enum UserFeatureOption {
    /// <summary>
    /// The requesting user is expected to be allowed to feature this account in a Collection and have this accepted automatically.
    /// </summary>
    [JsonStringEnumMemberName("automatic")]
    Automatic,

    /// <summary>
    /// The requesting user is expected to be allowed to feature this account in a Collection but it has to be reviewed and accepted manually.
    /// </summary>
    [JsonStringEnumMemberName("manual")]
    Manual,

    /// <summary>
    /// The requesting user is not expected to be allowed to feature this account in a Collection. Mastodon will return an error if you attempt to do so.
    /// </summary>
    [JsonStringEnumMemberName("denied")]
    Denied,

    /// <summary>
    /// The user is not covered by the policies supported by Mastodon, and there are additional underlying policies that are unsupported by Mastodon. This should be treated as denied.
    /// </summary>
    [JsonStringEnumMemberName("unknown")]
    Unknown,

    /// <summary>
    /// The user has no policy. This is most likely the case when it is a remote user on a server that does not support feature approval policies, e.g. using software other than Mastodon or older versions of Mastodon. This should be treated as denied.
    /// </summary>
    [JsonStringEnumMemberName("missing")]
    Missing,
}
