using System.Text.Json.Serialization;

namespace Model.Enumerations;
/// <summary>
/// The contexts in which the filter should be applied.
/// Version: 4.0.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Filter/#context">Mastodon API Documentation</see>
public enum FilterContext {
    /// <summary>
    /// home timeline and lists
    /// </summary>
    [JsonStringEnumMemberName("home")]
    Home,

    /// <summary>
    /// notifications timeline
    /// </summary>
    [JsonStringEnumMemberName("notifications")]
    Notifications,

    /// <summary>
    /// public timelines
    /// </summary>
    [JsonStringEnumMemberName("public")]
    Public,

    /// <summary>
    /// expanded thread of a detailed status
    /// </summary>
    [JsonStringEnumMemberName("thread")]
    Thread,

    /// <summary>
    /// when viewing a profile
    /// </summary>
    [JsonStringEnumMemberName("account")]
    Account,
}