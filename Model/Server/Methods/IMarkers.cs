using Model.Server.Entities;
using Refit;
using System.Text.Json.Serialization;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/markers/">Mastodon API Documentation</see>
public interface IMarkers {
    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/markers/#get"/>
    [Get("/api/v1/markers")]
    Task<Markers> Get([AliasAs("timeline[]")][Query(CollectionFormat.Multi)] IEnumerable<string> timelines);

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/markers/#create"/>
    [Post("/api/v1/markers")]
    Task<Markers> Create(
        [AliasAs("home[last_read_id]")] string? homeLastReadId = null,
        [AliasAs("notifications[last_read_id]")] string? notificationsLastReadId = null);
}

public sealed class Markers {
    [JsonPropertyName("home")]
    public Marker? Home { get; set; } = null;

    [JsonPropertyName("notifications")]
    public Marker? Notifications { get; set; } = null;
}
