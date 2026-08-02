using Refit;

namespace Model.Server.Admin.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/canonical_email_blocks/">Mastodon API Documentation</see>
public interface ICanonicalEmailBlocks {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/canonical_email_blocks/#get"/>
    [Get("/api/v1/admin/canonical_email_blocks")]
    Task<List<AdminCanonicalEmailBlock>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/canonical_email_blocks/#get-one"/>
    [Get("/api/v1/admin/canonical_email_blocks/{id}")]
    Task<AdminCanonicalEmailBlock> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/canonical_email_blocks/#test"/>
    [Post("/api/v1/admin/canonical_email_blocks/test")]
    Task<List<AdminCanonicalEmailBlock>> Test([AliasAs("email")] string email);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/canonical_email_blocks/#create"/>
    [Post("/api/v1/admin/canonical_email_blocks")]
    Task<AdminCanonicalEmailBlock> Create(
        [AliasAs("email")] string? email = null,
        [AliasAs("canonical_email_hash")] string? canonicalEmailHash = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/canonical_email_blocks/#delete"/>
    [Delete("/api/v1/admin/canonical_email_blocks/{id}")]
    Task<AdminCanonicalEmailBlock> Delete(string id);
}
