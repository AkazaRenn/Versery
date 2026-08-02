using Model.Server.Entities.Admin;
using Refit;
using System.Text.Json.Serialization;

namespace Model.Server.Methods.Admin;

/// <see href="https://docs.joinmastodon.org/methods/admin/accounts/">Mastodon API Documentation</see>
public interface IAccounts {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/accounts/#v1"/>
    [Get("/api/v1/admin/accounts")]
    Task<List<Account>> V1(
        [AliasAs("local")] bool? local = null,
        [AliasAs("remote")] bool? remote = null,
        [AliasAs("active")] bool? active = null,
        [AliasAs("pending")] bool? pending = null,
        [AliasAs("disabled")] bool? disabled = null,
        [AliasAs("silenced")] bool? silenced = null,
        [AliasAs("suspended")] bool? suspended = null,
        [AliasAs("sensitized")] bool? sensitized = null,
        [AliasAs("username")] string? username = null,
        [AliasAs("display_name")] string? displayName = null,
        [AliasAs("by_domain")] string? byDomain = null,
        [AliasAs("email")] string? email = null,
        [AliasAs("ip")] string? ip = null,
        [AliasAs("staff")] bool? staff = null,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/accounts/#v2"/>
    [Get("/api/v2/admin/accounts")]
    Task<List<Account>> V2(
        [AliasAs("origin")] AccountsOrigin? origin = null,
        [AliasAs("status")] AccountsStatus? status = null,
        [AliasAs("permissions")] AccountsPermissions? permissions = null,
        [AliasAs("role_ids[]")][Query(CollectionFormat.Multi)] IEnumerable<string>? roleIds = null,
        [AliasAs("invited_by")] string? invitedBy = null,
        [AliasAs("username")] string? username = null,
        [AliasAs("display_name")] string? displayName = null,
        [AliasAs("by_domain")] string? byDomain = null,
        [AliasAs("email")] string? email = null,
        [AliasAs("ip")] string? ip = null,
        [AliasAs("max_id")] string? maxId = null,
        [AliasAs("since_id")] string? sinceId = null,
        [AliasAs("min_id")] string? minId = null,
        [AliasAs("limit")] int? limit = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/accounts/#get-one"/>
    [Get("/api/v1/admin/accounts/{id}")]
    Task<Account> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/accounts/#approve"/>
    [Post("/api/v1/admin/accounts/{id}/approve")]
    Task<Account> Approve(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/accounts/#reject"/>
    [Post("/api/v1/admin/accounts/{id}/reject")]
    Task<Account> Reject(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/accounts/#delete"/>
    [Delete("/api/v1/admin/accounts/{id}")]
    Task<Account> Delete(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/accounts/#action"/>
    [Post("/api/v1/admin/accounts/{id}/action")]
    Task Action(
        string id,
        [AliasAs("type")] AccountActionType type,
        [AliasAs("report_id")] string? reportId = null,
        [AliasAs("warning_preset_id")] string? warningPresetId = null,
        [AliasAs("text")] string? text = null,
        [AliasAs("send_email_notification")] bool? sendEmailNotification = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/accounts/#enable"/>
    [Post("/api/v1/admin/accounts/{id}/enable")]
    Task<Account> Enable(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/accounts/#unsilence"/>
    [Post("/api/v1/admin/accounts/{id}/unsilence")]
    Task<Account> Unsilence(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/accounts/#unsuspend"/>
    [Post("/api/v1/admin/accounts/{id}/unsuspend")]
    Task<Account> Unsuspend(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/admin/accounts/#unsensitive"/>
    [Post("/api/v1/admin/accounts/{id}/unsensitive")]
    Task<Account> Unsensitive(string id);
}

public enum AccountsOrigin {
    [JsonStringEnumMemberName("local")]
    Local,

    [JsonStringEnumMemberName("remote")]
    Remote
}

public enum AccountsStatus {
    [JsonStringEnumMemberName("active")]
    Active,

    [JsonStringEnumMemberName("pending")]
    Pending,

    [JsonStringEnumMemberName("disabled")]
    Disabled,

    [JsonStringEnumMemberName("silenced")]
    Silenced,

    [JsonStringEnumMemberName("suspended")]
    Suspended
}

public enum AccountsPermissions {
    [JsonStringEnumMemberName("staff")]
    Staff
}

public enum AccountActionType {
    [JsonStringEnumMemberName("none")]
    None,

    [JsonStringEnumMemberName("sensitive")]
    Sensitive,

    [JsonStringEnumMemberName("disable")]
    Disable,

    [JsonStringEnumMemberName("silence")]
    Silence,

    [JsonStringEnumMemberName("suspend")]
    Suspend
}
