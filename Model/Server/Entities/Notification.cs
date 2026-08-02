using Raiqub.Generators.EnumUtilities;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Notification/">Mastodon API Documentation</see>
public sealed class Notification {
    /// <summary>
    /// Version: 0.9.9
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Notification/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Notification/#type"/>
    [JsonPropertyName("type")]
    public NotificationType Type { get; set; } = NotificationType.Mention;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Notification/#group_key"/>
    [JsonPropertyName("group_key")]
    public string GroupKey { get; set; } = string.Empty;

    /// <summary>
    /// Version: 0.9.9
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Notification/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;

    /// <summary>
    /// Version: 0.9.9
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Notification/#account"/>
    [JsonPropertyName("account")]
    public Account Account { get; set; } = new();

    /// <summary>
    /// Version: 0.9.9
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Notification/#status"/>
    [JsonPropertyName("status")]
    public Status? Status { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Notification/#report"/>
    [JsonPropertyName("report")]
    public Report? Report { get; set; } = null;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Notification/#relationship_severance_event"/>
    [JsonPropertyName("event")]
    public RelationshipSeveranceEvent? Event { get; set; } = null;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Notification/#moderation_warning"/>
    [JsonPropertyName("moderation_warning")]
    public AccountWarning? ModerationWarning { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Notification/#fallback"/>
    [JsonPropertyName("fallback")]
    public NotificationFallback? Fallback { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Notification/#collection"/>
    [JsonPropertyName("collection")]
    public Collection? Collection { get; set; } = null;
}

[JsonConverterGenerator]
public enum NotificationType {
    [JsonStringEnumMemberName("mention")]
    Mention,

    [JsonStringEnumMemberName("status")]
    Status,

    [JsonStringEnumMemberName("reblog")]
    Reblog,

    [JsonStringEnumMemberName("follow")]
    Follow,

    [JsonStringEnumMemberName("follow_request")]
    FollowRequest,

    [JsonStringEnumMemberName("favourite")]
    Favourite,

    [JsonStringEnumMemberName("poll")]
    Poll,

    [JsonStringEnumMemberName("update")]
    Update,

    [JsonStringEnumMemberName("admin.sign_up")]
    AdminSignUp,

    [JsonStringEnumMemberName("admin.report")]
    AdminReport,

    [JsonStringEnumMemberName("severed_relationships")]
    SeveredRelationships,

    [JsonStringEnumMemberName("moderation_warning")]
    ModerationWarning,

    [JsonStringEnumMemberName("quote")]
    Quote,

    [JsonStringEnumMemberName("quoted_update")]
    QuotedUpdate,

    [JsonStringEnumMemberName("added_to_collection")]
    AddedToCollection,

    [JsonStringEnumMemberName("collection_update")]
    CollectionUpdate,
}
