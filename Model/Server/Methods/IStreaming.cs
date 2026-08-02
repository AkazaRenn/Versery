using Refit;
using System.Net.Http;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/streaming/">Mastodon API Documentation</see>
public interface IStreaming {
    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/streaming/#websocket">Mastodon API Documentation</see>
    [Get("/api/v1/streaming")]
    Task<HttpResponseMessage> Websocket(
        [AliasAs("access_token")] string? accessToken = null,
        [AliasAs("stream")] string? stream = null,
        [AliasAs("list")] string? list = null,
        [AliasAs("tag")] string? tag = null,
        [AliasAs("type")] string? type = null);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/streaming/#user">Mastodon API Documentation</see>
    [Get("/api/v1/streaming/user")]
    Task<HttpResponseMessage> User();

    /// <summary>
    /// Version: 1.4.2
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/streaming/#notification">Mastodon API Documentation</see>
    [Get("/api/v1/streaming/user/notification")]
    Task<HttpResponseMessage> Notification();

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/streaming/#public">Mastodon API Documentation</see>
    [Get("/api/v1/streaming/public")]
    Task<HttpResponseMessage> Public([AliasAs("only_media")] bool? onlyMedia = null);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/streaming/#public-local">Mastodon API Documentation</see>
    [Get("/api/v1/streaming/public/local")]
    Task<HttpResponseMessage> PublicLocal([AliasAs("only_media")] bool? onlyMedia = null);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/streaming/#public-remote">Mastodon API Documentation</see>
    [Get("/api/v1/streaming/public/remote")]
    Task<HttpResponseMessage> PublicRemote([AliasAs("only_media")] bool? onlyMedia = null);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/streaming/#hashtag">Mastodon API Documentation</see>
    [Get("/api/v1/streaming/hashtag")]
    Task<HttpResponseMessage> Hashtag([AliasAs("tag")] string tag);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/streaming/#hashtag-local">Mastodon API Documentation</see>
    [Get("/api/v1/streaming/hashtag/local")]
    Task<HttpResponseMessage> HashtagLocal([AliasAs("tag")] string tag);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/streaming/#list">Mastodon API Documentation</see>
    [Get("/api/v1/streaming/list")]
    Task<HttpResponseMessage> List([AliasAs("list")] string listId);

    /// <summary>
    /// Version: 2.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/streaming/#direct">Mastodon API Documentation</see>
    [Get("/api/v1/streaming/direct")]
    Task<HttpResponseMessage> Direct();

    /// <summary>
    /// Version: 2.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/streaming/#health">Mastodon API Documentation</see>
    [Get("/api/v1/streaming/health")]
    Task<string> Health();
}
