using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <see href="https://docs.joinmastodon.org/entities/Filter/#filter_action">Mastodon API Documentation</see>
public enum FilterAction {
    [JsonStringEnumMemberName("warn")]
    Warn,

    [JsonStringEnumMemberName("hide")]
    Hide,

    [JsonStringEnumMemberName("blur")]
    Blur,
}