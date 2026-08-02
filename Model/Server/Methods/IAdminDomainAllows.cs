using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/domain_allows/">Mastodon API Documentation</see>
public interface IAdminDomainAllows {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/domain_allows/#get">Mastodon API Documentation</see>
    [Get("/api/v1/admin/domain_allows")]
    Task<List<AdminDomainAllow>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/domain_allows/#get-one">Mastodon API Documentation</see>
    [Get("/api/v1/admin/domain_allows/{id}")]
    Task<AdminDomainAllow> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/domain_allows/#create">Mastodon API Documentation</see>
    [Post("/api/v1/admin/domain_allows")]
    Task<AdminDomainAllow> Create([AliasAs("domain")] string domain);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/domain_allows/#delete">Mastodon API Documentation</see>
    [Delete("/api/v1/admin/domain_allows/{id}")]
    Task<AdminDomainAllow> Delete(string id);
}
