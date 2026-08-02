using Refit;

namespace Model.Server.Admin.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/reports/">Mastodon API Documentation</see>
public interface IReports {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/reports/#get"/>
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
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/reports/#get-one"/>
    [Get("/api/v1/admin/reports/{id}")]
    Task<AdminReport> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/reports/#update"/>
    [Put("/api/v1/admin/reports/{id}")]
    Task<AdminReport> Update(
        string id,
        [AliasAs("category")] ReportCategory? category = null,
        [AliasAs("rule_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? ruleIds = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/reports/#assign_to_self"/>
    [Post("/api/v1/admin/reports/{id}/assign_to_self")]
    Task<AdminReport> AssignToSelf(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/reports/#unassign"/>
    [Post("/api/v1/admin/reports/{id}/unassign")]
    Task<AdminReport> Unassign(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/reports/#resolve"/>
    [Post("/api/v1/admin/reports/{id}/resolve")]
    Task<AdminReport> Resolve(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/reports/#reopen"/>
    [Post("/api/v1/admin/reports/{id}/reopen")]
    Task<AdminReport> Reopen(string id);
}
