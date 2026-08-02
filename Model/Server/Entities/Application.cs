using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents an application that interfaces with the REST API, for example to access account information or post statuses.
/// Version: 4.4.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Application/">Mastodon API Documentation</see>
public class Application {
    /// <summary>
    /// The name of your application.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The website associated with your application.
    /// </summary>
    [JsonPropertyName("website")]
    public Uri? Website { get; set; } = null;

    /// <summary>
    /// The scopes for the application. This is the registered scopes string split on whitespace.
    /// </summary>
    [JsonPropertyName("scopes")]
    public IEnumerable<Scope> Scopes { get; set; } = [];

    /// <summary>
    /// The registered redirection URI(s) for your application.
    /// </summary>
    [JsonPropertyName("redirect_uris")]
    public IEnumerable<Uri> RedirectUris { get; set; } = [];

    /// <summary>
    /// The registered redirection URI(s) for your application stored as a single string. Multiple URIs are separated by whitespace characters. May contain \n characters when multiple redirect URIs are registered.
    /// </summary>
    [Obsolete("deprecated in favour of RedirectUris, since the value of this property is not a well-formed URI when multiple redirect URIs are registered.")]
    [JsonPropertyName("redirect_uri")]
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// Used for Push Streaming API. Returned with POST /api/v1/apps. Equivalent to WebPushSubscription#server_key and Instance#vapid_public_key
    /// </summary>
    [Obsolete("deprecated pending removal, please see api/v2/instance for this value (configuration.vapid.public_key)")]
    [JsonPropertyName("vapid_key")]
    public string? VapidKey { get; set; } = null;
}

public sealed class CredentialApplication: Application {
    /// <summary>
    /// Client ID key, to be used for obtaining OAuth tokens.
    /// </summary>
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// Client secret key, to be used for obtaining OAuth tokens.
    /// </summary>
    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// When the client secret key will expire. Presently this always returns 0 indicating that OAuth Clients do not expire.
    /// </summary>
    [JsonPropertyName("client_secret_expires_at")]
    public int ClientSecretExpiresAt { get; set; } = 0;
}

/// <summary>
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
