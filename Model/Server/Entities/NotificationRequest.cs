using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/NotificationRequest/">Mastodon API Documentation</see>
public sealed class NotificationRequest {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationRequest/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationRequest/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationRequest/#updated_at"/>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = default;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationRequest/#account"/>
    [JsonPropertyName("account")]
    public Account Account { get; set; } = new();

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationRequest/#notifications_count"/>
    [JsonPropertyName("notifications_count")]
    public string NotificationsCount { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationRequest/#last_status"/>
    [JsonPropertyName("last_status")]
    public Status? LastStatus { get; set; } = null;
}
