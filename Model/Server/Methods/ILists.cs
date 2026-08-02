using Model.Server.Entities;
using Raiqub.Generators.EnumUtilities;
using Refit;
using System.Text.Json.Serialization;
using ServerList = Model.Server.Entities.List;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/lists/">Mastodon API Documentation</see>
public interface ILists {
    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/lists/#get"/>
    [Get("/api/v1/lists")]
    Task<List<ServerList>> Get();

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/lists/#get-one"/>
    [Get("/api/v1/lists/{listId}")]
    Task<ServerList> GetOne(string listId);

    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/lists/#accounts"/>
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
    /// <seealso href="https://docs.joinmastodon.org/methods/lists/#create"/>
    [Post("/api/v1/lists")]
    Task<ServerList> Create(
        [AliasAs("title")] string title,
        [AliasAs("replies_policy")] ListsRepliesPolicy? repliesPolicy = null,
        [AliasAs("exclusive")] bool? exclusive = null);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/lists/#update"/>
    [Put("/api/v1/lists/{listId}")]
    Task<ServerList> Update(
        string listId,
        [AliasAs("title")] string title,
        [AliasAs("replies_policy")] ListsRepliesPolicy? repliesPolicy = null,
        [AliasAs("exclusive")] bool? exclusive = null);

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/lists/#delete"/>
    [Delete("/api/v1/lists/{listId}")]
    Task Delete(string listId);

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/lists/#accounts-add"/>
    [Post("/api/v1/lists/{listId}/accounts")]
    Task AccountsAdd(
        string listId,
        [AliasAs("account_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string> accountIds);

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/lists/#accounts-remove"/>
    [Delete("/api/v1/lists/{listId}/accounts")]
    Task AccountsRemove(
        string listId,
        [AliasAs("account_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string> accountIds);
}

[JsonConverterGenerator]
public enum ListsRepliesPolicy {
    [JsonPropertyName("followed")]
    Followed,
    [JsonPropertyName("list")]
    List,
    [JsonPropertyName("none")]
    None
}
