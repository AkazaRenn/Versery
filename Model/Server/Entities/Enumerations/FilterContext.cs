using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <see href="https://docs.joinmastodon.org/entities/Filter/#context">Mastodon API Documentation</see>
public enum FilterContext {
    [JsonStringEnumMemberName("home")]
    Home,

    [JsonStringEnumMemberName("notifications")]
    Notifications,

    [JsonStringEnumMemberName("public")]
    Public,

    [JsonStringEnumMemberName("thread")]
    Thread,

    [JsonStringEnumMemberName("account")]
    Account,
}