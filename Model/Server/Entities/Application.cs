using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Application/">Mastodon API Documentation</see>
public class Application {
    /// <summary>
    /// Version: 0.9.9
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Application/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 0.9.9
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Application/#name"/>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Application/#website"/>
    [JsonPropertyName("website")]
    public Uri? Website { get; set; } = null;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Application/#scopes"/>
    [JsonPropertyName("scopes")]
    public List<string> Scopes { get; set; } = [];

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Application/#redirect_uris"/>
    [JsonPropertyName("redirect_uris")]
    public List<string> RedirectUris { get; set; } = [];

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Application/#redirect_uri"/>
    [Obsolete("deprecated in favour of RedirectUris, since the value of this property is not a well-formed URI when multiple redirect URIs are registered.")]
    [JsonPropertyName("redirect_uri")]
    public string RedirectUri { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Application/#vapid_key"/>
    [Obsolete("deprecated pending removal, please see api/v2/instance for this value (configuration.vapid.public_key)")]
    [JsonPropertyName("vapid_key")]
    public string? VapidKey { get; set; } = null;
}

public sealed class CredentialApplication: Application {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Application/#client_id"/>
    [JsonPropertyName("client_id")]
    public string ClientId { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Application/#client_secret"/>
    [JsonPropertyName("client_secret")]
    public string ClientSecret { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Application/#client_secret_expires_at"/>
    [JsonPropertyName("client_secret_expires_at")]
    public int ClientSecretExpiresAt { get; set; } = 0;
}
