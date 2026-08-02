using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/profile/">Mastodon API Documentation</see>
public interface IProfile {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/profile/#get">Mastodon API Documentation</see>
    [Get("/api/v1/profile")]
    Task<Profile> Get();

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/profile/#update">Mastodon API Documentation</see>
    [Multipart]
    [Patch("/api/v1/profile")]
    Task<Profile> Update(
        [AliasAs("display_name")] string? displayName = null,
        [AliasAs("note")] string? note = null,
        [AliasAs("avatar")] StreamPart? avatar = null,
        [AliasAs("avatar_description")] string? avatarDescription = null,
        [AliasAs("header")] StreamPart? header = null,
        [AliasAs("header_description")] string? headerDescription = null,
        [AliasAs("locked")] bool? locked = null,
        [AliasAs("bot")] bool? bot = null,
        [AliasAs("discoverable")] bool? discoverable = null,
        [AliasAs("hide_collections")] bool? hideCollections = null,
        [AliasAs("indexable")] bool? indexable = null,
        [AliasAs("show_media")] bool? showMedia = null,
        [AliasAs("show_media_replies")] bool? showMediaReplies = null,
        [AliasAs("show_featured")] bool? showFeatured = null,
        [AliasAs("attribution_domains[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? attributionDomains = null,
        [AliasAs("fields_attributes[][name]")][Query(CollectionFormat.Multi)] IEnumerable<string>? fieldNames = null,
        [AliasAs("fields_attributes[][value]")][Query(CollectionFormat.Multi)] IEnumerable<string>? fieldValues = null);

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/profile/#delete-avatar">Mastodon API Documentation</see>
    [Delete("/api/v1/profile/avatar")]
    Task<CredentialAccount> DeleteAvatar();

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/profile/#delete-header">Mastodon API Documentation</see>
    [Delete("/api/v1/profile/header")]
    Task<CredentialAccount> DeleteHeader();
}
