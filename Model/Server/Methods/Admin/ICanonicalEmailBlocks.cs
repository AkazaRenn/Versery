using Model.Server.Entities.Admin;
using Refit;

namespace Model.Server.Methods.Admin;

/// <see href="https://docs.joinmastodon.org/methods/admin/canonical_email_blocks/">Mastodon API Documentation</see>
public interface ICanonicalEmailBlocks {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/canonical_email_blocks/#get"/>
    [Get("/api/v1/admin/canonical_email_blocks")]
    Task<List<CanonicalEmailBlock>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/canonical_email_blocks/#get-one"/>
    [Get("/api/v1/admin/canonical_email_blocks/{id}")]
    Task<CanonicalEmailBlock> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/canonical_email_blocks/#test"/>
    [Post("/api/v1/admin/canonical_email_blocks/test")]
    Task<List<CanonicalEmailBlock>> Test([AliasAs("email")] string email);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/canonical_email_blocks/#create"/>
    [Post("/api/v1/admin/canonical_email_blocks")]
    Task<CanonicalEmailBlock> Create(
        [AliasAs("email")] string? email = null,
        [AliasAs("canonical_email_hash")] string? canonicalEmailHash = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/canonical_email_blocks/#delete"/>
    [Delete("/api/v1/admin/canonical_email_blocks/{id}")]
    Task<CanonicalEmailBlock> Delete(string id);
}
