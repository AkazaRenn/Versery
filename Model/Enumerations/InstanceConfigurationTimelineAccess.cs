using System.Text.Json.Serialization;

namespace Model.Enumerations;
/// <summary>
/// Version: 4.0.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Instance/#timeline_access">
public enum InstanceConfigurationTimelineAccess {
    /// <summary>
    /// Access to posts in the public feed is available to both visitors and logged-in users.
    /// </summary>
    [JsonStringEnumMemberName("public")]
    Public,

    /// <summary>
    /// Access to posts in the public feed requires authentication.
    /// </summary>
    [JsonStringEnumMemberName("authenticated")]
    Authenticated,

    /// <summary>
    /// Access to posts in the public feed is only possible for users with the “View live and topic feeds” permission.
    /// </summary>
    [JsonStringEnumMemberName("disabled")]
    Disabled,
}