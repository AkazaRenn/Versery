using Model.Server.Entities;
using Model.Server.Entities.Enumerations;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/accounts/">Mastodon API Documentation</see>
public interface IAccounts {
    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#create">Mastodon API Documentation</see>
    [Post("/api/v1/accounts")]
    Task<Token> Create(
        [AliasAs("username")] string username,
        [AliasAs("email")] string email,
        [AliasAs("password")] string password,
        [AliasAs("agreement")] bool agreement,
        [AliasAs("locale")] string locale,
        [AliasAs("reason")] string? reason = null,
        [AliasAs("date_of_birth")] string? dateOfBirth = null);

    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#get">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/{id}")]
    Task<Account> Get(string id);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#index">Mastodon API Documentation</see>
    [Get("/api/v1/accounts")]
    Task<List<Account>> Index(
        [AliasAs("id[]")][Query(CollectionFormat.Multi)] IEnumerable<string> id);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#verify_credentials">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/verify_credentials")]
    Task<Account> VerifyCredentials();

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#relationships">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/relationships")]
    Task<List<Relationship>> Relationships(
        [AliasAs("id[]")][Query(CollectionFormat.Multi)] IEnumerable<string> id,
        [AliasAs("with_suspended")] bool? withSuspended = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#followers">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/{id}/followers")]
    Task<List<Account>> Followers(
        string id,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#following">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/{id}/following")]
    Task<List<Account>> Following(
        string id,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#statuses">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/{id}/statuses")]
    Task<List<Status>> Statuses(
        string id,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("only_media")] bool? onlyMedia = null,
        [AliasAs("exclude_replies")] bool? excludeReplies = null,
        [AliasAs("exclude_reblogs")] bool? excludeReblogs = null,
        [AliasAs("pinned")] bool? pinned = null,
        [AliasAs("tagged")] string? tagged = null);

    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#featured_tags">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/{id}/featured_tags")]
    Task<List<FeaturedTag>> FeaturedTags(string id);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#remove_from_followers">Mastodon API Documentation</see>
    [Post("/api/v1/accounts/{id}/remove_from_followers")]
    Task<Relationship> RemoveFromFollowers(string id);

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#endorsements">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/{id}/endorsements")]
    Task<List<Account>> Endorsements(
        string id,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#endorse">Mastodon API Documentation</see>
    [Post("/api/v1/accounts/{id}/endorse")]
    Task<Relationship> Endorse(string id);

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#unendorse">Mastodon API Documentation</see>
    [Post("/api/v1/accounts/{id}/unendorse")]
    Task<Relationship> Unendorse(string id);

    /// <summary>
    /// Version: 3.2.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#note">Mastodon API Documentation</see>
    [Post("/api/v1/accounts/{id}/note")]
    Task<Relationship> Note(
        string id,
        [AliasAs("comment")] string? comment = null);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#familiar_followers">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/familiar_followers")]
    Task<List<FamiliarFollowers>> FamiliarFollowers(
        [AliasAs("id[]")][Query(CollectionFormat.Multi)] IEnumerable<string> id);

    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#lookup">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/lookup")]
    Task<Account> Lookup([AliasAs("acct")] string acct);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#identity_proofs">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/{id}/identity_proofs")]
    Task<List<IdentityProof>> IdentityProofs(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#follow">Mastodon API Documentation</see>
    [Post("/api/v1/accounts/{id}/follow")]
    Task<Relationship> Follow(
        string id,
        [AliasAs("reblogs")] bool? reblogs = null,
        [AliasAs("notify")] bool? notify = null,
        [AliasAs("languages[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? languages = null);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#unfollow">Mastodon API Documentation</see>
    [Post("/api/v1/accounts/{id}/unfollow")]
    Task<Relationship> Unfollow(string id);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#block">Mastodon API Documentation</see>
    [Post("/api/v1/accounts/{id}/block")]
    Task<Relationship> Block(string id);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#unblock">Mastodon API Documentation</see>
    [Post("/api/v1/accounts/{id}/unblock")]
    Task<Relationship> Unblock(string id);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#mute">Mastodon API Documentation</see>
    [Post("/api/v1/accounts/{id}/mute")]
    Task<Relationship> Mute(
        string id,
        [AliasAs("notifications")] bool? notifications = null,
        [AliasAs("duration")] int? duration = null);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#unmute">Mastodon API Documentation</see>
    [Post("/api/v1/accounts/{id}/unmute")]
    Task<Relationship> Unmute(string id);

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#pin">Mastodon API Documentation</see>
    [Post("/api/v1/accounts/{id}/pin")]
    Task<Relationship> Pin(string id);

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#unpin">Mastodon API Documentation</see>
    [Post("/api/v1/accounts/{id}/unpin")]
    Task<Relationship> Unpin(string id);

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#lists">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/{id}/lists")]
    Task<List<Model.Server.Entities.List>> Lists(string id);

    /// <summary>
    /// Version: 4.6.1
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#update_credentials">Mastodon API Documentation</see>
    [Multipart]
    [Patch("/api/v1/accounts/update_credentials")]
    Task<Account> UpdateCredentials(
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
        [AliasAs("attribution_domains[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? attributionDomains = null,
        [AliasAs("fields_attributes[][name]")] IEnumerable<string>? fieldNames = null,
        [AliasAs("fields_attributes[][value]")] IEnumerable<string>? fieldValues = null,
        [AliasAs("source[privacy]")] StatusVisibility? sourcePrivacy = null,
        [AliasAs("source[sensitive]")] bool? sourceSensitive = null,
        [AliasAs("source[language]")] string? sourceLanguage = null,
        [AliasAs("source[quote_policy]")] QuotePolicy? sourceQuotePolicy = null);

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/accounts/#search">Mastodon API Documentation</see>
    [Get("/api/v1/accounts/search")]
    Task<List<Account>> Search(
        [AliasAs("q")] string q,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("offset")] int? offset = null,
        [AliasAs("resolve")] bool? resolve = null,
        [AliasAs("following")] bool? following = null);
}
