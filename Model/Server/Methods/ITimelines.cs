using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface ITimelines {
    [Get("/api/v1/timelines/home")]
    Task<List<Status>> GetHome(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    [Get("/api/v1/timelines/public")]
    Task<List<Status>> GetPublic(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("local")] bool? local = null,
        [AliasAs("only_media")] bool? onlyMedia = null);

    [Get("/api/v1/timelines/tag/{hashtag}")]
    Task<List<Status>> GetTag(
        string hashtag,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("local")] bool? local = null,
        [AliasAs("only_media")] bool? onlyMedia = null);

    [Get("/api/v1/timelines/list/{listId}")]
    Task<List<Status>> GetList(
        long listId,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);
}
