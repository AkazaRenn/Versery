using Model.Server.Entities;
using Refit;
using ServerList = Model.Server.Entities.List;

namespace Model.Server.Methods;

public interface ILists {
    [Get("/api/v1/lists")]
    Task<List<ServerList>> Get();

    [Get("/api/v1/lists/{listId}")]
    Task<ServerList> Get(string listId);

    [Get("/api/v1/lists/{listId}/accounts")]
    Task<List<Account>> GetAccounts(
        string listId,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    [Post("/api/v1/lists")]
    Task<ServerList> Post([AliasAs("title")] string title);

    [Put("/api/v1/lists/{listId}")]
    Task<ServerList> Put(string listId, [AliasAs("title")] string title);

    [Delete("/api/v1/lists/{listId}")]
    Task Delete(string listId);

    [Post("/api/v1/lists/{listId}/accounts")]
    Task PostAccounts(
        string listId,
        [AliasAs("account_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string> accountIds);

    [Delete("/api/v1/lists/{listId}/accounts")]
    Task DeleteAccounts(
        string listId,
        [AliasAs("account_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string> accountIds);
}
