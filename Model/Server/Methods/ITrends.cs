using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface ITrends {
    [Get("/api/v1/trends/tags")]
    Task<List<Tag>> GetTags();

    [Get("/api/v1/trends/statuses")]
    Task<List<Status>> GetStatuses(
        [AliasAs("offset")] int? offset = null,
        [AliasAs("limit")] int? limit = null);
}
