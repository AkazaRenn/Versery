using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Notification/">Mastodon API Documentation</see>
public sealed class Notification {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public NotificationType Type { get; set; } = NotificationType.Unknown;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    [JsonPropertyName("account")]
    public Account Account { get; set; } = new Account();

    [JsonPropertyName("status")]
    public Status? Status { get; set; }

    [JsonPropertyName("event")]
    public RelationshipSeveranceEvent? Event { get; set; }

    [JsonPropertyName("warning")]
    public AccountWarning? Warning { get; set; }

    [JsonPropertyName("fallback")]
    public NotificationFallback? Fallback { get; set; }

    [JsonPropertyName("collection")]
    public Collection? Collection { get; set; }
}
