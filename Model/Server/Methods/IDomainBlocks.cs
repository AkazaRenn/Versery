using Refit;

namespace Model.Server.Methods;

public interface IDomainBlocks {
    [Get("/api/v1/domain_blocks")]
    Task<List<string>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    [Post("/api/v1/domain_blocks")]
    Task Post([AliasAs("domain")] string domain);

    [Delete("/api/v1/domain_blocks")]
    Task Delete([AliasAs("domain")] string domain);
}
