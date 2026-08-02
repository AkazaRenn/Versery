using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/V1_NotificationPolicy/">Mastodon API Documentation</see>
public sealed class V1NotificationPolicy {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_NotificationPolicy/#filter_not_following"/>
    [JsonPropertyName("filter_not_following")]
    public bool FilterNotFollowing { get; set; } = false;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_NotificationPolicy/#filter_not_followers"/>
    [JsonPropertyName("filter_not_followers")]
    public bool FilterNotFollowers { get; set; } = false;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_NotificationPolicy/#filter_new_accounts"/>
    [JsonPropertyName("filter_new_accounts")]
    public bool FilterNewAccounts { get; set; } = false;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_NotificationPolicy/#filter_private_mentions"/>
    [JsonPropertyName("filter_private_mentions")]
    public bool FilterPrivateMentions { get; set; } = false;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_NotificationPolicy/#summary"/>
    [JsonPropertyName("summary")]
    public V1NotificationPolicySummary Summary { get; set; } = new();
}

public sealed class V1NotificationPolicySummary {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_NotificationPolicy/#pending_requests_count"/>
    [JsonPropertyName("pending_requests_count")]
    public int PendingRequestsCount { get; set; } = 0;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_NotificationPolicy/#pending_notifications_count"/>
    [JsonPropertyName("pending_notifications_count")]
    public int PendingNotificationsCount { get; set; } = 0;
}
