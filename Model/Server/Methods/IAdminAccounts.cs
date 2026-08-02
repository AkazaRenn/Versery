using Model.Server.Entities;
using Refit;
using System.Text.Json.Serialization;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/admin/accounts/">Mastodon API Documentation</see>
public interface IAdminAccounts {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/accounts/#v1">Mastodon API Documentation</see>
    [Get("/api/v1/admin/accounts")]
    Task<List<AdminAccount>> V1(
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
    /// <see href="https://docs.joinmastodon.org/methods/admin/accounts/#v2">Mastodon API Documentation</see>
    [Get("/api/v2/admin/accounts")]
    Task<List<AdminAccount>> V2(
        [AliasAs("origin")] AdminAccountsOrigin? origin = null,
        [AliasAs("status")] AdminAccountsStatus? status = null,
        [AliasAs("permissions")] AdminAccountsPermissions? permissions = null,
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
    /// <see href="https://docs.joinmastodon.org/methods/admin/accounts/#get-one">Mastodon API Documentation</see>
    [Get("/api/v1/admin/accounts/{id}")]
    Task<AdminAccount> GetOne(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/accounts/#approve">Mastodon API Documentation</see>
    [Post("/api/v1/admin/accounts/{id}/approve")]
    Task<AdminAccount> Approve(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/accounts/#reject">Mastodon API Documentation</see>
    [Post("/api/v1/admin/accounts/{id}/reject")]
    Task<AdminAccount> Reject(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/accounts/#delete">Mastodon API Documentation</see>
    [Delete("/api/v1/admin/accounts/{id}")]
    Task<AdminAccount> Delete(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/accounts/#action">Mastodon API Documentation</see>
    [Post("/api/v1/admin/accounts/{id}/action")]
    Task Action(
        string id,
        [AliasAs("type")] AdminAccountActionType type,
        [AliasAs("report_id")] string? reportId = null,
        [AliasAs("warning_preset_id")] string? warningPresetId = null,
        [AliasAs("text")] string? text = null,
        [AliasAs("send_email_notification")] bool? sendEmailNotification = null);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/accounts/#enable">Mastodon API Documentation</see>
    [Post("/api/v1/admin/accounts/{id}/enable")]
    Task<AdminAccount> Enable(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/accounts/#unsilence">Mastodon API Documentation</see>
    [Post("/api/v1/admin/accounts/{id}/unsilence")]
    Task<AdminAccount> Unsilence(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/accounts/#unsuspend">Mastodon API Documentation</see>
    [Post("/api/v1/admin/accounts/{id}/unsuspend")]
    Task<AdminAccount> Unsuspend(string id);

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/methods/admin/accounts/#unsensitive">Mastodon API Documentation</see>
    [Post("/api/v1/admin/accounts/{id}/unsensitive")]
    Task<AdminAccount> Unsensitive(string id);
}

public enum AdminAccountsOrigin {
    [JsonStringEnumMemberName("local")]
    Local,

    [JsonStringEnumMemberName("remote")]
    Remote
}

public enum AdminAccountsStatus {
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

public enum AdminAccountsPermissions {
    [JsonStringEnumMemberName("staff")]
    Staff
}

public enum AdminAccountActionType {
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
