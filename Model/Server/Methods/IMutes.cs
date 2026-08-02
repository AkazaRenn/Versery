using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IMutes {
    [Get("/api/v1/mutes")]
    Task<List<Account>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);
}
