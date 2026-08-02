using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <see href="https://docs.joinmastodon.org/entities/AccountWarning/#action">Mastodon API Documentation</see>
public enum AccountWarningAction {
    [JsonStringEnumMemberName("none")]
    None,

    [JsonStringEnumMemberName("disable")]
    Disable,

    [JsonStringEnumMemberName("mark_statuses_as_sensitive")]
    MarkStatusesAsSensitive,

    [JsonStringEnumMemberName("delete_statuses")]
    DeleteStatuses,

    [JsonStringEnumMemberName("sensitive")]
    Sensitive,

    [JsonStringEnumMemberName("silence")]
    Silence,

    [JsonStringEnumMemberName("suspend")]
    Suspend,
}