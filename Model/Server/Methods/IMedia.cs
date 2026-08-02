using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/media/">Mastodon API Documentation</see>
public interface IMedia {
    /// <summary>
    /// Version: 3.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/media/#v1"/>
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
    /// <seealso href="https://docs.joinmastodon.org/methods/media/#get"/>
    [Get("/api/v1/media/{mediaId}")]
    Task<MediaAttachment> Get(string mediaId);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/media/#v2"/>
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
    /// <seealso href="https://docs.joinmastodon.org/methods/media/#update"/>
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
    /// <seealso href="https://docs.joinmastodon.org/methods/media/#delete"/>
    [Delete("/api/v1/media/{mediaId}")]
    Task Delete(string mediaId);
}
