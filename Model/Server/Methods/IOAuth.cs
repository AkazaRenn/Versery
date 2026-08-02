using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;
/// <summary>
/// OAuth API methods
/// Generate and manage OAuth tokens.
/// </summary>
/// <see href="https://docs.joinmastodon.org/methods/oauth/">Mastodon API Documentation</see>
public interface IOAuth {
    /// <summary>
    /// Authorize a user
    /// Displays an authorization form to the user. If approved, it will create and return an authorization code, then redirect to the desired redirect_uri, or show the authorization code if urn:ietf:wg:oauth:2.0:oob was requested. The authorization code can be used while requesting a token to obtain access to user-level methods.
    /// Version: 4.3.0
    /// </summary>
    /// <param name="responseType">Should be set equal to code.</param>
    /// <param name="clientId">The client ID, obtained during app registration.</param>
    /// <param name="redirectUri">Set a URI to redirect the user to. If this parameter is set to urn:ietf:wg:oauth:2.0:oob then the authorization code will be shown instead. Must match one of the redirect_uris declared during app registration.</param>
    /// <param name="scope">List of requested OAuth scopes, separated by spaces (or by pluses, if using query parameters). Must be a subset of scopes declared during app registration. If not provided, defaults to read.</param>
    /// <param name="state">Arbitrary value to pass through to your server when the user authorizes or rejects the authorization request.</param>
    /// <param name="codeChallenge">The PKCE code challenge for the authorization request.</param>
    /// <param name="codeChallengeMethod">Must be S256, as this is the only code challenge method that is supported by Mastodon for PKCE.</param>
    /// <param name="forceLogin">Forces the user to re-login, which is necessary for authorizing with multiple accounts from the same instance.</param>
    /// <param name="lang">The ISO 639-1 two-letter language code to use while rendering the authorization form.</param>
    /// <returns>Returns a URL or HTML response</returns>
    /// <see href="https://docs.joinmastodon.org/methods/oauth/#authorize">Mastodon API Documentation</see>
    [Get("/oauth/authorize")]
    Task<string> GetAuthorize(
        [AliasAs("response_type")] string responseType,
        [AliasAs("client_id")] string clientId,
        [AliasAs("redirect_uri")] string redirectUri,
        [AliasAs("scope")] string? scope = null,
        [AliasAs("state")] string? state = null,
        [AliasAs("code_challenge")] string? codeChallenge = null,
        [AliasAs("code_challenge_method")] string? codeChallengeMethod = null,
        [AliasAs("force_login")] bool? forceLogin = null,
        [AliasAs("lang")] string? lang = null
    );

    /// <summary>
    /// Obtain a token
    /// Obtain an access token, to be used during API calls that are not public.
    /// Version: 4.3.0
    /// </summary>
    /// <param name="grantType">Should be set equal to authorization_code.</param>
    /// <param name="code">The authorization code received from the authorization step.</param>
    /// <param name="clientId">The client ID, obtained during app registration.</param>
    /// <param name="clientSecret">The client secret, obtained during app registration.</param>
    /// <param name="redirectUri">Must match the redirect_uri used in the authorization step.</param>
    /// <param name="codeVerifier">The PKCE code verifier for the authorization request.</param>
    /// <param name="scope">When grant_type is set to client_credentials, the list of requested OAuth scopes, separated by spaces (or pluses, if using query parameters). Must be a subset of the scopes requested at the time the application was created. If omitted, it defaults to read. Has no effect when grant_type is authorization_code.</param>
    /// <returns>Returns an access token and related information.</returns>
    /// <see href="https://docs.joinmastodon.org/methods/oauth/#token">Mastodon API Documentation</see>
    [Post("/oauth/token")]
    Task<Token> Token(
        [AliasAs("grant_type")] string grantType,
        [AliasAs("code")] string code,
        [AliasAs("client_id")] string clientId,
        [AliasAs("client_secret")] string clientSecret,
        [AliasAs("redirect_uri")] string redirectUri,
        [AliasAs("code_verifier")] string? codeVerifier = null,
        [AliasAs("scope")] string? scope = null
    );

    /// <summary>
    /// Revoke a token
    /// Revoke an access token to make it no longer valid for use.
    /// Version: 0.1.0
    /// </summary>
    /// <param name="clientId">The client ID, obtained during app registration.</param>
    /// <param name="clientSecret">The client secret, obtained during app registration.</param>
    /// <param name="token">The access token to revoke.</param>
    /// <see href="https://docs.joinmastodon.org/methods/oauth/#revoke">Mastodon API Documentation</see>
    [Post("/oauth/revoke")]
    Task Revoke(
        [AliasAs("client_id")] string clientId,
        [AliasAs("client_secret")] string clientSecret,
        [AliasAs("token")] string token
    );

    /// <summary>
    /// Retrieve user information
    /// Retrieves standardised OIDC claims about the currently authenticated user.
    /// Note: For compatibility with the OIDC specification this endpoint is also available via POST request.
    /// Version: 4.4.0
    /// </summary>
    /// <returns>Returns the authenticated user's information.</returns>
    /// <see href="https://docs.joinmastodon.org/methods/oauth/#userinfo">Mastodon API Documentation</see>
    [Get("/oauth/userinfo")]
    Task<User> GetUserInfo();

    /// <summary>
    /// Discover OAuth Server Configuration
    /// Returns the OAuth 2 Authorization Server Metadata for the Mastodon server, as defined by RFC 8414.
    /// We include the additional non-standard property of app_registration_endpoint which refers to the POST /api/v1/apps endpoint, since we don’t currently support the standard registration_endpoint endpoint for Dynamic Client Registration.
    /// The properties exposed by this endpoint can help you better integrate with the Mastodon API, such as allowing for negotiation of scopes across different versions of Mastodon.
    /// Version: 4.4.0
    /// </summary>
    /// <returns>Returns the OAuth server's configuration information.</returns>
    /// <see href="https://docs.joinmastodon.org/methods/oauth/#authorization-server-metadata">Mastodon API Documentation</see>
    [Get("/.well-known/oauth-authorization-server ")]
    Task<OAuthServerConfiguration> GetOAuthServerConfiguration();
}
