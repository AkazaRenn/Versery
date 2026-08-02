using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/scheduled_statuses/">Mastodon API Documentation</see>
public interface IScheduledStatuses {
    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/scheduled_statuses/#get"/>
    [Get("/api/v1/scheduled_statuses")]
    Task<List<ScheduledStatus>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/scheduled_statuses/#get-one"/>
    [Get("/api/v1/scheduled_statuses/{scheduledStatusId}")]
    Task<ScheduledStatus> GetOne(string scheduledStatusId);

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/scheduled_statuses/#update"/>
    [Put("/api/v1/scheduled_statuses/{scheduledStatusId}")]
    Task<ScheduledStatus> Update(
        string scheduledStatusId,
        [AliasAs("scheduled_at")] DateTime? scheduledAt = null);

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/scheduled_statuses/#cancel"/>
    [Delete("/api/v1/scheduled_statuses/{scheduledStatusId}")]
    Task Cancel(string scheduledStatusId);
}
