using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IConversations {
    [Get("/api/v1/conversations")]
    Task<List<Conversation>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    [Delete("/api/v1/conversations/{id}")]
    Task Delete(string id);

    [Post("/api/v1/conversations/{id}/read")]
    Task<Conversation> Read(string id);
}
