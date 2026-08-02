using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/bookmarks/">Mastodon API Documentation</see>
public interface IBookmarks {
    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/bookmarks/#get">Mastodon API Documentation</see>
    [Get("/api/v1/bookmarks")]
    Task<List<Status>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);
}
