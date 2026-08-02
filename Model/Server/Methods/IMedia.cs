using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IMedia {
    [Multipart]
    [Post("/api/v2/media")]
    Task<MediaAttachment> Post(
        [AliasAs("file")] StreamPart file,
        [AliasAs("description")] string? description = null,
        [AliasAs("focus")] string? focus = null);

    [Put("/api/v1/media/{mediaId}")]
    Task<MediaAttachment> Put(
        string mediaId,
        [AliasAs("description")] string? description = null,
        [AliasAs("focus")] string? focus = null);
}
