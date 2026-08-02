using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;

/// <see href="https://docs.joinmastodon.org/entities/Account/#source-quote_policy">Mastodon API Documentation</see>
public enum QuotePolicy {
    [JsonStringEnumMemberName("public")]
    Public,

    [JsonStringEnumMemberName("followers")]
    Followers,

    [JsonStringEnumMemberName("nobody")]
    Nobody,
}
