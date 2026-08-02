using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IAnnouncements {
    [Get("/api/v1/announcements")]
    Task<List<Announcement>> Get();

    [Post("/api/v1/announcements/{id}/dismiss")]
    Task Dismiss(string id);

    [Put("/api/v1/announcements/{id}/reactions/{emoji}")]
    Task PutReactions(string id, string emoji);

    [Delete("/api/v1/announcements/{id}/reactions/{emoji}")]
    Task DeleteReactions(string id, string emoji);
}
