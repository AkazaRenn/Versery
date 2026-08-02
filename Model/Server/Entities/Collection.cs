using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Collection/">Mastodon API Documentation</see>
public sealed class Collection {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#id">Mastodon API Documentation</see>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#account_id">Mastodon API Documentation</see>
    [JsonPropertyName("account_id")]
    public string AccountId { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#uri">Mastodon API Documentation</see>
    [JsonPropertyName("uri")]
    public Uri? Uri { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#url">Mastodon API Documentation</see>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#name">Mastodon API Documentation</see>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#description">Mastodon API Documentation</see>
    [JsonPropertyName("description")]
    public string? Description { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#language">Mastodon API Documentation</see>
    [JsonPropertyName("language")]
    public CultureInfo? Language { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#local">Mastodon API Documentation</see>
    [JsonPropertyName("local")]
    public bool Local { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#sensitive">Mastodon API Documentation</see>
    [JsonPropertyName("sensitive")]
    public bool Sensitive { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#discoverable">Mastodon API Documentation</see>
    [JsonPropertyName("discoverable")]
    public bool Discoverable { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#tag">Mastodon API Documentation</see>
    [JsonPropertyName("tag")]
    public ShallowTag? Tag { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#item_count">Mastodon API Documentation</see>
    [JsonPropertyName("item_count")]
    public int ItemCount { get; set; } = 0;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#items">Mastodon API Documentation</see>
    [JsonPropertyName("items")]
    public IEnumerable<CollectionItem> Items { get; set; } = [];

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#created_at">Mastodon API Documentation</see>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#updated_at">Mastodon API Documentation</see>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.MinValue;
}

/// <see href="https://docs.joinmastodon.org/entities/Collection/#WrappedCollection">Mastodon API Documentation</see>
public sealed class WrappedCollection {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#collection">Mastodon API Documentation</see>
    [JsonPropertyName("collection")]
    public Collection Collection { get; set; } = new();
}