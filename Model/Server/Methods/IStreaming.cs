using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/streaming/">Mastodon API Documentation</see>
public interface IStreaming {
    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/streaming/#websocket"/>
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
    /// <seealso href="https://docs.joinmastodon.org/methods/streaming/#user"/>
    [Get("/api/v1/streaming/user")]
    Task<HttpResponseMessage> User();

    /// <summary>
    /// Version: 1.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/streaming/#notification"/>
    [Get("/api/v1/streaming/user/notification")]
    Task<HttpResponseMessage> Notification();

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/streaming/#public"/>
    [Get("/api/v1/streaming/public")]
    Task<HttpResponseMessage> Public([AliasAs("only_media")] bool? onlyMedia = null);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/streaming/#public-local"/>
    [Get("/api/v1/streaming/public/local")]
    Task<HttpResponseMessage> PublicLocal([AliasAs("only_media")] bool? onlyMedia = null);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/streaming/#public-remote"/>
    [Get("/api/v1/streaming/public/remote")]
    Task<HttpResponseMessage> PublicRemote([AliasAs("only_media")] bool? onlyMedia = null);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/streaming/#hashtag"/>
    [Get("/api/v1/streaming/hashtag")]
    Task<HttpResponseMessage> Hashtag([AliasAs("tag")] string tag);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/streaming/#hashtag-local"/>
    [Get("/api/v1/streaming/hashtag/local")]
    Task<HttpResponseMessage> HashtagLocal([AliasAs("tag")] string tag);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/streaming/#list"/>
    [Get("/api/v1/streaming/list")]
    Task<HttpResponseMessage> List([AliasAs("list")] string listId);

    /// <summary>
    /// Version: 2.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/streaming/#direct"/>
    [Get("/api/v1/streaming/direct")]
    Task<HttpResponseMessage> Direct();

    /// <summary>
    /// Version: 2.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/streaming/#health"/>
    [Get("/api/v1/streaming/health")]
    Task<string> Health();
}
