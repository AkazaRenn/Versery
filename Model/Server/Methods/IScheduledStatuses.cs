using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/scheduled_statuses/">Mastodon API Documentation</see>
public interface IScheduledStatuses {
    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/scheduled_statuses/#get">Mastodon API Documentation</see>
    [Get("/api/v1/scheduled_statuses")]
    Task<List<ScheduledStatus>> Get(
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/scheduled_statuses/#get-one">Mastodon API Documentation</see>
    [Get("/api/v1/scheduled_statuses/{scheduledStatusId}")]
    Task<ScheduledStatus> GetOne(string scheduledStatusId);

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/scheduled_statuses/#update">Mastodon API Documentation</see>
    [Put("/api/v1/scheduled_statuses/{scheduledStatusId}")]
    Task<ScheduledStatus> Update(
        string scheduledStatusId,
        [AliasAs("scheduled_at")] string? scheduledAt = null);

    /// <summary>
    /// Version: 2.7.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/scheduled_statuses/#cancel">Mastodon API Documentation</see>
    [Delete("/api/v1/scheduled_statuses/{scheduledStatusId}")]
    Task Cancel(string scheduledStatusId);
}
