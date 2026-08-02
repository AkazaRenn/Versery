using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/List/">Mastodon API Documentation</see>
public sealed class List {
    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/List/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/List/#title"/>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/List/#replies_policy"/>
    [JsonPropertyName("replies_policy")]
    public ListRepliesPolicy RepliesPolicy { get; set; } = ListRepliesPolicy.None;

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/List/#exclusive"/>
    [JsonPropertyName("exclusive")]
    public bool Exclusive { get; set; } = false;
}



public enum ListRepliesPolicy {
    [JsonStringEnumMemberName("followed")]
    Followed,

    [JsonStringEnumMemberName("list")]
    List,

    [JsonStringEnumMemberName("none")]
    None,
}