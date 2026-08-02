using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <see href="https://docs.joinmastodon.org/entities/MediaAttachment/#type">Mastodon API Documentation</see>
public enum MediaAttachmentType {
    [JsonStringEnumMemberName("unknown")]
    Unknown,

    [JsonStringEnumMemberName("image")]
    Image,

    [JsonStringEnumMemberName("gifv")]
    Gifv,

    [JsonStringEnumMemberName("video")]
    Video,

    [JsonStringEnumMemberName("audio")]
    Audio,
}