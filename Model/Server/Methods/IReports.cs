using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IReports {
    [Get("/api/v1/reports")]
    Task<List<Report>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    [Post("/api/v1/reports")]
    Task<Report> Post(
        [AliasAs("account_id")] string accountId,
        [AliasAs("status_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? statusIds = null,
        [AliasAs("comment")] string? comment = null,
        [AliasAs("forward")] bool? forward = null);
}
