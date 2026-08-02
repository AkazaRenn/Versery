using Model.Server.Entities;
using Model.Server.Methods.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IMarkers {
    [Get("/api/v1/markers")]
    Task<Markers> Get([AliasAs("timeline[]")][Query(CollectionFormat.Multi)] IEnumerable<string> timelines);

    [Post("/api/v1/markers")]
    Task<Markers> Post(
        [AliasAs("home[last_read_id]")] string? homeLastReadId = null,
        [AliasAs("notifications[last_read_id]")] string? notificationLastReadId = null);
}
