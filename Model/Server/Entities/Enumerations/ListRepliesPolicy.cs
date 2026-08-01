using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <summary>
/// Version: 3.3.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/List/#replies_policy">Mastodon API Documentation</see>
public enum ListRepliesPolicy {
    /// <summary>
    /// Show replies to any followed user.
    /// </summary>
    [JsonStringEnumMemberName("followed")]
    Followed,

    /// <summary>
    /// Show replies to members of the list.
    /// </summary>
    [JsonStringEnumMemberName("list")]
    List,

    /// <summary>
    /// Do not show any replies.
    /// </summary>
    [JsonStringEnumMemberName("none")]
    None,
}
