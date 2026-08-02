using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/tags/">Mastodon API Documentation</see>
public interface ITags {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/tags/#get"/>
    [Get("/api/v1/tags/{name}")]
    Task<Tag> Get(string name);

    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/tags/#follow"/>
    [Post("/api/v1/tags/{name}/follow")]
    Task<Tag> Follow(string name);

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/tags/#feature"/>
    [Post("/api/v1/tags/{id}/feature")]
    Task<Tag> Feature(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/tags/#unfollow"/>
    [Post("/api/v1/tags/{name}/unfollow")]
    Task<Tag> Unfollow(string name);

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/tags/#unfeature"/>
    [Post("/api/v1/tags/{id}/unfeature")]
    Task<Tag> Unfeature(string id);
}
