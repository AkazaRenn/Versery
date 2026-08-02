using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

public interface IScheduledStatuses {
    [Get("/api/v1/scheduled_statuses")]
    Task<List<ScheduledStatus>> Get();

    [Get("/api/v1/scheduled_statuses/{scheduledStatusId}")]
    Task<ScheduledStatus> Get(string scheduledStatusId);

    [Put("/api/v1/scheduled_statuses/{scheduledStatusId}")]
    Task<ScheduledStatus> Put(
        string scheduledStatusId,
        [AliasAs("scheduled_at")] string? scheduledAt = null);

    [Delete("/api/v1/scheduled_statuses/{scheduledStatusId}")]
    Task Delete(string scheduledStatusId);
}
