using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <see href="https://docs.joinmastodon.org/entities/Report/#category">Mastodon API Documentation</see>
public enum ReportCategory {
    [JsonStringEnumMemberName("spam")]
    Spam,

    [JsonStringEnumMemberName("legal")]
    Legal,

    [JsonStringEnumMemberName("violation")]
    Violation,

    [JsonStringEnumMemberName("other")]
    Other,
}