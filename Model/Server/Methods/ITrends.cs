using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/trends/">Mastodon API Documentation</see>
public interface ITrends {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/trends/#tags"/>
    [Get("/api/v1/trends/tags")]
    Task<List<Tag>> Tags(
        [AliasAs("limit")] int? limit = null,
        [AliasAs("offset")] int? offset = null);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/trends/#statuses"/>
    [Get("/api/v1/trends/statuses")]
    Task<List<Status>> Statuses(
        [AliasAs("limit")] int? limit = null,
        [AliasAs("offset")] int? offset = null);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/trends/#links"/>
    [Get("/api/v1/trends/links")]
    Task<List<TrendsLink>> Links(
        [AliasAs("limit")] int? limit = null,
        [AliasAs("offset")] int? offset = null);
}
