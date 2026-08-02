using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/followed_tags/">Mastodon API Documentation</see>
public interface IFollowedTags {
    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/followed_tags/#get"/>
    [Get("/api/v1/followed_tags")]
    Task<List<Tag>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);
}
