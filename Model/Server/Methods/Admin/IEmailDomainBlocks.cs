
using Model.Server.Entities.Admin;
using Refit;

namespace Model.Server.Methods.Admin;

/// <see href="https://docs.joinmastodon.org/methods/admin/email_domain_blocks/">Mastodon API Documentation</see>
public interface IEmailDomainBlocks {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/email_domain_blocks/#get"/>
    [Get("/api/v1/admin/email_domain_blocks")]
    Task<List<EmailDomainBlock>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/email_domain_blocks/#get-one"/>
    [Get("/api/v1/admin/email_domain_blocks/{id}")]
    Task<EmailDomainBlock> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/email_domain_blocks/#create"/>
    [Post("/api/v1/admin/email_domain_blocks")]
    Task<EmailDomainBlock> Create([AliasAs("domain")] string domain);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/email_domain_blocks/#delete"/>
    [Delete("/api/v1/admin/email_domain_blocks/{id}")]
    Task<EmailDomainBlock> Delete(string id);
}
