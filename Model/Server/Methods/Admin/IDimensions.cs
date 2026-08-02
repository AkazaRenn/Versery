using Refit;
using System.Text.Json.Serialization;

namespace Model.Server.Admin.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/dimensions/">Mastodon API Documentation</see>
public interface IDimensions {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/dimensions/#get"/>
    [Post("/api/v1/admin/dimensions")]
    Task<List<AdminDimension>> Get(
        [AliasAs("keys[]")][Query(CollectionFormat.Multi)] IEnumerable<AdminDimensionKey> keys,
        [AliasAs("start_at")] string startAt,
        [AliasAs("end_at")] string endAt,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("tag_servers[id]")] string? tagServersId = null,
        [AliasAs("tag_languages[id]")] string? tagLanguagesId = null,
        [AliasAs("instance_accounts[domain]")] string? instanceAccountsDomain = null,
        [AliasAs("instance_languages[domain]")] string? instanceLanguagesDomain = null);
}

public enum DimensionKey {
    [JsonStringEnumMemberName("languages")]
    Languages,

    [JsonStringEnumMemberName("sources")]
    Sources,

    [JsonStringEnumMemberName("servers")]
    Servers,

    [JsonStringEnumMemberName("space_usage")]
    SpaceUsage,

    [JsonStringEnumMemberName("software_versions")]
    SoftwareVersions,

    [JsonStringEnumMemberName("tag_servers")]
    TagServers,

    [JsonStringEnumMemberName("tag_languages")]
    TagLanguages,

    [JsonStringEnumMemberName("instance_accounts")]
    InstanceAccounts,

    [JsonStringEnumMemberName("instance_languages")]
    InstanceLanguages
}
