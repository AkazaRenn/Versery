using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/List/">Mastodon API Documentation</see>
public sealed class List {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("replies_policy")]
    public ListRepliesPolicy RepliesPolicy { get; set; } = default;

    [JsonPropertyName("exclusive")]
    public bool Exclusive { get; set; } = false;
}