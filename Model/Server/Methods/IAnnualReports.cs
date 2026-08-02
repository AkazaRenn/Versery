using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/annual_reports/">Mastodon API Documentation</see>
public interface IAnnualReports {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/annual_reports/#index">Mastodon API Documentation</see>
    [Get("/api/v1/annual_reports")]
    Task<WrappedAnnualReports> Index();

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/annual_reports/#get">Mastodon API Documentation</see>
    [Get("/api/v1/annual_reports/{year}")]
    Task<WrappedAnnualReports> Get(string year);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/annual_reports/#get-state">Mastodon API Documentation</see>
    [Get("/api/v1/annual_reports/{year}/state")]
    Task<AnnualReportState> GetState(string year);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/annual_reports/#read">Mastodon API Documentation</see>
    [Post("/api/v1/annual_reports/{year}/read")]
    Task Read(string year);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/annual_reports/#generate">Mastodon API Documentation</see>
    [Post("/api/v1/annual_reports/{year}/generate")]
    Task Generate(string year);
}
