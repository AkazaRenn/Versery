using Model.Server.Entities;
using Refit;
using System.Text.Json.Serialization;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/notifications/">Mastodon API Documentation</see>
public interface INotifications {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#get">Mastodon API Documentation</see>
    [Get("/api/v1/notifications")]
    Task<List<Notification>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? types = null,
        [AliasAs("exclude_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? excludeTypes = null,
        [AliasAs("account_id")] string? accountId = null,
        [AliasAs("include_filtered")] bool? includeFiltered = null,
        [AliasAs("supported_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? supportedTypes = null);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#get-one">Mastodon API Documentation</see>
    [Get("/api/v1/notifications/{notificationId}")]
    Task<Notification> GetOne(
        string notificationId,
        [AliasAs("supported_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? supportedTypes = null);

    /// <summary>
    /// Version: 0.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#clear">Mastodon API Documentation</see>
    [Post("/api/v1/notifications/clear")]
    Task Clear();

    /// <summary>
    /// Version: 1.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#dismiss">Mastodon API Documentation</see>
    [Post("/api/v1/notifications/{notificationId}/dismiss")]
    Task Dismiss(string notificationId);

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#dismiss-deprecated">Mastodon API Documentation</see>
    [System.Obsolete]
    [Post("/api/v1/notifications/dismiss")]
    Task DismissDeprecated([AliasAs("id")] string id);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#unread-count">Mastodon API Documentation</see>
    [Get("/api/v1/notifications/unread_count")]
    Task<NotificationUnreadCount> UnreadCount(
        [AliasAs("limit")] int? limit = null,
        [AliasAs("types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? types = null,
        [AliasAs("exclude_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? excludeTypes = null,
        [AliasAs("account_id")] string? accountId = null);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#get-requests">Mastodon API Documentation</see>
    [Get("/api/v1/notifications/requests")]
    Task<List<NotificationRequest>> GetRequests(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#get-one-request">Mastodon API Documentation</see>
    [Get("/api/v1/notifications/requests/{id}")]
    Task<NotificationRequest> GetOneRequest(string id);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#accept-request">Mastodon API Documentation</see>
    [Post("/api/v1/notifications/requests/{id}/accept")]
    Task AcceptRequest(string id);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#dismiss-request">Mastodon API Documentation</see>
    [Post("/api/v1/notifications/requests/{id}/dismiss")]
    Task DismissRequest(string id);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#accept-multiple-requests">Mastodon API Documentation</see>
    [Post("/api/v1/notifications/requests/accept")]
    Task AcceptMultipleRequests([AliasAs("id[]")][Query(CollectionFormat.Multi)] IEnumerable<string> ids);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#dismiss-multiple-requests">Mastodon API Documentation</see>
    [Post("/api/v1/notifications/requests/dismiss")]
    Task DismissMultipleRequests([AliasAs("id[]")][Query(CollectionFormat.Multi)] IEnumerable<string> ids);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#requests-merged">Mastodon API Documentation</see>
    [Get("/api/v1/notifications/requests/merged")]
    Task<NotificationRequestsMerged> RequestsMerged();

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#get-policy">Mastodon API Documentation</see>
    [Get("/api/v2/notifications/policy")]
    Task<NotificationPolicy> GetPolicy();

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications/#update-the-filtering-policy-for-notifications">Mastodon API Documentation</see>
    [Patch("/api/v2/notifications/policy")]
    Task<NotificationPolicy> UpdatePolicy(
        [AliasAs("for_not_following")] string? forNotFollowing = null,
        [AliasAs("for_not_followers")] string? forNotFollowers = null,
        [AliasAs("for_new_accounts")] string? forNewAccounts = null,
        [AliasAs("for_private_mentions")] string? forPrivateMentions = null,
        [AliasAs("for_limited_accounts")] string? forLimitedAccounts = null);
}

public sealed class NotificationUnreadCount {
    [JsonPropertyName("count")]
    public int Count { get; set; } = 0;
}

public sealed class NotificationRequestsMerged {
    [JsonPropertyName("merged")]
    public bool Merged { get; set; } = false;
}
