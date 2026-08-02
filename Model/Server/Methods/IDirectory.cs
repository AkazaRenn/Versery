using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/directory/">Mastodon API Documentation</see>
public interface IDirectory {
    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/directory/#get"/>
    [Get("/api/v1/directory")]
    Task<List<Account>> Get(
        [AliasAs("offset")] int? offset = null,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("order")] string? order = null,
        [AliasAs("local")] bool? local = null);
}
