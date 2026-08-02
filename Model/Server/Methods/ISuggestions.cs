using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/suggestions/">Mastodon API Documentation</see>
public interface ISuggestions {
    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/suggestions/#v1">Mastodon API Documentation</see>
    [Get("/api/v1/suggestions")]
    Task<List<Account>> V1([AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/suggestions/#v2">Mastodon API Documentation</see>
    [Get("/api/v2/suggestions")]
    Task<List<Suggestion>> V2([AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 2.4.3
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/suggestions/#remove">Mastodon API Documentation</see>
    [Delete("/api/v1/suggestions/{accountId}")]
    Task Remove(string accountId);
}
