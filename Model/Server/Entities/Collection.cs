using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Collection/">Mastodon API Documentation</see>
public sealed class Collection {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#account_id"/>
    [JsonPropertyName("account_id")]
    public string AccountId { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#uri"/>
    [JsonPropertyName("uri")]
    public Uri? Uri { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#url"/>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#name"/>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#description"/>
    [JsonPropertyName("description")]
    public string? Description { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#language"/>
    [JsonPropertyName("language")]
    public CultureInfo? Language { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#local"/>
    [JsonPropertyName("local")]
    public bool Local { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#sensitive"/>
    [JsonPropertyName("sensitive")]
    public bool Sensitive { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#discoverable"/>
    [JsonPropertyName("discoverable")]
    public bool Discoverable { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#tag"/>
    [JsonPropertyName("tag")]
    public ShallowTag? Tag { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#updated_at"/>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = default;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#item_count"/>
    [JsonPropertyName("item_count")]
    public int ItemCount { get; set; } = 0;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#items"/>
    [JsonPropertyName("items")]
    public List<CollectionItem> Items { get; set; } = [];
}

/// <see href="https://docs.joinmastodon.org/entities/Collection/#WrappedCollection">Mastodon API Documentation</see>
public sealed class WrappedCollection {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#collection"/>
    [JsonPropertyName("collection")]
    public Collection Collection { get; set; } = new();
}

/// <see href="https://docs.joinmastodon.org/entities/Collection/#Collections">Mastodon API Documentation</see>
public sealed class Collections {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Collection/#collections"/>
    [JsonPropertyName("collections")]
    public List<Collection> Items { get; set; } = [];
}