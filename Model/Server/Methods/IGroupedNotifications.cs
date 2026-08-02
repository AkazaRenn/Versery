using Model.Server.Entities;
using Refit;
using System.Text.Json.Serialization;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/grouped_notifications/">Mastodon API Documentation</see>
public interface IGroupedNotifications {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/grouped_notifications/#get-grouped"/>
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
        [AliasAs("grouped_types[]")][Query(CollectionFormat.Multi)] IEnumerable<NotificationType>? groupedTypes = null,
        [AliasAs("include_filtered")] bool? includeFiltered = null,
        [AliasAs("supported_types[]")][Query(CollectionFormat.Multi)] IEnumerable<NotificationType>? supportedTypes = null);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/grouped_notifications/#get-notification-group"/>
    [Get("/api/v2/notifications/{groupKey}")]
    Task<GroupedNotificationsResults> GetNotificationGroup(
        string groupKey,
        [AliasAs("supported_types[]")][Query(CollectionFormat.Multi)] IEnumerable<NotificationType>? supportedTypes = null);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/grouped_notifications/#dismiss-group"/>
    [Post("/api/v2/notifications/{groupKey}/dismiss")]
    Task DismissGroup(string groupKey);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/grouped_notifications/#get-group-accounts"/>
    [Get("/api/v2/notifications/{groupKey}/accounts")]
    Task<List<Account>> GetGroupAccounts(string groupKey);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/grouped_notifications/#unread-group-count"/>
    [Get("/api/v2/notifications/unread_count")]
    Task<GroupedNotificationUnreadCount> UnreadGroupCount(
        [AliasAs("limit")] int? limit = null,
        [AliasAs("types[]")][Query(CollectionFormat.Multi)] IEnumerable<NotificationType>? types = null,
        [AliasAs("exclude_types[]")][Query(CollectionFormat.Multi)] IEnumerable<NotificationType>? excludeTypes = null,
        [AliasAs("account_id")] string? accountId = null,
        [AliasAs("grouped_types[]")][Query(CollectionFormat.Multi)] IEnumerable<NotificationType>? groupedTypes = null);
}

public sealed class GroupedNotificationsResults {
    [JsonPropertyName("accounts")]
    public List<Account> Accounts { get; set; } = [];

    [JsonPropertyName("partial_accounts")]
    public List<PartialAccountWithAvatar>? PartialAccounts { get; set; } = null;

    [JsonPropertyName("statuses")]
    public List<Status> Statuses { get; set; } = [];

    [JsonPropertyName("notification_groups")]
    public List<NotificationGroup> NotificationGroups { get; set; } = [];
}

public sealed class NotificationGroup {
    [JsonPropertyName("group_key")]
    public string GroupKey { get; set; } = string.Empty;

    [JsonPropertyName("notifications_count")]
    public int NotificationsCount { get; set; } = 0;

    [JsonPropertyName("type")]
    public NotificationGroupType Type { get; set; } = NotificationGroupType.Mention;

    [JsonPropertyName("most_recent_notification_id")]
    public string MostRecentNotificationId { get; set; } = string.Empty;

    [JsonPropertyName("page_min_id")]
    public string? PageMinId { get; set; } = null;

    [JsonPropertyName("page_max_id")]
    public string? PageMaxId { get; set; } = null;

    [JsonPropertyName("latest_page_notification_at")]
    public DateTime? LatestPageNotificationAt { get; set; } = null;

    [JsonPropertyName("sample_account_ids")]
    public List<string> SampleAccountIds { get; set; } = [];

    [JsonPropertyName("status_id")]
    public string? StatusId { get; set; } = null;

    [JsonPropertyName("report")]
    public Report? Report { get; set; } = null;

    [JsonPropertyName("event")]
    public RelationshipSeveranceEvent? Event { get; set; } = null;

    [JsonPropertyName("moderation_warning")]
    public AccountWarning? ModerationWarning { get; set; } = null;

    [JsonPropertyName("fallback")]
    public NotificationFallback? Fallback { get; set; } = null;
}

public sealed class GroupedNotificationUnreadCount {
    [JsonPropertyName("count")]
    public int Count { get; set; } = 0;
}

public enum NotificationGroupType {
    [JsonStringEnumMemberName("mention")]
    Mention,

    [JsonStringEnumMemberName("status")]
    Status,

    [JsonStringEnumMemberName("reblog")]
    Reblog,

    [JsonStringEnumMemberName("follow")]
    Follow,

    [JsonStringEnumMemberName("follow_request")]
    FollowRequest,

    [JsonStringEnumMemberName("favourite")]
    Favourite,

    [JsonStringEnumMemberName("poll")]
    Poll,

    [JsonStringEnumMemberName("update")]
    Update,

    [JsonStringEnumMemberName("admin.sign_up")]
    AdminSignUp,

    [JsonStringEnumMemberName("admin.report")]
    AdminReport,

    [JsonStringEnumMemberName("severed_relationships")]
    SeveredRelationships,

    [JsonStringEnumMemberName("moderation_warning")]
    ModerationWarning,

    [JsonStringEnumMemberName("quote")]
    Quote,

    [JsonStringEnumMemberName("quoted_update")]
    QuotedUpdate
}
