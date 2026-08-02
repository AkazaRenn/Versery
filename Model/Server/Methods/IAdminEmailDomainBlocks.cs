using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/email_domain_blocks/">Mastodon API Documentation</see>
public interface IAdminEmailDomainBlocks {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/email_domain_blocks/#get">Mastodon API Documentation</see>
    [Get("/api/v1/admin/email_domain_blocks")]
    Task<List<AdminEmailDomainBlock>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/email_domain_blocks/#get-one">Mastodon API Documentation</see>
    [Get("/api/v1/admin/email_domain_blocks/{id}")]
    Task<AdminEmailDomainBlock> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/email_domain_blocks/#create">Mastodon API Documentation</see>
    [Post("/api/v1/admin/email_domain_blocks")]
    Task<AdminEmailDomainBlock> Create([AliasAs("domain")] string domain);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/email_domain_blocks/#delete">Mastodon API Documentation</see>
    [Delete("/api/v1/admin/email_domain_blocks/{id}")]
    Task<AdminEmailDomainBlock> Delete(string id);
}
