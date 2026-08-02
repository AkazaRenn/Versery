using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/statuses/">Mastodon API Documentation</see>
public interface IStatuses {
    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#get">Mastodon API Documentation</see>
    [Get("/api/v1/statuses/{statusId}")]
    Task<Status> Get(string statusId);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#index">Mastodon API Documentation</see>
    [Get("/api/v1/statuses")]
    Task<List<Status>> Index([AliasAs("id[]")][Query(CollectionFormat.Multi)] IEnumerable<string> ids);

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#context">Mastodon API Documentation</see>
    [Get("/api/v1/statuses/{statusId}/context")]
    Task<Context> Context(string statusId);

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#card">Mastodon API Documentation</see>
    [Get("/api/v1/statuses/{statusId}/card")]
    Task<PreviewCard> Card(string statusId);

    /// <summary>
    /// Version: 0.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#reblogged_by">Mastodon API Documentation</see>
    [Get("/api/v1/statuses/{statusId}/reblogged_by")]
    Task<List<Account>> RebloggedBy(
        string statusId,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 0.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#favourited_by">Mastodon API Documentation</see>
    [Get("/api/v1/statuses/{statusId}/favourited_by")]
    Task<List<Account>> FavouritedBy(
        string statusId,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#create">Mastodon API Documentation</see>
    [Post("/api/v1/statuses")]
    Task<Status> Create(
        [AliasAs("status")] string? status = null,
        [AliasAs("media_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? mediaIds = null,
        [AliasAs("poll[options][]")][Query(CollectionFormat.Multi)] IEnumerable<string>? pollOptions = null,
        [AliasAs("poll[expires_in]")] double? pollExpiresIn = null,
        [AliasAs("poll[multiple]")] bool? pollMultiple = null,
        [AliasAs("poll[hide_totals]")] bool? pollHideTotals = null,
        [AliasAs("in_reply_to_id")] string? inReplyToId = null,
        [AliasAs("sensitive")] bool? sensitive = null,
        [AliasAs("spoiler_text")] string? spoilerText = null,
        [AliasAs("visibility")] string? visibility = null,
        [AliasAs("language")] string? language = null,
        [AliasAs("scheduled_at")] string? scheduledAt = null,
        [AliasAs("quoted_status_id")] string? quotedStatusId = null,
        [AliasAs("quote_approval_policy")] string? quoteApprovalPolicy = null);

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#edit">Mastodon API Documentation</see>
    [Put("/api/v1/statuses/{statusId}")]
    Task<Status> Edit(
        string statusId,
        [AliasAs("status")] string? status = null,
        [AliasAs("spoiler_text")] string? spoilerText = null,
        [AliasAs("sensitive")] bool? sensitive = null,
        [AliasAs("language")] string? language = null,
        [AliasAs("media_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? mediaIds = null,
        [AliasAs("media_attributes[][id]")][Query(CollectionFormat.Multi)] IEnumerable<string>? mediaAttributeIds = null,
        [AliasAs("media_attributes[][description]")][Query(CollectionFormat.Multi)] IEnumerable<string>? mediaAttributeDescriptions = null,
        [AliasAs("media_attributes[][focus]")][Query(CollectionFormat.Multi)] IEnumerable<string>? mediaAttributeFocuses = null,
        [AliasAs("poll[options][]")][Query(CollectionFormat.Multi)] IEnumerable<string>? pollOptions = null,
        [AliasAs("poll[expires_in]")] double? pollExpiresIn = null,
        [AliasAs("poll[multiple]")] bool? pollMultiple = null,
        [AliasAs("poll[hide_totals]")] bool? pollHideTotals = null,
        [AliasAs("quote_approval_policy")] string? quoteApprovalPolicy = null);

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#edit_interaction_policy">Mastodon API Documentation</see>
    [Put("/api/v1/statuses/{statusId}/interaction_policy")]
    Task<Status> EditInteractionPolicy(
        string statusId,
        [AliasAs("quote_approval_policy")] string quoteApprovalPolicy);

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#delete">Mastodon API Documentation</see>
    [Delete("/api/v1/statuses/{statusId}")]
    Task<Status> Delete(string statusId, [AliasAs("delete_media")] bool? deleteMedia = null);

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#boost">Mastodon API Documentation</see>
    [Post("/api/v1/statuses/{statusId}/reblog")]
    Task<Status> Boost(string statusId, [AliasAs("visibility")] string? visibility = null);

    /// <summary>
    /// Version: 0.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#unreblog">Mastodon API Documentation</see>
    [Post("/api/v1/statuses/{statusId}/unreblog")]
    Task<Status> Unreblog(string statusId);

    /// <summary>
    /// Version: 0.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#favourite">Mastodon API Documentation</see>
    [Post("/api/v1/statuses/{statusId}/favourite")]
    Task<Status> Favourite(string statusId);

    /// <summary>
    /// Version: 0.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#unfavourite">Mastodon API Documentation</see>
    [Post("/api/v1/statuses/{statusId}/unfavourite")]
    Task<Status> Unfavourite(string statusId);

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#bookmark">Mastodon API Documentation</see>
    [Post("/api/v1/statuses/{statusId}/bookmark")]
    Task<Status> Bookmark(string statusId);

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#unbookmark">Mastodon API Documentation</see>
    [Post("/api/v1/statuses/{statusId}/unbookmark")]
    Task<Status> Unbookmark(string statusId);

    /// <summary>
    /// Version: 1.4.2
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#mute">Mastodon API Documentation</see>
    [Post("/api/v1/statuses/{statusId}/mute")]
    Task<Status> Mute(string statusId);

    /// <summary>
    /// Version: 1.4.2
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#unmute">Mastodon API Documentation</see>
    [Post("/api/v1/statuses/{statusId}/unmute")]
    Task<Status> Unmute(string statusId);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#pin">Mastodon API Documentation</see>
    [Post("/api/v1/statuses/{statusId}/pin")]
    Task<Status> Pin(string statusId);

    /// <summary>
    /// Version: 1.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#unpin">Mastodon API Documentation</see>
    [Post("/api/v1/statuses/{statusId}/unpin")]
    Task<Status> Unpin(string statusId);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#history">Mastodon API Documentation</see>
    [Get("/api/v1/statuses/{statusId}/history")]
    Task<List<StatusEdit>> History(string statusId);

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#source">Mastodon API Documentation</see>
    [Get("/api/v1/statuses/{statusId}/source")]
    Task<StatusSource> Source(string statusId);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#translate">Mastodon API Documentation</see>
    [Post("/api/v1/statuses/{statusId}/translate")]
    Task<Translation> Translate(string statusId, [AliasAs("lang")] string? lang = null);

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#quotes">Mastodon API Documentation</see>
    [Get("/api/v1/statuses/{statusId}/quotes")]
    Task<List<Status>> Quotes(
        string statusId,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/statuses/#revoke_quote">Mastodon API Documentation</see>
    [Post("/api/v1/statuses/{statusId}/quotes/{quotingStatusId}/revoke")]
    Task<Status> RevokeQuote(string statusId, string quotingStatusId);
}
