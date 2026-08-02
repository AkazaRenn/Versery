using Model.Server.Entities.Admin;
using Refit;

namespace Model.Server.Methods.Admin;

/// <see href="https://docs.joinmastodon.org/methods/admin/retention/">Mastodon API Documentation</see>
public interface IRetention {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/retention/#create"/>
    [Post("/api/v1/admin/retention")]
    Task<List<Cohort>> Create(
        [AliasAs("start_at")] string startAt,
        [AliasAs("end_at")] string endAt,
        [AliasAs("frequency")] CohortFrequency frequency);
}
