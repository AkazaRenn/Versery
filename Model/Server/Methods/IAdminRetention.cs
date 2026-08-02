using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/retention/">Mastodon API Documentation</see>
public interface IAdminRetention {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/retention/#create">Mastodon API Documentation</see>
    [Post("/api/v1/admin/retention")]
    Task<List<AdminCohort>> Create(
        [AliasAs("start_at")] string startAt,
        [AliasAs("end_at")] string endAt,
        [AliasAs("frequency")] AdminCohortFrequency frequency);
}
