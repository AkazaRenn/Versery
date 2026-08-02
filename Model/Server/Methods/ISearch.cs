using Model.Server.Entities;
using Refit;
using System.Text.Json.Serialization;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/search/">Mastodon API Documentation</see>
public interface ISearch {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/search/#v2"/>
    [Get("/api/v2/search")]
    Task<Search> V2(
        [AliasAs("q")] string q,
        [AliasAs("type")] string? type = null,
        [AliasAs("resolve")] bool? resolve = null,
        [AliasAs("following")] bool? following = null,
        [AliasAs("account_id")] string? accountId = null,
        [AliasAs("exclude_unreviewed")] bool? excludeUnreviewed = null,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("offset")] int? offset = null);

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/search/#v1"/>
    [Get("/api/v1/search")]
    Task<SearchV1> V1(
        [AliasAs("q")] string q,
        [AliasAs("type")] string? type = null,
        [AliasAs("resolve")] bool? resolve = null,
        [AliasAs("account_id")] string? accountId = null,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("offset")] int? offset = null);
}

public sealed class SearchV1 {
    [JsonPropertyName("accounts")]
    public List<Account> Accounts { get; set; } = [];

    [JsonPropertyName("statuses")]
    public List<Status> Statuses { get; set; } = [];

    [JsonPropertyName("hashtags")]
    public List<string> Hashtags { get; set; } = [];
}
