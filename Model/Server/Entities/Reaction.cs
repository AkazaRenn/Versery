using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents an emoji reaction to an Announcement.
/// Version: 3.1.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Reaction/">Mastodon API Documentation</see>
public sealed class Reaction {
    /// <summary>
    /// The emoji used for the reaction. Either a unicode emoji, or a custom emoji's shortcode.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The total number of users who have added this reaction.
    /// </summary>
    [JsonPropertyName("count")]
    public int Count { get; set; } = 0;

    /// <summary>
    /// Whether the authorized user has added this reaction to the announcement.
    /// </summary>
    [JsonPropertyName("me")]
    public bool Me { get; set; } = false;

    /// <summary>
    /// A link to the custom emoji.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// A link to a non-animated version of the custom emoji.
    /// </summary>
    [JsonPropertyName("static_url")]
    public Uri? StaticUrl { get; set; } = null;
}