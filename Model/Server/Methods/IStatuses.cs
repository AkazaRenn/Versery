using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IStatuses {
    [Get("/api/v1/statuses/{statusId}")]
    Task<Status> Get(string statusId);

    [Get("/api/v1/statuses/{statusId}/context")]
    Task<Context> GetContext(string statusId);

    [Get("/api/v1/statuses/{statusId}/card")]
    Task<PreviewCard> GetCard(string statusId);

    [Get("/api/v1/statuses/{statusId}/reblogged_by")]
    Task<List<Account>> GetRebloggedBy(
        string statusId,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    [Get("/api/v1/statuses/{statusId}/favourited_by")]
    Task<List<Account>> GetFavouritedBy(
        string statusId,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    [Post("/api/v1/statuses")]
    Task<Status> Post(
        [AliasAs("status")] string? status = null,
        [AliasAs("in_reply_to_id")] string? inReplyToId = null,
        [AliasAs("media_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? mediaIds = null,
        [AliasAs("sensitive")] bool? sensitive = null,
        [AliasAs("spoiler_text")] string? spoilerText = null,
        [AliasAs("visibility")] string? visibility = null,
        [AliasAs("scheduled_at")] string? scheduledAt = null,
        [AliasAs("language")] string? language = null,
        [AliasAs("poll[options][]")][Query(CollectionFormat.Multi)] IEnumerable<string>? pollOptions = null,
        [AliasAs("poll[expires_in]")] double? pollExpiresIn = null,
        [AliasAs("poll[multiple]")] bool? pollMultiple = null,
        [AliasAs("poll[hide_totals]")] bool? pollHideTotals = null);

    [Put("/api/v1/statuses/{statusId}")]
    Task<Status> Put(
        string statusId,
        [AliasAs("status")] string? status = null,
        [AliasAs("media_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? mediaIds = null,
        [AliasAs("sensitive")] bool? sensitive = null,
        [AliasAs("spoiler_text")] string? spoilerText = null,
        [AliasAs("language")] string? language = null,
        [AliasAs("poll[options][]")][Query(CollectionFormat.Multi)] IEnumerable<string>? pollOptions = null,
        [AliasAs("poll[expires_in]")] double? pollExpiresIn = null,
        [AliasAs("poll[multiple]")] bool? pollMultiple = null,
        [AliasAs("poll[hide_totals]")] bool? pollHideTotals = null);

    [Delete("/api/v1/statuses/{statusId}")]
    Task Delete(string statusId);

    [Post("/api/v1/statuses/{statusId}/reblog")]
    Task<Status> Reblog(string statusId);

    [Post("/api/v1/statuses/{statusId}/unreblog")]
    Task<Status> Unreblog(string statusId);

    [Post("/api/v1/statuses/{statusId}/favourite")]
    Task<Status> Favourite(string statusId);

    [Post("/api/v1/statuses/{statusId}/unfavourite")]
    Task<Status> Unfavourite(string statusId);

    [Post("/api/v1/statuses/{statusId}/bookmark")]
    Task<Status> Bookmark(string statusId);

    [Post("/api/v1/statuses/{statusId}/unbookmark")]
    Task<Status> Unbookmark(string statusId);

    [Post("/api/v1/statuses/{statusId}/mute")]
    Task<Status> MuteConversation(string statusId);

    [Post("/api/v1/statuses/{statusId}/unmute")]
    Task<Status> UnmuteConversation(string statusId);

    [Post("/api/v1/statuses/{statusId}/pin")]
    Task<Status> Pin(string statusId);

    [Post("/api/v1/statuses/{statusId}/unpin")]
    Task<Status> Unpin(string statusId);
}
