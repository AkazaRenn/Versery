using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/notifications_alpha/">Mastodon API Documentation</see>
[System.Obsolete]
public interface INotificationsAlpha {
    /// <summary>
    /// Version: 4.3.0-beta.2
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications_alpha/#get-grouped">Mastodon API Documentation</see>
    [System.Obsolete]
    [Get("/api/v2_alpha/notifications")]
    Task<GroupedNotificationsResults> GetGrouped(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? types = null,
        [AliasAs("exclude_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? excludeTypes = null,
        [AliasAs("account_id")] string? accountId = null,
        [AliasAs("expand_accounts")] string? expandAccounts = null,
        [AliasAs("grouped_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? groupedTypes = null);

    /// <summary>
    /// Version: 4.3.0-beta.2
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications_alpha/#get-notification-group">Mastodon API Documentation</see>
    [System.Obsolete]
    [Get("/api/v2_alpha/notifications/{groupKey}")]
    Task<GroupedNotificationsResults> GetNotificationGroup(string groupKey);

    /// <summary>
    /// Version: 4.3.0-beta.2
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications_alpha/#dismiss-group">Mastodon API Documentation</see>
    [System.Obsolete]
    [Post("/api/v2_alpha/notifications/{groupKey}/dismiss")]
    Task DismissGroup(string groupKey);

    /// <summary>
    /// Version: 4.3.0-beta.2
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/notifications_alpha/#unread-group-count">Mastodon API Documentation</see>
    [System.Obsolete]
    [Get("/api/v2_alpha/notifications/unread_count")]
    Task<GroupedNotificationUnreadCount> UnreadGroupCount(
        [AliasAs("limit")] int? limit = null,
        [AliasAs("types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? types = null,
        [AliasAs("exclude_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? excludeTypes = null,
        [AliasAs("account_id")] string? accountId = null,
        [AliasAs("grouped_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? groupedTypes = null);
}
