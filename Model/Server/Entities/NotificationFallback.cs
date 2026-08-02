using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a notification of an event relevant to the user.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/NotificationFallback/">Mastodon API Documentation</see>
public sealed class NotificationFallback {
    /// <summary>
    /// Localized fallback title for the notification, for instance “Alice added you to a collection”.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Localized fallback summary for the notification, for instance “You’re on an app that does not support the most recent version of Mastodon. Sign in to the Mastodon web app for full functionality.”
    /// </summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; set; } = null;

    /// <summary>
    /// Localized details for the notifications, to be displayed when clicking the notification, for instance.
    /// </summary>
    [JsonPropertyName("details")]
    public string? Details { get; set; } = null;
}