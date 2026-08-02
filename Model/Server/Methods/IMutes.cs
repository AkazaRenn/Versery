using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/mutes/">Mastodon API Documentation</see>
public interface IMutes {
    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/mutes/#get">Mastodon API Documentation</see>
    [Get("/api/v1/mutes")]
    Task<List<MutedAccount>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("limit")] int? limit = null);
}
