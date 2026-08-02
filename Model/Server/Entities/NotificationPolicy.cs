using Raiqub.Generators.EnumUtilities;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/NotificationPolicy/">Mastodon API Documentation</see>
public sealed class NotificationPolicy {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationPolicy/#for_not_following"/>
    [JsonPropertyName("for_not_following")]
    public NotificationPolicyFilter ForNotFollowing { get; set; } = NotificationPolicyFilter.Accept;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationPolicy/#for_not_followers"/>
    [JsonPropertyName("for_not_followers")]
    public NotificationPolicyFilter ForNotFollowers { get; set; } = NotificationPolicyFilter.Accept;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationPolicy/#for_new_accounts"/>
    [JsonPropertyName("for_new_accounts")]
    public NotificationPolicyFilter ForNewAccounts { get; set; } = NotificationPolicyFilter.Accept;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationPolicy/#for_private_mentions"/>
    [JsonPropertyName("for_private_mentions")]
    public NotificationPolicyFilter ForPrivateMentions { get; set; } = NotificationPolicyFilter.Accept;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationPolicy/#for_limited_accounts"/>
    [JsonPropertyName("for_limited_accounts")]
    public NotificationPolicyFilter ForLimitedAccounts { get; set; } = NotificationPolicyFilter.Accept;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationPolicy/#summary"/>
    [JsonPropertyName("summary")]
    public NotificationPolicySummary Summary { get; set; } = new();
}

public sealed class NotificationPolicySummary {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationPolicy/#pending_requests_count"/>
    [JsonPropertyName("pending_requests_count")]
    public int PendingRequestsCount { get; set; } = 0;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationPolicy/#pending_notifications_count"/>
    [JsonPropertyName("pending_notifications_count")]
    public int PendingNotificationsCount { get; set; } = 0;
}

[JsonConverterGenerator]
public enum NotificationPolicyFilter {
    [JsonStringEnumMemberName("accept")]
    Accept,

    [JsonStringEnumMemberName("filter")]
    Filter,

    [JsonStringEnumMemberName("drop")]
    Drop,
}
