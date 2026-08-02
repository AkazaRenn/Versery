using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/conversations/">Mastodon API Documentation</see>
public interface IConversations {
    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/conversations/#get">Mastodon API Documentation</see>
    [Get("/api/v1/conversations")]
    Task<List<Conversation>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 2.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/conversations/#delete">Mastodon API Documentation</see>
    [Delete("/api/v1/conversations/{id}")]
    Task Delete(string id);

    /// <summary>
    /// Version: 2.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/conversations/#read">Mastodon API Documentation</see>
    [Post("/api/v1/conversations/{id}/read")]
    Task<Conversation> Read(string id);
}
