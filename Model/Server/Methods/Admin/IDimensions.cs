using Model.Server.Entities.Admin;
using Raiqub.Generators.EnumUtilities;
using Refit;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Methods.Admin;

/// <see href="https://docs.joinmastodon.org/methods/admin/dimensions/">Mastodon API Documentation</see>
public interface IDimensions {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/dimensions/#get"/>
    [Post("/api/v1/admin/dimensions")]
    Task<List<Dimension>> Get(
        [AliasAs("keys[]")][Query(CollectionFormat.Multi)] IEnumerable<DimensionKey> keys,
        [AliasAs("start_at")] DateTime startAt,
        [AliasAs("end_at")] DateTime endAt,
        [AliasAs("limit")] int? limit = null,
        [AliasAs("tag_servers[id]")] string? tagServersId = null,
        [AliasAs("tag_languages[id]")] CultureInfo? tagLanguagesId = null,
        [AliasAs("instance_accounts[domain]")] string? instanceAccountsDomain = null,
        [AliasAs("instance_languages[domain]")] string? instanceLanguagesDomain = null);
}

[JsonConverterGenerator]
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
