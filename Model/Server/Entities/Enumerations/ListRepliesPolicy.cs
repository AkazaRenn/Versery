using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <see href="https://docs.joinmastodon.org/entities/List/#replies_policy">Mastodon API Documentation</see>
public enum ListRepliesPolicy {
    [JsonStringEnumMemberName("followed")]
    Followed,

    [JsonStringEnumMemberName("list")]
    List,

    [JsonStringEnumMemberName("none")]
    None,
}
