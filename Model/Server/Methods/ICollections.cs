using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/collections/">Mastodon API Documentation</see>
public interface ICollections {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/collections/#create">Mastodon API Documentation</see>
    [Post("/api/v1/collections")]
    Task<WrappedCollection> Create(
        [AliasAs("name")] string name,
        [AliasAs("description")] string? description = null,
        [AliasAs("language")] string? language = null,
        [AliasAs("tag_name")] string? tagName = null,
        [AliasAs("sensitive")] bool? sensitive = null,
        [AliasAs("discoverable")] bool? discoverable = null,
        [AliasAs("account_ids")][Query(CollectionFormat.Multi)] IEnumerable<string>? accountIds = null);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/collections/#get_collection">Mastodon API Documentation</see>
    [Get("/api/v1/collections/{id}")]
    Task<CollectionWithAccounts> GetCollection(string id);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/collections/#get_collections">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/{accountId}/collections")]
    Task<Collections> GetCollections(
        string accountId,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("offset")] int? offset = null);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/collections/#in_collections">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/{accountId}/in_collections")]
    Task<Collections> InCollections(
        string accountId,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("offset")] int? offset = null);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/collections/#update_collection">Mastodon API Documentation</see>
    [Patch("/api/v1/collections/{id}")]
    Task<WrappedCollection> UpdateCollection(
        string id,
        [AliasAs("name")] string? name = null,
        [AliasAs("description")] string? description = null,
        [AliasAs("language")] string? language = null,
        [AliasAs("tag_name")] string? tagName = null,
        [AliasAs("sensitive")] bool? sensitive = null,
        [AliasAs("discoverable")] bool? discoverable = null);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/collections/#delete_collection">Mastodon API Documentation</see>
    [Delete("/api/v1/collections/{id}")]
    Task DeleteCollection(string id);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/collections/#add_account">Mastodon API Documentation</see>
    [Post("/api/v1/collections/{collectionId}/items")]
    Task<WrappedCollectionItem> AddAccount(
        string collectionId,
        [AliasAs("account_id")] string accountId);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/collections/#remove_account">Mastodon API Documentation</see>
    [Delete("/api/v1/collections/{collectionId}/items/{id}")]
    Task RemoveAccount(string collectionId, string id);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/collections/#revoke_item">Mastodon API Documentation</see>
    [Post("/api/v1/collections/{collectionId}/items/{id}/revoke")]
    Task RevokeItem(string collectionId, string id);
}
