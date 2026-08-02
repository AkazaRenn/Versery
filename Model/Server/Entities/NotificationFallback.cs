using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/NotificationFallback/">Mastodon API Documentation</see>
public sealed class NotificationFallback {
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("summary")]
    public string? Summary { get; set; } = null;

    [JsonPropertyName("details")]
    public string? Details { get; set; } = null;
}