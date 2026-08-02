using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/reports/">Mastodon API Documentation</see>
public interface IReports {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/reports/#post"/>
    [Post("/api/v1/reports")]
    Task<Report> Create(
        [AliasAs("account_id")] string accountId,
        [AliasAs("status_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? statusIds = null,
        [AliasAs("collection_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? collectionIds = null,
        [AliasAs("comment")] string? comment = null,
        [AliasAs("forward")] bool? forward = null,
        [AliasAs("category")] ReportCategory? category = null,
        [AliasAs("rule_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? ruleIds = null);
}
