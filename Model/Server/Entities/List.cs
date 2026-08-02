using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a list of some users that the authenticated user follows.
/// Version: 4.2.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/List/">Mastodon API Documentation</see>
public sealed class List {
    /// <summary>
    /// The ID of the list.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The user-defined title of the list.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Which replies should be shown in the list.
    /// </summary>
    [JsonPropertyName("replies_policy")]
    public ListRepliesPolicy RepliesPolicy { get; set; } = default;

    /// <summary>
    /// Whether members of the list should be removed from the “Home” feed.
    /// </summary>
    [JsonPropertyName("exclusive")]
    public bool Exclusive { get; set; } = false;
}