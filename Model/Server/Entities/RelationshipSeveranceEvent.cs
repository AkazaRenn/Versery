using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/RelationshipSeveranceEvent/">Mastodon API Documentation</see>
public sealed class RelationshipSeveranceEvent {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/RelationshipSeveranceEvent/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/RelationshipSeveranceEvent/#type"/>
    [JsonPropertyName("type")]
    public RelationshipSeveranceEventType Type { get; set; } = RelationshipSeveranceEventType.DomainBlock;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/RelationshipSeveranceEvent/#purged"/>
    [JsonPropertyName("purged")]
    public bool Purged { get; set; } = false;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/RelationshipSeveranceEvent/#target_name"/>
    [JsonPropertyName("target_name")]
    public string TargetName { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/RelationshipSeveranceEvent/#followers_count"/>
    [JsonPropertyName("followers_count")]
    public int FollowersCount { get; set; } = 0;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/RelationshipSeveranceEvent/#following_count"/>
    [JsonPropertyName("following_count")]
    public int FollowingCount { get; set; } = 0;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/RelationshipSeveranceEvent/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;
}

public enum RelationshipSeveranceEventType {
    [JsonStringEnumMemberName("domain_block")]
    DomainBlock,

    [JsonStringEnumMemberName("user_domain_block")]
    UserDomainBlock,

    [JsonStringEnumMemberName("account_suspension")]
    AccountSuspension,
}
