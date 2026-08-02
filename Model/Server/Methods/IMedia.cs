using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/media/">Mastodon API Documentation</see>
public interface IMedia {
    /// <summary>
    /// Version: 3.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/media/#v1">Mastodon API Documentation</see>
    [Multipart]
    [Post("/api/v1/media")]
    Task<MediaAttachment> V1(
        [AliasAs("file")] StreamPart file,
        [AliasAs("thumbnail")] StreamPart? thumbnail = null,
        [AliasAs("description")] string? description = null,
        [AliasAs("focus")] string? focus = null);

    /// <summary>
    /// Version: 3.1.3
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/media/#get">Mastodon API Documentation</see>
    [Get("/api/v1/media/{mediaId}")]
    Task<MediaAttachment> Get(string mediaId);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/media/#v2">Mastodon API Documentation</see>
    [Multipart]
    [Post("/api/v2/media")]
    Task<MediaAttachment> V2(
        [AliasAs("file")] StreamPart file,
        [AliasAs("thumbnail")] StreamPart? thumbnail = null,
        [AliasAs("description")] string? description = null,
        [AliasAs("focus")] string? focus = null);

    /// <summary>
    /// Version: 3.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/media/#update">Mastodon API Documentation</see>
    [Multipart]
    [Put("/api/v1/media/{mediaId}")]
    Task<MediaAttachment> Update(
        string mediaId,
        [AliasAs("thumbnail")] StreamPart? thumbnail = null,
        [AliasAs("description")] string? description = null,
        [AliasAs("focus")] string? focus = null);

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/media/#delete">Mastodon API Documentation</see>
    [Delete("/api/v1/media/{mediaId}")]
    Task Delete(string mediaId);
}
