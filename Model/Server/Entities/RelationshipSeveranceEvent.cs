using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a relationship severance event.
/// Version: 4.3.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/RelationshipSeveranceEvent/">Mastodon API Documentation</see>
public sealed class RelationshipSeveranceEvent {
    /// <summary>
    /// The ID of the relationship severance event in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Type of event.
    /// </summary>
    [JsonPropertyName("type")]
    public RelationshipSeveranceEventType Type { get; set; } = default;

    /// <summary>
    /// Whether the list of severed relationships is unavailable because the underlying issue has been purged.
    /// </summary>
    [JsonPropertyName("purged")]
    public bool Purged { get; set; } = false;

    /// <summary>
    /// Name of the target of the moderation/block event. This is either a domain name or a user handle, depending on the event type.
    /// </summary>
    [JsonPropertyName("target_name")]
    public string TargetName { get; set; } = string.Empty;

    /// <summary>
    /// Number of followers that were removed as result of the event.
    /// </summary>
    [JsonPropertyName("followers_count")]
    public int FollowersCount { get; set; } = 0;

    /// <summary>
    /// Number of accounts the user stopped following as result of the event.
    /// </summary>
    [JsonPropertyName("following_count")]
    public int FollowingCount { get; set; } = 0;

    /// <summary>
    /// When the event took place.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
}
