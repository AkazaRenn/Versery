using Raiqub.Generators.EnumUtilities;
using System.Text.Json.Serialization;

namespace Model.Server.OauthScopes;
/// <see href="https://docs.joinmastodon.org/api/oauth-scopes/">Mastodon API Documentation</see>
[JsonConverterGenerator]
public enum Scope {
    [JsonStringEnumMemberName("profile")]
    Profile,

    [JsonStringEnumMemberName("push")]
    Push,

    [JsonStringEnumMemberName("read")]
    Read,

    [JsonStringEnumMemberName("read:accounts")]
    Read_Accounts,
    [JsonStringEnumMemberName("read:blocks")]
    Read_Blocks,
    [JsonStringEnumMemberName("read:bookmarks")]
    Read_Bookmarks,
    [JsonStringEnumMemberName("read:collections")]
    Read_Collections,
    [JsonStringEnumMemberName("read:favourites")]
    Read_Favourites,
    [JsonStringEnumMemberName("read:filters")]
    Read_Filters,
    [JsonStringEnumMemberName("read:follows")]
    Read_Follows,
    [JsonStringEnumMemberName("read:lists")]
    Read_Lists,
    [JsonStringEnumMemberName("read:mutes")]
    Read_Mutes,
    [JsonStringEnumMemberName("read:notifications")]
    Read_Notifications,
    [JsonStringEnumMemberName("read:search")]
    Read_Search,
    [JsonStringEnumMemberName("read:statuses")]
    Read_Statuses,

    [JsonStringEnumMemberName("write")]
    Write,

    [JsonStringEnumMemberName("write:accounts")]
    Write_Accounts,
    [JsonStringEnumMemberName("write:blocks")]
    Write_Blocks,
    [JsonStringEnumMemberName("write:bookmarks")]
    Write_Bookmarks,
    [JsonStringEnumMemberName("write:collections")]
    Write_Collections,
    [JsonStringEnumMemberName("write:conversations")]
    Write_Conversations,
    [JsonStringEnumMemberName("write:favourites")]
    Write_Favourites,
    [JsonStringEnumMemberName("write:filters")]
    Write_Filters,
    [JsonStringEnumMemberName("write:follows")]
    Write_Follows,
    [JsonStringEnumMemberName("write:lists")]
    Write_Lists,
    [JsonStringEnumMemberName("write:media")]
    Write_Media,
    [JsonStringEnumMemberName("write:mutes")]
    Write_Mutes,
    [JsonStringEnumMemberName("write:notifications")]
    Write_Notifications,
    [JsonStringEnumMemberName("write:reports")]
    Write_Reports,
    [JsonStringEnumMemberName("write:statuses")]
    Write_Statuses,

    [Obsolete("This scope has been deprecated in 3.5.0 and newer. You should instead request the granular scopes individually, or request read/write scopes as needed.")]
    [JsonStringEnumMemberName("follow")]
    Follow,

    [JsonStringEnumMemberName("admin:read")]
    Admin_Read,

    [JsonStringEnumMemberName("admin:read:accounts")]
    Admin_Read_Accounts,
    [JsonStringEnumMemberName("admin:read:canonical_email_blocks")]
    Admin_Read_CanonicalEmailBlocks,
    [JsonStringEnumMemberName("admin:read:domain_allows")]
    Admin_Read_DomainAllows,
    [JsonStringEnumMemberName("admin:read:domain_blocks")]
    Admin_Read_DomainBlocks,
    [JsonStringEnumMemberName("admin:read:email_domain_blocks")]
    Admin_Read_EmailDomainBlocks,
    [JsonStringEnumMemberName("admin:read:ip_blocks")]
    Admin_Read_IpBlocks,
    [JsonStringEnumMemberName("admin:read:reports")]
    Admin_Read_Reports,

    [JsonStringEnumMemberName("admin:write")]
    Admin_Write,

    [JsonStringEnumMemberName("admin:write:accounts")]
    Admin_Write_Accounts,
    [JsonStringEnumMemberName("admin:write:canonical_email_blocks")]
    Admin_Write_CanonicalEmailBlocks,
    [JsonStringEnumMemberName("admin:write:domain_allows")]
    Admin_Write_DomainAllows,
    [JsonStringEnumMemberName("admin:write:domain_blocks")]
    Admin_Write_DomainBlocks,
    [JsonStringEnumMemberName("admin:write:email_domain_blocks")]
    Admin_Write_EmailDomainBlocks,
    [JsonStringEnumMemberName("admin:write:ip_blocks")]
    Admin_Write_IpBlocks,
    [JsonStringEnumMemberName("admin:write:reports")]
    Admin_Write_Reports,
}
