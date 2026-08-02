using Refit;

namespace Model.Server.Admin.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/domain_allows/">Mastodon API Documentation</see>
public interface IDomainAllows {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_allows/#get"/>
    [Get("/api/v1/admin/domain_allows")]
    Task<List<AdminDomainAllow>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_allows/#get-one"/>
    [Get("/api/v1/admin/domain_allows/{id}")]
    Task<AdminDomainAllow> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_allows/#create"/>
    [Post("/api/v1/admin/domain_allows")]
    Task<AdminDomainAllow> Create([AliasAs("domain")] string domain);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_allows/#delete"/>
    [Delete("/api/v1/admin/domain_allows/{id}")]
    Task<AdminDomainAllow> Delete(string id);
}
