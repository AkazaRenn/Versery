using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IBookmarks {
    [Get("/api/v1/bookmarks")]
    Task<List<Status>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);
}
