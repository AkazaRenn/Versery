using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <summary>
/// Version: 4.4.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Filter/#filter_action">Mastodon API Documentation</see>
public enum FilterAction {
    /// <summary>
    /// show a warning that identifies the matching filter by title, and allow the user to expand the filtered status. This is the default (and unknown values should be treated as equivalent to warn).
    /// </summary>
    [JsonStringEnumMemberName("warn")]
    Warn,

    /// <summary>
    /// do not show this status if it is received
    /// </summary>
    [JsonStringEnumMemberName("hide")]
    Hide,

    /// <summary>
    /// hide/blur media attachments with a warning identifying the matching filter by title
    /// </summary>
    [JsonStringEnumMemberName("blur")]
    Blur,
}