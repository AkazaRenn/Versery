using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/suggestions/">Mastodon API Documentation</see>
public interface ISuggestions {
    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/suggestions/#v1"/>
    [Get("/api/v1/suggestions")]
    Task<List<Account>> V1([AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/suggestions/#v2"/>
    [Get("/api/v2/suggestions")]
    Task<List<Suggestion>> V2([AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 2.4.3
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/suggestions/#remove"/>
    [Delete("/api/v1/suggestions/{accountId}")]
    Task Remove(string accountId);
}
