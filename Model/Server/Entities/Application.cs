using Model.Server.Api.Enumerations;
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
