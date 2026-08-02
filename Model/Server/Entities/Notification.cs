using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a notification of an event relevant to the user.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Notification/">Mastodon API Documentation</see>
public sealed class Notification {
    /// <summary>
    /// The id of the notification in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The type of event that resulted in the notification.
    /// </summary>
    [JsonPropertyName("type")]
    public NotificationType Type { get; set; } = NotificationType.Unknown;

    /// <summary>
    /// The timestamp of the notification.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The account that performed the action that generated the notification.
    /// </summary>
    [JsonPropertyName("account")]
    public Account Account { get; set; } = new Account();

    /// <summary>
    /// Status that was the object of the notification, e.g. in mentions, reblogs, favourites, or polls.
    /// </summary>
    [JsonPropertyName("status")]
    public Status? Status { get; set; }

    /// <summary>
    /// Summary of the event that caused follow relationships to be severed. Attached when type of the notification is severed_relationships.
    /// </summary>
    [JsonPropertyName("event")]
    public RelationshipSeveranceEvent? Event { get; set; }

    /// <summary>
    /// Moderation warning against the account. Attached when type of the notification is account_warning.
    /// </summary>
    [JsonPropertyName("warning")]
    public AccountWarning? Warning { get; set; }

    /// <summary>
    /// Fallback information available for some notification types that clients may not support. Only available for some notification types, and only if the supported_types parameter is used when querying.
    /// </summary>
    [JsonPropertyName("fallback")]
    public NotificationFallback? Fallback { get; set; }

    /// <summary>
    /// Collection that was the object of the notification. Attached when type of the notification is added_to_collection or collection_update.
    /// </summary>
    [JsonPropertyName("collection")]
    public Collection? Collection { get; set; }
}
