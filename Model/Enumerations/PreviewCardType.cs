using System.Text.Json.Serialization;

namespace Model.Enumerations;
/// <summary>
/// Version: 1.3.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/PreviewCard/#type">Mastodon API Documentation</see>
public enum PreviewCardType {
    /// <summary>
    /// Link OEmbed
    /// </summary>
    [JsonStringEnumMemberName("link")]
    Link,

    /// <summary>
    /// Photo OEmbed
    /// </summary>
    [JsonStringEnumMemberName("photo")]
    Photo,

    /// <summary>
    /// Video OEmbed
    /// </summary>
    [JsonStringEnumMemberName("video")]
    Video,

    /// <summary>
    /// iframe OEmbed. Not currently accepted, so won't show up in practice.
    /// </summary>
    [JsonStringEnumMemberName("rich")]
    Rich
}