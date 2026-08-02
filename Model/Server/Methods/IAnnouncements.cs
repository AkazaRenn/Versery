using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/announcements/">Mastodon API Documentation</see>
public interface IAnnouncements {
    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/announcements/#get"/>
    [Get("/api/v1/announcements")]
    Task<List<Announcement>> Get();

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/announcements/#dismiss"/>
    [Post("/api/v1/announcements/{id}/dismiss")]
    Task Dismiss(string id);

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/announcements/#put-reactions"/>
    [Put("/api/v1/announcements/{id}/reactions/{name}")]
    Task PutReactions(string id, string name);

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/announcements/#delete-reactions"/>
    [Delete("/api/v1/announcements/{id}/reactions/{name}")]
    Task DeleteReactions(string id, string name);
}
