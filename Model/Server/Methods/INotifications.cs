using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface INotifications {
    [Get("/api/v1/notifications")]
    Task<List<Notification>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("exclude_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? excludeTypes = null);

    [Get("/api/v1/notifications/{notificationId}")]
    Task<Notification> Get(string notificationId);

    [Post("/api/v1/notifications/clear")]
    Task Clear();

    [Post("/api/v1/notifications/{notificationId}/dismiss")]
    Task Dismiss(string notificationId);
}
