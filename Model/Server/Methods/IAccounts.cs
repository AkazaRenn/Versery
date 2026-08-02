using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IAccounts {
    [Get("/api/v1/accounts/{accountId}")]
    Task<Account> Get(string accountId);

    [Get("/api/v1/accounts/verify_credentials")]
    Task<Account> GetVerifyCredentials();

    [Get("/api/v1/accounts/relationships")]
    Task<List<Relationship>> GetRelationships(
        [AliasAs("id[]")][Query(CollectionFormat.Multi)] IEnumerable<string> ids);

    [Get("/api/v1/accounts/{accountId}/followers")]
    Task<List<Account>> GetFollowers(
        string accountId,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    [Get("/api/v1/accounts/{accountId}/following")]
    Task<List<Account>> GetFollowing(
        string accountId,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    [Get("/api/v1/accounts/{accountId}/statuses")]
    Task<List<Status>> GetStatuses(
        string accountId,
        [AliasAs("only_media")] bool? onlyMedia = null,
        [AliasAs("exclude_replies")] bool? excludeReplies = null,
        [AliasAs("pinned")] bool? pinned = null,
        [AliasAs("exclude_reblogs")] bool? excludeReblogs = null,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    [Post("/api/v1/accounts/{accountId}/follow")]
    Task<Relationship> Follow(
        string accountId,
        [AliasAs("reblogs")] bool? reblogs = null);

    [Post("/api/v1/accounts/{accountId}/unfollow")]
    Task<Relationship> Unfollow(string accountId);

    [Post("/api/v1/accounts/{accountId}/block")]
    Task<Relationship> Block(string accountId);

    [Post("/api/v1/accounts/{accountId}/unblock")]
    Task<Relationship> Unblock(string accountId);

    [Post("/api/v1/accounts/{accountId}/mute")]
    Task<Relationship> Mute(
        string accountId,
        [AliasAs("notifications")] bool? notifications = null);

    [Post("/api/v1/accounts/{accountId}/unmute")]
    Task<Relationship> Unmute(string accountId);

    [Post("/api/v1/accounts/{accountId}/pin")]
    Task<Relationship> Pin(string accountId);

    [Post("/api/v1/accounts/{accountId}/unpin")]
    Task<Relationship> Unpin(string accountId);

    [Get("/api/v1/accounts/{accountId}/lists")]
    Task<List<Model.Server.Entities.List>> GetLists(string accountId);

    [Multipart]
    [Patch("/api/v1/accounts/update_credentials")]
    Task<Account> UpdateCredentials(
        [AliasAs("discoverable")] string? discoverable = null,
        [AliasAs("bot")] string? bot = null,
        [AliasAs("display_name")] string? displayName = null,
        [AliasAs("note")] string? note = null,
        [AliasAs("avatar")] StreamPart? avatar = null,
        [AliasAs("header")] StreamPart? header = null,
        [AliasAs("locked")] string? locked = null,
        [AliasAs("source[privacy]")] string? sourcePrivacy = null,
        [AliasAs("source[sensitive]")] string? sourceSensitive = null,
        [AliasAs("source[language]")] string? sourceLanguage = null,
        [AliasAs("fields_attributes[][name]")] IEnumerable<string>? fieldNames = null,
        [AliasAs("fields_attributes[][value]")] IEnumerable<string>? fieldValues = null);

    [Get("/api/v1/accounts/search")]
    Task<List<Account>> GetSearch(
        [AliasAs("q")] string query,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("offset")] int? offset = null,
        [AliasAs("resolve")] bool? resolve = null,
        [AliasAs("following")] bool? following = null);
}
