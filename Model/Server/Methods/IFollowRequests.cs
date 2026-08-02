using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IFollowRequests {
    [Get("/api/v1/follow_requests")]
    Task<List<Account>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    [Post("/api/v1/follow_requests/{accountId}/authorize")]
    Task Authorize(string accountId);

    [Post("/api/v1/follow_requests/{accountId}/reject")]
    Task Reject(string accountId);
}
