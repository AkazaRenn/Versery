using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <summary>
/// Version: 2.9.1
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/MediaAttachment/#type">Mastodon API Documentation</see>
public enum MediaAttachmentType {
    /// <summary>
    /// unsupported or unrecognized file type
    /// </summary>
    [JsonStringEnumMemberName("unknown")]
    Unknown,

    /// <summary>
    /// Static image
    /// </summary>
    [JsonStringEnumMemberName("image")]
    Image,

    /// <summary>
    /// Looping, soundless animation
    /// </summary>
    [JsonStringEnumMemberName("gifv")]
    Gifv,

    /// <summary>
    /// Video clip
    /// </summary>
    [JsonStringEnumMemberName("video")]
    Video,

    /// <summary>
    /// Audio track
    /// </summary>
    [JsonStringEnumMemberName("audio")]
    Audio,
}