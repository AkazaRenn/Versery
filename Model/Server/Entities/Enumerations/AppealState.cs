using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <see href="https://docs.joinmastodon.org/entities/Appeal/#state">Mastodon API Documentation</see>
public enum AppealState {
    [JsonStringEnumMemberName("approved")]
    Approved,

    [JsonStringEnumMemberName("rejected")]
    Rejected,

    [JsonStringEnumMemberName("pending")]
    Pending,
}