using Model.Server.Entities;
using Refit;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Methods;
/// <see href="https://docs.joinmastodon.org/methods/oauth/">Mastodon API Documentation</see>
public interface IOAuth {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/oauth/#authorize"/>
    [Get("/oauth/authorize")]
    Task<string> Authorize(
        [AliasAs("response_type")] string responseType,
        [AliasAs("client_id")] string clientId,
        [AliasAs("redirect_uri")] Uri redirectUri,
        [AliasAs("scope")] string? scope = null,
        [AliasAs("state")] string? state = null,
        [AliasAs("code_challenge")] string? codeChallenge = null,
        [AliasAs("code_challenge_method")] string? codeChallengeMethod = null,
        [AliasAs("force_login")] bool? forceLogin = null,
        [AliasAs("lang")] CultureInfo? lang = null
    );

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/oauth/#token"/>
    [Post("/oauth/token")]
    Task<Token> Token(
        [AliasAs("grant_type")] string grantType,
        [AliasAs("code")] string code,
        [AliasAs("client_id")] string clientId,
        [AliasAs("client_secret")] string clientSecret,
        [AliasAs("redirect_uri")] Uri redirectUri,
        [AliasAs("code_verifier")] string? codeVerifier = null,
        [AliasAs("scope")] string? scope = null
    );

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/oauth/#revoke"/>
    [Post("/oauth/revoke")]
    Task Revoke(
        [AliasAs("client_id")] string clientId,
        [AliasAs("client_secret")] string clientSecret,
        [AliasAs("token")] string token
    );

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/oauth/#userinfo"/>
    [Get("/oauth/userinfo")]
    Task<UserInfo> Userinfo();

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/oauth/#userinfo"/>
    [Post("/oauth/userinfo")]
    Task<UserInfo> UserinfoPost();

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/oauth/#authorization-server-metadata"/>
    [Get("/.well-known/oauth-authorization-server")]
    Task<OAuthServerConfiguration> AuthorizationServerMetadata();
}

public sealed class UserInfo {
    [JsonPropertyName("iss")]
    public Uri? Iss { get; set; } = null;

    [JsonPropertyName("sub")]
    public Uri? Sub { get; set; } = null;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("preferred_username")]
    public string PreferredUsername { get; set; } = string.Empty;

    [JsonPropertyName("profile")]
    public Uri? Profile { get; set; } = null;

    [JsonPropertyName("picture")]
    public Uri? Picture { get; set; } = null;
}

public sealed class OAuthServerConfiguration {
    [JsonPropertyName("issuer")]
    public Uri? Issuer { get; set; } = null;

    [JsonPropertyName("service_documentation")]
    public Uri? ServiceDocumentation { get; set; } = null;

    [JsonPropertyName("authorization_endpoint")]
    public Uri? AuthorizationEndpoint { get; set; } = null;

    [JsonPropertyName("token_endpoint")]
    public Uri? TokenEndpoint { get; set; } = null;

    [JsonPropertyName("app_registration_endpoint")]
    public Uri? AppRegistrationEndpoint { get; set; } = null;

    [JsonPropertyName("revocation_endpoint")]
    public Uri? RevocationEndpoint { get; set; } = null;

    [JsonPropertyName("userinfo_endpoint")]
    public Uri? UserinfoEndpoint { get; set; } = null;

    [JsonPropertyName("scopes_supported")]
    public List<string> ScopesSupported { get; set; } = [];

    [JsonPropertyName("response_types_supported")]
    public List<string> ResponseTypesSupported { get; set; } = [];

    [JsonPropertyName("response_modes_supported")]
    public List<string> ResponseModesSupported { get; set; } = [];

    [JsonPropertyName("code_challenge_methods_supported")]
    public List<string> CodeChallengeMethodsSupported { get; set; } = [];

    [JsonPropertyName("grant_types_supported")]
    public List<string> GrantTypesSupported { get; set; } = [];

    [JsonPropertyName("token_endpoint_auth_methods_supported")]
    public List<string> TokenEndpointAuthMethodsSupported { get; set; } = [];
}
