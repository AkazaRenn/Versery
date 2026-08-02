using Model.Server.Entities;
using Refit;

namespace Model.Server.Admin.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/trends/">Mastodon API Documentation</see>
public interface ITrends {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/trends/#links"/>
    [Get("/api/v1/admin/trends/links")]
    Task<List<TrendsLink>> Links();

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/trends/#statuses"/>
    [Get("/api/v1/admin/trends/statuses")]
    Task<List<Status>> Statuses();

    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/trends/#tags"/>
    [Get("/api/v1/admin/trends/tags")]
    Task<List<AdminTag>> Tags();
}
