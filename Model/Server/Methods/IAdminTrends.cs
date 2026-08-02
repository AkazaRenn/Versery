using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/trends/">Mastodon API Documentation</see>
public interface IAdminTrends {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/trends/#links">Mastodon API Documentation</see>
    [Get("/api/v1/admin/trends/links")]
    Task<List<TrendsLink>> Links();

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/trends/#statuses">Mastodon API Documentation</see>
    [Get("/api/v1/admin/trends/statuses")]
    Task<List<Status>> Statuses();

    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/trends/#tags">Mastodon API Documentation</see>
    [Get("/api/v1/admin/trends/tags")]
    Task<List<AdminTag>> Tags();
}
