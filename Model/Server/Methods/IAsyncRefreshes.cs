using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/async_refreshes/">Mastodon API Documentation</see>
public interface IAsyncRefreshes {
    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/async_refreshes/#show"/>
    [Get("/api/v1_alpha/async_refreshes/{id}")]
    Task<AsyncRefreshResponse> Show(string id);
}
