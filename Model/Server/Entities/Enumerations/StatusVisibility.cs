using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;

/// <see href="https://docs.joinmastodon.org/entities/Status/#visibility">Mastodon API Documentation</see>
public enum StatusVisibility {
    [JsonStringEnumMemberName("public")]
    Public,

    [JsonStringEnumMemberName("unlisted")]
    Unlisted,

    [JsonStringEnumMemberName("private")]
    Private,

    [JsonStringEnumMemberName("direct")]
    Direct
}
