using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/RelationshipSeveranceEvent/">Mastodon API Documentation</see>
public sealed class RelationshipSeveranceEvent {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public RelationshipSeveranceEventType Type { get; set; } = default;

    [JsonPropertyName("purged")]
    public bool Purged { get; set; } = false;

    [JsonPropertyName("target_name")]
    public string TargetName { get; set; } = string.Empty;

    [JsonPropertyName("followers_count")]
    public int FollowersCount { get; set; } = 0;

    [JsonPropertyName("following_count")]
    public int FollowingCount { get; set; } = 0;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
}
