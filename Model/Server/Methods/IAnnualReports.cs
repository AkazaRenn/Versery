using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/annual_reports/">Mastodon API Documentation</see>
public interface IAnnualReports {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/annual_reports/#index"/>
    [Get("/api/v1/annual_reports")]
    Task<WrappedAnnualReports> Index();

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/annual_reports/#get"/>
    [Get("/api/v1/annual_reports/{year}")]
    Task<WrappedAnnualReports> Get(int year);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/annual_reports/#get-state"/>
    [Get("/api/v1/annual_reports/{year}/state")]
    Task<AnnualReportState> GetState(int year);

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/annual_reports/#read"/>
    [Post("/api/v1/annual_reports/{year}/read")]
    Task Read(int year);

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/annual_reports/#generate"/>
    [Post("/api/v1/annual_reports/{year}/generate")]
    Task Generate(int year);
}
