using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/domain_blocks/">Mastodon API Documentation</see>
public interface IDomainBlocks {
    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/domain_blocks/#get">Mastodon API Documentation</see>
    [Get("/api/v1/domain_blocks")]
    Task<List<string>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 1.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/domain_blocks/#block">Mastodon API Documentation</see>
    [Post("/api/v1/domain_blocks")]
    Task Block([AliasAs("domain")] string domain);

    /// <summary>
    /// Version: 1.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/domain_blocks/#unblock">Mastodon API Documentation</see>
    [Delete("/api/v1/domain_blocks")]
    Task Unblock([AliasAs("domain")] string domain);
}
