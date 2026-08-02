using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <see href="https://docs.joinmastodon.org/entities/PreviewCard/#type">Mastodon API Documentation</see>
public enum PreviewCardType {
    [JsonStringEnumMemberName("link")]
    Link,

    [JsonStringEnumMemberName("photo")]
    Photo,

    [JsonStringEnumMemberName("video")]
    Video,

    [JsonStringEnumMemberName("rich")]
    Rich
}