using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/polls/">Mastodon API Documentation</see>
public interface IPolls {
    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/polls/#get">Mastodon API Documentation</see>
    [Get("/api/v1/polls/{id}")]
    Task<Poll> Get(string id);

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/polls/#vote">Mastodon API Documentation</see>
    [Post("/api/v1/polls/{id}/votes")]
    Task<Poll> Vote(string id, [AliasAs("choices[]")][Query(CollectionFormat.Multi)] IEnumerable<int> choices);
}
