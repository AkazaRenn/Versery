using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/ip_blocks/">Mastodon API Documentation</see>
public interface IAdminIpBlocks {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/ip_blocks/#get">Mastodon API Documentation</see>
    [Get("/api/v1/admin/ip_blocks")]
    Task<List<AdminIpBlock>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/ip_blocks/#get-one">Mastodon API Documentation</see>
    [Get("/api/v1/admin/ip_blocks/{id}")]
    Task<AdminIpBlock> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/ip_blocks/#create">Mastodon API Documentation</see>
    [Post("/api/v1/admin/ip_blocks")]
    Task<AdminIpBlock> Create(
        [AliasAs("ip")] string? ip = null,
        [AliasAs("severity")] AdminIpBlockSeverity severity = AdminIpBlockSeverity.SignUpRequiresApproval,
        [AliasAs("comment")] string? comment = null,
        [AliasAs("expires_in")] int? expiresIn = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/ip_blocks/#update">Mastodon API Documentation</see>
    [Put("/api/v1/admin/ip_blocks/{id}")]
    Task<AdminIpBlock> Update(
        string id,
        [AliasAs("ip")] string? ip = null,
        [AliasAs("severity")] AdminIpBlockSeverity? severity = null,
        [AliasAs("comment")] string? comment = null,
        [AliasAs("expires_in")] int? expiresIn = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/ip_blocks/#delete">Mastodon API Documentation</see>
    [Delete("/api/v1/admin/ip_blocks/{id}")]
    Task<AdminIpBlock> Delete(string id);
}
