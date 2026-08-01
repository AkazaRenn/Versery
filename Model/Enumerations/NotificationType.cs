using System.Text.Json.Serialization;

namespace Model.Enumerations;
/// <summary>
/// Represents the types of notifications an account can receive.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Notification/#types">Mastodon API Documentation</see>
public enum NotificationType {
    /// <summary>
    /// Someone mentioned you in their status.
    /// </summary>
    [JsonStringEnumMemberName("mention")]
    Mention,

    /// <summary>
    /// Someone you enabled notifications for has posted a status.
    /// </summary>
    [JsonStringEnumMemberName("status")]
    Status,

    /// <summary>
    /// Someone boosted one of your statuses.
    /// </summary>
    [JsonStringEnumMemberName("reblog")]
    Reblog,

    /// <summary>
    /// Someone followed you.
    /// </summary>
    [JsonStringEnumMemberName("follow")]
    Follow,

    /// <summary>
    /// Someone requested to follow you.
    /// </summary>
    [JsonStringEnumMemberName("follow_request")]
    FollowRequest,

    /// <summary>
    /// Someone favourited one of your statuses.
    /// </summary>
    [JsonStringEnumMemberName("favourite")]
    Favourite,

    /// <summary>
    /// A poll you have voted in or created has ended.
    /// </summary>
    [JsonStringEnumMemberName("poll")]
    Poll,

    /// <summary>
    /// A status you reblogged has been edited.
    /// </summary>
    [JsonStringEnumMemberName("update")]
    Update,

    /// <summary>
    /// Someone signed up (optionally sent to admins).
    /// </summary>
    [JsonStringEnumMemberName("admin.sign_up")]
    AdminSignUp,

    /// <summary>
    /// A new report has been filed.
    /// </summary>
    [JsonStringEnumMemberName("admin.report")]
    AdminReport,

    /// <summary>
    /// Some of your follow relationships have been severed as a result of a moderation or block event.
    /// </summary>
    [JsonStringEnumMemberName("severed_relationships")]
    SeveredRelationships,

    /// <summary>
    /// A moderator has taken action against your account or has sent you a warning.
    /// </summary>
    [JsonStringEnumMemberName("moderation_warning")]
    ModerationWarning,

    /// <summary>
    /// Someone has quoted one of your statuses.
    /// </summary>
    [JsonStringEnumMemberName("quote")]
    Quote,

    /// <summary>
    /// A status you have quoted has been edited.
    /// </summary>
    [JsonStringEnumMemberName("quoted_update")]
    QuotedUpdate,

    /// <summary>
    /// Someone added you to a Collection.
    /// </summary>
    [JsonStringEnumMemberName("added_to_collection")]
    AddedToCollection,

    /// <summary>
    /// A Collection you are featured in was updated.
    /// </summary>
    [JsonStringEnumMemberName("collection_update")]
    CollectionUpdate,
}
