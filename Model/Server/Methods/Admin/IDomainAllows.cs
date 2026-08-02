using Model.Server.Entities.Admin;
using Refit;

namespace Model.Server.Methods.Admin;

/// <see href="https://docs.joinmastodon.org/methods/admin/domain_allows/">Mastodon API Documentation</see>
public interface IDomainAllows {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_allows/#get"/>
    [Get("/api/v1/admin/domain_allows")]
    Task<List<DomainAllow>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_allows/#get-one"/>
    [Get("/api/v1/admin/domain_allows/{id}")]
    Task<DomainAllow> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_allows/#create"/>
    [Post("/api/v1/admin/domain_allows")]
    Task<DomainAllow> Create([AliasAs("domain")] string domain);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_allows/#delete"/>
    [Delete("/api/v1/admin/domain_allows/{id}")]
    Task<DomainAllow> Delete(string id);
}
