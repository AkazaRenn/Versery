using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/follow_requests/">Mastodon API Documentation</see>
public interface IFollowRequests {
    /// <summary>
    /// Version: 0.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/follow_requests/#get">Mastodon API Documentation</see>
    [Get("/api/v1/follow_requests")]
    Task<List<Account>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/follow_requests/#accept">Mastodon API Documentation</see>
    [Post("/api/v1/follow_requests/{accountId}/authorize")]
    Task<Relationship> Accept(string accountId);

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/follow_requests/#reject">Mastodon API Documentation</see>
    [Post("/api/v1/follow_requests/{accountId}/reject")]
    Task<Relationship> Reject(string accountId);
}
