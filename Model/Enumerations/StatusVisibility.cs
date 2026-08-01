using System.Text.Json.Serialization;

namespace Model.Enumerations;

/// <summary>
/// Visibility of this status.
/// Version: 0.9.9
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Status/#visibility">Mastodon API Documentation</see>
public enum StatusVisibility {
    /// <summary>
    /// Visible to everyone, shown in public timelines.
    /// </summary>
    [JsonStringEnumMemberName("public")]
    Public,

    /// <summary>
    /// Visible to public, but not included in public timelines.
    /// </summary>
    [JsonStringEnumMemberName("unlisted")]
    Unlisted,

    /// <summary>
    /// Visible to followers only, and to any mentioned users.
    /// </summary>
    [JsonStringEnumMemberName("private")]
    Private,

    /// <summary>
    /// Visible only to mentioned users.
    /// </summary>
    [JsonStringEnumMemberName("direct")]
    Direct
}
