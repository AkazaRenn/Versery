using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <summary>
/// Version: 4.4.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/AccountWarning/#action">Mastodon API Documentation</see>
public enum AccountWarningAction {
    /// <summary>
    /// No action was taken, this is a simple warning
    /// </summary>
    [JsonStringEnumMemberName("none")]
    None,

    /// <summary>
    /// The account has been disabled
    /// </summary>
    [JsonStringEnumMemberName("disable")]
    Disable,

    /// <summary>
    /// Specific posts from the target account have been marked as sensitive
    /// </summary>
    [JsonStringEnumMemberName("mark_statuses_as_sensitive")]
    MarkStatusesAsSensitive,

    /// <summary>
    /// Specific statuses from the target account have been deleted
    /// </summary>
    [JsonStringEnumMemberName("delete_statuses")]
    DeleteStatuses,

    /// <summary>
    /// All posts from the target account are marked as sensitive
    /// </summary>
    [JsonStringEnumMemberName("sensitive")]
    Sensitive,

    /// <summary>
    /// The target account has been limited
    /// </summary>
    [JsonStringEnumMemberName("silence")]
    Silence,

    /// <summary>
    /// The target account has been suspended
    /// </summary>
    [JsonStringEnumMemberName("suspend")]
    Suspend,
}