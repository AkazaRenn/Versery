using Refit;

namespace Model.Server.Admin.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/ip_blocks/">Mastodon API Documentation</see>
public interface IIpBlocks {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/ip_blocks/#get"/>
    [Get("/api/v1/admin/ip_blocks")]
    Task<List<AdminIpBlock>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/ip_blocks/#get-one"/>
    [Get("/api/v1/admin/ip_blocks/{id}")]
    Task<AdminIpBlock> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/ip_blocks/#create"/>
    [Post("/api/v1/admin/ip_blocks")]
    Task<AdminIpBlock> Create(
        [AliasAs("ip")] string? ip = null,
        [AliasAs("severity")] IpBlockSeverity severity = IpBlockSeverity.SignUpRequiresApproval,
        [AliasAs("comment")] string? comment = null,
        [AliasAs("expires_in")] int? expiresIn = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/ip_blocks/#update"/>
    [Put("/api/v1/admin/ip_blocks/{id}")]
    Task<AdminIpBlock> Update(
        string id,
        [AliasAs("ip")] string? ip = null,
        [AliasAs("severity")] IpBlockSeverity? severity = null,
        [AliasAs("comment")] string? comment = null,
        [AliasAs("expires_in")] int? expiresIn = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/ip_blocks/#delete"/>
    [Delete("/api/v1/admin/ip_blocks/{id}")]
    Task<AdminIpBlock> Delete(string id);
}
