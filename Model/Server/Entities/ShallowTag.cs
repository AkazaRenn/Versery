using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Minimal representation of a hashtag.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/ShallowTag/">Mastodon API Documentation</see>
public class ShallowTag {
    /// <summary>
    /// The value of the hashtag after the # sign.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A link to the hashtag on the local server.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;
}