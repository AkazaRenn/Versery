using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/announcements/">Mastodon API Documentation</see>
public interface IAnnouncements {
    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/announcements/#get">Mastodon API Documentation</see>
    [Get("/api/v1/announcements")]
    Task<List<Announcement>> Get();

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/announcements/#dismiss">Mastodon API Documentation</see>
    [Post("/api/v1/announcements/{id}/dismiss")]
    Task Dismiss(string id);

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/announcements/#put-reactions">Mastodon API Documentation</see>
    [Put("/api/v1/announcements/{id}/reactions/{name}")]
    Task PutReactions(string id, string name);

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/announcements/#delete-reactions">Mastodon API Documentation</see>
    [Delete("/api/v1/announcements/{id}/reactions/{name}")]
    Task DeleteReactions(string id, string name);
}
