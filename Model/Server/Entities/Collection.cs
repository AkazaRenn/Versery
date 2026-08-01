using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a curated collection of accounts that a user recommends others to follow.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Collection/">Mastodon API Documentation</see>
public class Collection {
    /// <summary>
    /// The collection id.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The id of the account that curates this Collection.
    /// </summary>
    [JsonPropertyName("account_id")]
    public string AccountId { get; set; } = string.Empty;

    /// <summary>
    /// The Collection’s ActivityPub identifier (used for federation).
    /// </summary>
    [JsonPropertyName("uri")]
    public Uri? Uri { get; set; } = null;

    /// <summary>
    /// The url of the Collection’s HTML page (web interface URL).
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// The name of the Collection.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// An optional description of the Collection.
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; } = null;

    /// <summary>
    /// Primary language of this Collection.
    /// </summary>
    [JsonPropertyName("language")]
    public CultureInfo? Language { get; set; } = null;

    /// <summary>
    /// Whether the Collection was created on this server or resides on a remote server.
    /// </summary>
    [JsonPropertyName("local")]
    public bool Local { get; set; } = false;

    /// <summary>
    /// Whether the Collection has been marked as including sensitive content.
    /// </summary>
    [JsonPropertyName("sensitive")]
    public bool Sensitive { get; set; } = false;

    /// <summary>
    /// Whether the Collection should show up on the owner’s profile, in search results and recommendations
    /// </summary>
    [JsonPropertyName("discoverable")]
    public bool Discoverable { get; set; } = false;

    /// <summary>
    /// A single hashtag that describes this Collection.
    /// </summary>
    [JsonPropertyName("tag")]
    public ShallowTag? Tag { get; set; } = null;
}