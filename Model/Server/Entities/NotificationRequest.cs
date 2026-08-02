using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class NotificationRequest {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("account")]
    public Account Account { get; set; } = new();

    [JsonPropertyName("notifications_count")]
    public string NotificationsCount { get; set; } = string.Empty;

    [JsonPropertyName("last_status")]
    public Status? LastStatus { get; set; } = null;
}
