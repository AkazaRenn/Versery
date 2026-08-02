using Model.Server.Entities;
using Refit;
using ServerList = Model.Server.Entities.List;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/lists/">Mastodon API Documentation</see>
public interface ILists {
    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/lists/#get">Mastodon API Documentation</see>
    [Get("/api/v1/lists")]
    Task<List<ServerList>> Get();

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/lists/#get-one">Mastodon API Documentation</see>
    [Get("/api/v1/lists/{listId}")]
    Task<ServerList> GetOne(string listId);

    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/lists/#accounts">Mastodon API Documentation</see>
    [Get("/api/v1/lists/{listId}/accounts")]
    Task<List<Account>> Accounts(
        string listId,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/lists/#create">Mastodon API Documentation</see>
    [Post("/api/v1/lists")]
    Task<ServerList> Create(
        [AliasAs("title")] string title,
        [AliasAs("replies_policy")] string? repliesPolicy = null,
        [AliasAs("exclusive")] bool? exclusive = null);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/lists/#update">Mastodon API Documentation</see>
    [Put("/api/v1/lists/{listId}")]
    Task<ServerList> Update(
        string listId,
        [AliasAs("title")] string title,
        [AliasAs("replies_policy")] string? repliesPolicy = null,
        [AliasAs("exclusive")] bool? exclusive = null);

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/lists/#delete">Mastodon API Documentation</see>
    [Delete("/api/v1/lists/{listId}")]
    Task Delete(string listId);

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/lists/#accounts-add">Mastodon API Documentation</see>
    [Post("/api/v1/lists/{listId}/accounts")]
    Task AccountsAdd(
        string listId,
        [AliasAs("account_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string> accountIds);

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/lists/#accounts-remove">Mastodon API Documentation</see>
    [Delete("/api/v1/lists/{listId}/accounts")]
    Task AccountsRemove(
        string listId,
        [AliasAs("account_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string> accountIds);
}
