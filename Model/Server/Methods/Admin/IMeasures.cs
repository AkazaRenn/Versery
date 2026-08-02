using Refit;
using System.Text.Json.Serialization;

namespace Model.Server.Admin.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/measures/">Mastodon API Documentation</see>
public interface IMeasures {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/measures/#get"/>
    [Post("/api/v1/admin/measures")]
    Task<List<AdminMeasure>> Get(
        [AliasAs("keys[]")][Query(CollectionFormat.Multi)] IEnumerable<AdminMeasureKey> keys,
        [AliasAs("start_at")] string startAt,
        [AliasAs("end_at")] string endAt,
        [AliasAs("tag_accounts[id]")] string? tagAccountsId = null,
        [AliasAs("tag_uses[id]")] string? tagUsesId = null,
        [AliasAs("tag_servers[id]")] string? tagServersId = null,
        [AliasAs("instance_accounts[domain]")] string? instanceAccountsDomain = null,
        [AliasAs("instance_media_attachments[domain]")] string? instanceMediaAttachmentsDomain = null,
        [AliasAs("instance_reports[domain]")] string? instanceReportsDomain = null,
        [AliasAs("instance_statuses[domain]")] string? instanceStatusesDomain = null,
        [AliasAs("instance_follows[domain]")] string? instanceFollowsDomain = null,
        [AliasAs("instance_followers[domain]")] string? instanceFollowersDomain = null);
}

public enum MeasureKey {
    [JsonStringEnumMemberName("active_users")]
    ActiveUsers,

    [JsonStringEnumMemberName("new_users")]
    NewUsers,

    [JsonStringEnumMemberName("interactions")]
    Interactions,

    [JsonStringEnumMemberName("opened_reports")]
    OpenedReports,

    [JsonStringEnumMemberName("resolved_reports")]
    ResolvedReports,

    [JsonStringEnumMemberName("tag_accounts")]
    TagAccounts,

    [JsonStringEnumMemberName("tag_uses")]
    TagUses,

    [JsonStringEnumMemberName("tag_servers")]
    TagServers,

    [JsonStringEnumMemberName("instance_accounts")]
    InstanceAccounts,

    [JsonStringEnumMemberName("instance_media_attachments")]
    InstanceMediaAttachments,

    [JsonStringEnumMemberName("instance_reports")]
    InstanceReports,

    [JsonStringEnumMemberName("instance_statuses")]
    InstanceStatuses,

    [JsonStringEnumMemberName("instance_follows")]
    InstanceFollows,

    [JsonStringEnumMemberName("instance_followers")]
    InstanceFollowers
}
