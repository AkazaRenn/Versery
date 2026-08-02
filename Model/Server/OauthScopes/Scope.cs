using System.Text.Json.Serialization;

namespace Model.Server.OauthScopes;
/// <see href="https://docs.joinmastodon.org/api/oauth-scopes/">Mastodon API Documentation</see>
public enum Scope {
    [JsonStringEnumMemberName("profile")]
    Profile,

    [JsonStringEnumMemberName("push")]
    Push,

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

    [Obsolete("This scope has been deprecated in 3.5.0 and newer. You should instead request the granular scopes individually, or request read/write scopes as needed.")]
    [JsonStringEnumMemberName("follow")]
    Follow,

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
