using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/grouped_notifications/">Mastodon API Documentation</see>
public interface IGroupedNotifications {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/grouped_notifications/#get-grouped">Mastodon API Documentation</see>
    [Get("/api/v2/notifications")]
    Task<GroupedNotificationsResults> GetGrouped(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? types = null,
        [AliasAs("exclude_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? excludeTypes = null,
        [AliasAs("account_id")] string? accountId = null,
        [AliasAs("expand_accounts")] string? expandAccounts = null,
        [AliasAs("grouped_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? groupedTypes = null,
        [AliasAs("include_filtered")] bool? includeFiltered = null,
        [AliasAs("supported_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? supportedTypes = null);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/grouped_notifications/#get-notification-group">Mastodon API Documentation</see>
    [Get("/api/v2/notifications/{groupKey}")]
    Task<GroupedNotificationsResults> GetNotificationGroup(
        string groupKey,
        [AliasAs("supported_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? supportedTypes = null);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/grouped_notifications/#dismiss-group">Mastodon API Documentation</see>
    [Post("/api/v2/notifications/{groupKey}/dismiss")]
    Task DismissGroup(string groupKey);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/grouped_notifications/#get-group-accounts">Mastodon API Documentation</see>
    [Get("/api/v2/notifications/{groupKey}/accounts")]
    Task<List<Account>> GetGroupAccounts(string groupKey);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/grouped_notifications/#unread-group-count">Mastodon API Documentation</see>
    [Get("/api/v2/notifications/unread_count")]
    Task<GroupedNotificationUnreadCount> UnreadGroupCount(
        [AliasAs("limit")] int? limit = null,
        [AliasAs("types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? types = null,
        [AliasAs("exclude_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? excludeTypes = null,
        [AliasAs("account_id")] string? accountId = null,
        [AliasAs("grouped_types[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? groupedTypes = null);
}
