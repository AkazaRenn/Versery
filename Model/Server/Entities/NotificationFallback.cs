using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/NotificationFallback/">Mastodon API Documentation</see>
public sealed class NotificationFallback {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationFallback/#title"/>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationFallback/#summary"/>
    [JsonPropertyName("summary")]
    public string? Summary { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/NotificationFallback/#details"/>
    [JsonPropertyName("details")]
    public string? Details { get; set; } = null;
}