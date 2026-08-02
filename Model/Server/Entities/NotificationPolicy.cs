using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class NotificationPolicy {
    [JsonPropertyName("for_not_following")]
    public string ForNotFollowing { get; set; } = string.Empty;

    [JsonPropertyName("for_not_followers")]
    public string ForNotFollowers { get; set; } = string.Empty;

    [JsonPropertyName("for_new_accounts")]
    public string ForNewAccounts { get; set; } = string.Empty;

    [JsonPropertyName("for_private_mentions")]
    public string ForPrivateMentions { get; set; } = string.Empty;

    [JsonPropertyName("for_limited_accounts")]
    public string? ForLimitedAccounts { get; set; } = null;

    [JsonPropertyName("summary")]
    public NotificationPolicySummary Summary { get; set; } = new();
}

public sealed class NotificationPolicySummary {
    [JsonPropertyName("pending_requests_count")]
    public int PendingRequestsCount { get; set; } = 0;

    [JsonPropertyName("pending_notifications_count")]
    public int PendingNotificationsCount { get; set; } = 0;
}
