using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IBlocks {
    [Get("/api/v1/blocks")]
    Task<List<Account>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);
}
