using System.Text.Json.Serialization;

namespace Model.Server.Methods;
/// <summary>
/// Defining what you have permission to do with the API
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/api/oauth-scopes/">Mastodon API Documentation</see>
public enum Scope {
    /// <summary>
    /// Grants access to only minimal information about the currently authenticated user.
    /// </summary>
    [JsonStringEnumMemberName("profile")]
    Profile,

    /// <summary>
    /// Grants access to Web Push API subscriptions. Added in Mastodon 2.4.0.
    /// </summary>
    [JsonStringEnumMemberName("push")]
    Push,

    /// <summary>
    /// Grants access to read data, including other users. Requesting read will also grant granular scopes shown in the right column of the table below.
    /// </summary>
    [JsonStringEnumMemberName("read")]
    Read,

    [JsonStringEnumMemberName("read:accounts")]
    Read__Accounts,
    [JsonStringEnumMemberName("read:blocks")]
    Read__Blocks,
    [JsonStringEnumMemberName("read:bookmarks")]
    Read__Bookmarks,
    [JsonStringEnumMemberName("read:collections")]
    Read__Collections,
    [JsonStringEnumMemberName("read:favourites")]
    Read__Favourites,
    [JsonStringEnumMemberName("read:filters")]
    Read__Filters,
    [JsonStringEnumMemberName("read:follows")]
    Read__Follows,
    [JsonStringEnumMemberName("read:lists")]
    Read__Lists,
    [JsonStringEnumMemberName("read:mutes")]
    Read__Mutes,
    [JsonStringEnumMemberName("read:notifications")]
    Read__Notifications,
    [JsonStringEnumMemberName("read:search")]
    Read__Search,
    [JsonStringEnumMemberName("read:statuses")]
    Read__Statuses,

    /// <summary>
    /// Grants access to write data. Requesting write will also grant granular scopes shown in the right column of the table below.
    /// </summary>
    [JsonStringEnumMemberName("write")]
    Write,

    [JsonStringEnumMemberName("write:accounts")]
    Write__Accounts,
    [JsonStringEnumMemberName("write:blocks")]
    Write__Blocks,
    [JsonStringEnumMemberName("write:bookmarks")]
    Write__Bookmarks,
    [JsonStringEnumMemberName("write:collections")]
    Write__Collections,
    [JsonStringEnumMemberName("write:conversations")]
    Write__Conversations,
    [JsonStringEnumMemberName("write:favourites")]
    Write__Favourites,
    [JsonStringEnumMemberName("write:filters")]
    Write__Filters,
    [JsonStringEnumMemberName("write:follows")]
    Write__Follows,
    [JsonStringEnumMemberName("write:lists")]
    Write__Lists,
    [JsonStringEnumMemberName("write:media")]
    Write__Media,
    [JsonStringEnumMemberName("write:mutes")]
    Write__Mutes,
    [JsonStringEnumMemberName("write:notifications")]
    Write__Notifications,
    [JsonStringEnumMemberName("write:reports")]
    Write__Reports,
    [JsonStringEnumMemberName("write:statuses")]
    Write__Statuses,

    /// <summary>
    /// Grants access to manage relationships. Requesting follow will also grant granular scopes shown in the right column of the table below.
    /// </summary>
    [Obsolete("This scope has been deprecated in 3.5.0 and newer. You should instead request the granular scopes individually, or request read/write scopes as needed.")]
    [JsonStringEnumMemberName("follow")]
    Follow,

    /// <summary>
    /// Used for administrative and moderation APIs. Added in Mastodon 2.9.1.
    /// </summary>
    [JsonStringEnumMemberName("admin:read")]
    AdminRead,

    [JsonStringEnumMemberName("admin:read:accounts")]
    Admin__Read__Accounts,
    [JsonStringEnumMemberName("admin:read:canonical_email_blocks")]
    Admin__Read__CanonicalEmailBlocks,
    [JsonStringEnumMemberName("admin:read:domain_allows")]
    Admin__Read__DomainAllows,
    [JsonStringEnumMemberName("admin:read:domain_blocks")]
    Admin__Read__DomainBlocks,
    [JsonStringEnumMemberName("admin:read:email_domain_blocks")]
    Admin__Read__EmailDomainBlocks,
    [JsonStringEnumMemberName("admin:read:ip_blocks")]
    Admin__Read__IpBlocks,
    [JsonStringEnumMemberName("admin:read:reports")]
    Admin__Read__Reports,

    [JsonStringEnumMemberName("admin:write")]
    AdminWrite,

    [JsonStringEnumMemberName("admin:write:accounts")]
    Admin__Write__Accounts,
    [JsonStringEnumMemberName("admin:write:canonical_email_blocks")]
    Admin__Write__CanonicalEmailBlocks,
    [JsonStringEnumMemberName("admin:write:domain_allows")]
    Admin__Write__DomainAllows,
    [JsonStringEnumMemberName("admin:write:domain_blocks")]
    Admin__Write__DomainBlocks,
    [JsonStringEnumMemberName("admin:write:email_domain_blocks")]
    Admin__Write__EmailDomainBlocks,
    [JsonStringEnumMemberName("admin:write:ip_blocks")]
    Admin__Write__IpBlocks,
    [JsonStringEnumMemberName("admin:write:reports")]
    Admin__Write__Reports,
}