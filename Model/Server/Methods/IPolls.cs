using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IPolls {
    [Get("/api/v1/polls/{id}")]
    Task<Poll> Get(string id);

    [Post("/api/v1/polls/{id}/votes")]
    Task<Poll> Votes(string id, [AliasAs("choices[]")][Query(CollectionFormat.Multi)] IEnumerable<int> choices);
}
