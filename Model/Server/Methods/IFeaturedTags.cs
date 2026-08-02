using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/featured_tags/">Mastodon API Documentation</see>
public interface IFeaturedTags {
    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/featured_tags/#get"/>
    [Get("/api/v1/featured_tags")]
    Task<List<FeaturedTag>> Get();

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/featured_tags/#feature"/>
    [Post("/api/v1/featured_tags")]
    Task<FeaturedTag> Feature([AliasAs("name")] string name);

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/featured_tags/#unfeature"/>
    [Delete("/api/v1/featured_tags/{id}")]
    Task Unfeature(string id);

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/featured_tags/#suggestions"/>
    [Get("/api/v1/featured_tags/suggestions")]
    Task<List<Tag>> Suggestions();
}
