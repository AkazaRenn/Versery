using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <summary>
/// Version: 4.2.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Report/#category">Mastodon API Documentation</see>
public enum ReportCategory {
    /// <summary>
    /// Unwanted or repetitive content
    /// </summary>
    [JsonStringEnumMemberName("spam")]
    Spam,

    /// <summary>
    /// Illegal content
    /// </summary>
    [JsonStringEnumMemberName("legal")]
    Legal,

    /// <summary>
    /// A specific rule was violated
    /// </summary>
    [JsonStringEnumMemberName("violation")]
    Violation,

    /// <summary>
    /// Some other reason
    /// </summary>
    [JsonStringEnumMemberName("other")]
    Other,
}