using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <summary>
/// Version: 4.3.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Appeal/#state">Mastodon API Documentation</see>
public enum AppealState {
    /// <summary>
    /// The appeal has been approved by a moderator
    /// </summary>
    [JsonStringEnumMemberName("approved")]
    Approved,

    /// <summary>
    /// The appeal has been rejected by a moderator
    /// </summary>
    [JsonStringEnumMemberName("rejected")]
    Rejected,

    /// <summary>
    /// The appeal has been submitted, but neither approved nor rejected yet
    /// </summary>
    [JsonStringEnumMemberName("pending")]
    Pending,
}