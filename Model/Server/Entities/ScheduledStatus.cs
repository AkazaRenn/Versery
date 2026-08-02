using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/ScheduledStatus/">Mastodon API Documentation</see>
public sealed class ScheduledStatus {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("scheduled_at")]
    public DateTime ScheduledAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("params")]
    public Status Params { get; set; } = new Status();

    [JsonPropertyName("media_attachments")]
    public IEnumerable<MediaAttachment>? MediaAttachments { get; set; } = null;
}
