using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods.Admin;

/// <see href="https://docs.joinmastodon.org/methods/admin/domain_blocks/">Mastodon API Documentation</see>
public interface IDomainBlocks {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_blocks/#get"/>
    [Get("/api/v1/admin/domain_blocks")]
    Task<List<DomainBlock>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_blocks/#get-one"/>
    [Get("/api/v1/admin/domain_blocks/{id}")]
    Task<DomainBlock> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_blocks/#create"/>
    [Post("/api/v1/admin/domain_blocks")]
    Task<DomainBlock> Create(
        [AliasAs("domain")] string domain,
        [AliasAs("severity")] DomainBlockSeverity? severity = null,
        [AliasAs("reject_media")] bool? rejectMedia = null,
        [AliasAs("reject_reports")] bool? rejectReports = null,
        [AliasAs("private_comment")] string? privateComment = null,
        [AliasAs("public_comment")] string? publicComment = null,
        [AliasAs("obfuscate")] bool? obfuscate = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_blocks/#update"/>
    [Put("/api/v1/admin/domain_blocks/{id}")]
    Task<DomainBlock> Update(
        string id,
        [AliasAs("severity")] DomainBlockSeverity? severity = null,
        [AliasAs("reject_media")] bool? rejectMedia = null,
        [AliasAs("reject_reports")] bool? rejectReports = null,
        [AliasAs("private_comment")] string? privateComment = null,
        [AliasAs("public_comment")] string? publicComment = null,
        [AliasAs("obfuscate")] bool? obfuscate = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/domain_blocks/#delete"/>
    [Delete("/api/v1/admin/domain_blocks/{id}")]
    Task<DomainBlock> Delete(string id);
}
