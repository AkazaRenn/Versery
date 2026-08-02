using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/reports/">Mastodon API Documentation</see>
public interface IAdminReports {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/reports/#get">Mastodon API Documentation</see>
    [Get("/api/v1/admin/reports")]
    Task<List<AdminReport>> Get(
        [AliasAs("resolved")] bool? resolved = null,
        [AliasAs("unresolved")] bool? unresolved = null,
        [AliasAs("account_id")] string? accountId = null,
        [AliasAs("target_account_id")] string? targetAccountId = null,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/reports/#get-one">Mastodon API Documentation</see>
    [Get("/api/v1/admin/reports/{id}")]
    Task<AdminReport> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/reports/#update">Mastodon API Documentation</see>
    [Put("/api/v1/admin/reports/{id}")]
    Task<AdminReport> Update(
        string id,
        [AliasAs("category")] AdminReportCategory? category = null,
        [AliasAs("rule_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? ruleIds = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/reports/#assign_to_self">Mastodon API Documentation</see>
    [Post("/api/v1/admin/reports/{id}/assign_to_self")]
    Task<AdminReport> AssignToSelf(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/reports/#unassign">Mastodon API Documentation</see>
    [Post("/api/v1/admin/reports/{id}/unassign")]
    Task<AdminReport> Unassign(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/reports/#resolve">Mastodon API Documentation</see>
    [Post("/api/v1/admin/reports/{id}/resolve")]
    Task<AdminReport> Resolve(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/reports/#reopen">Mastodon API Documentation</see>
    [Post("/api/v1/admin/reports/{id}/reopen")]
    Task<AdminReport> Reopen(string id);
}
