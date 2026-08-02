using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/endorsements/">Mastodon API Documentation</see>
public interface IEndorsements {
    /// <summary>
    /// Version: 2.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/endorsements/#get">Mastodon API Documentation</see>
    [Get("/api/v1/endorsements")]
    Task<List<Account>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("limit")] int? limit = null);
}
