using System.Text.Json.Serialization;

namespace Model.Server.Methods;
/// <see href="https://docs.joinmastodon.org/methods/directory/#query-parameters">Mastodon API Documentation</see>
public enum DirectoryOrder {
    /// <summary>
    /// order by most active
    /// </summary>
    [JsonStringEnumMemberName("active")]
    Active,

    /// <summary>
    /// order by newest
    /// </summary>
    [JsonStringEnumMemberName("new")]
    New,
}
