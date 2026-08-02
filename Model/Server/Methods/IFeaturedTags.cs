using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/featured_tags/">Mastodon API Documentation</see>
public interface IFeaturedTags {
    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/featured_tags/#get">Mastodon API Documentation</see>
    [Get("/api/v1/featured_tags")]
    Task<List<FeaturedTag>> Get();

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/featured_tags/#feature">Mastodon API Documentation</see>
    [Post("/api/v1/featured_tags")]
    Task<FeaturedTag> Feature([AliasAs("name")] string name);

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/featured_tags/#unfeature">Mastodon API Documentation</see>
    [Delete("/api/v1/featured_tags/{id}")]
    Task Unfeature(string id);

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/featured_tags/#suggestions">Mastodon API Documentation</see>
    [Get("/api/v1/featured_tags/suggestions")]
    Task<List<Tag>> Suggestions();
}
