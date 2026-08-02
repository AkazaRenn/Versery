using System.Text.Json.Serialization;

namespace Model.Server.Entities.Enumerations;
/// <see href="https://docs.joinmastodon.org/entities/Notification/#types">Mastodon API Documentation</see>
public enum NotificationType {
    Unknown,

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
