using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/timelines/">Mastodon API Documentation</see>
public interface ITimelines {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/timelines/#home">Mastodon API Documentation</see>
    [Get("/api/v1/timelines/home")]
    Task<List<Status>> Home(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/timelines/#public">Mastodon API Documentation</see>
    [Get("/api/v1/timelines/public")]
    Task<List<Status>> Public(
        [AliasAs("local")] bool? local = null,
        [AliasAs("remote")] bool? remote = null,
        [AliasAs("only_media")] bool? onlyMedia = null,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/timelines/#link">Mastodon API Documentation</see>
    [Get("/api/v1/timelines/link")]
    Task<List<Status>> Link(
        [AliasAs("url")] string url,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/timelines/#tag">Mastodon API Documentation</see>
    [Get("/api/v1/timelines/tag/{hashtag}")]
    Task<List<Status>> Tag(
        string hashtag,
        [AliasAs("any[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? any = null,
        [AliasAs("all[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? all = null,
        [AliasAs("none[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? none = null,
        [AliasAs("local")] bool? local = null,
        [AliasAs("remote")] bool? remote = null,
        [AliasAs("only_media")] bool? onlyMedia = null,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/timelines/#direct">Mastodon API Documentation</see>
    [Get("/api/v1/timelines/direct")]
    Task<List<Status>> Direct(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/timelines/#list">Mastodon API Documentation</see>
    [Get("/api/v1/timelines/list/{listId}")]
    Task<List<Status>> List(
        string listId,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);
}
