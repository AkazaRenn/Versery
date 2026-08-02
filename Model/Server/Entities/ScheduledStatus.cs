using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a status that will be published at a future scheduled date.
/// Version: 4.5.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/ScheduledStatus/">Mastodon API Documentation</see>
public sealed class ScheduledStatus {
    /// <summary>
    /// The ID of the scheduled status in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The timestamp for when the status will be posted.
    /// </summary>
    [JsonPropertyName("scheduled_at")]
    public DateTime ScheduledAt { get; set; } = DateTime.MinValue;

    /// <summary>
    /// The parameters that were used when scheduling the status, to be used when the status is posted.
    /// </summary>
    [JsonPropertyName("params")]
    public Status Params { get; set; } = new Status();

    /// <summary>
    /// Media that will be attached when the status is posted.
    /// </summary>
    [JsonPropertyName("media_attachments")]
    public IEnumerable<MediaAttachment>? MediaAttachments { get; set; } = null;
}
