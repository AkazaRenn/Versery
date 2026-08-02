using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/CollectionItem/">Mastodon API Documentation</see>
public sealed class CollectionItem {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/CollectionItem/#id">Mastodon API Documentation</see>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/CollectionItem/#account_id">Mastodon API Documentation</see>
    [JsonPropertyName("account_id")]
    public string? AccountId { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/CollectionItem/#state">Mastodon API Documentation</see>
    [JsonPropertyName("state")]
    public CollectionItemState State { get; set; } = default;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/CollectionItem/#created_at">Mastodon API Documentation</see>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
}

/// <summary>
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/CollectionItem/#state">Mastodon API Documentation</see>
public enum CollectionItemState {
    [JsonStringEnumMemberName("pending")]
    Pending,

    [JsonStringEnumMemberName("accepted")]
    Accepted,

    [JsonStringEnumMemberName("rejected")]
    Rejected,

    [JsonStringEnumMemberName("revoked")]
    Revoked,
}

/// <see href="https://docs.joinmastodon.org/entities/CollectionItem/#WrappedCollectionItem">Mastodon API Documentation</see>
public sealed class WrappedCollectionItem {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/CollectionItem/#collection_item">Mastodon API Documentation</see>
    [JsonPropertyName("collection_item")]
    public CollectionItem CollectionItem { get; set; } = new();
}