using Model.Server.Entities;
using Refit;

namespace Model.Server.Methods;

/// <summary>
/// Register client applications that can be used to obtain OAuth tokens.
/// </summary>
/// <see href="https://docs.joinmastodon.org/methods/apps/">Mastodon API Documentation</see>
public interface IApps {
    /// <summary>
    /// Create an application
    /// Create a new application to obtain OAuth2 credentials.
    /// Version: 4.4.0
    /// </summary>
    /// <param name="clientName">A name for your application</param>
    /// <param name="redirectUris">Where the user should be redirected after authorization. To display the authorization code to the user instead of redirecting to a web page, use urn:ietf:wg:oauth:2.0:oob in this parameter.</param>
    /// <param name="scopes">Space separated list of scopes. If none is provided, defaults to read. See OAuth Scopes for a list of possible scopes.</param>
    /// <param name="website">A URL to the homepage of your app</param>
    /// <returns>Store the client_id and client_secret in your cache, as these will be used to obtain OAuth tokens.</returns>
    /// <see href="https://docs.joinmastodon.org/methods/apps/#create">Mastodon API Documentation</see>
    [Post("/api/v1/apps")]
    Task<CredentialApplication> Post(
        [AliasAs("client_name")] string clientName,
        [AliasAs("redirect_uris")] IEnumerable<string> redirectUris,
        [AliasAs("scopes")] string? scopes = null,
        [AliasAs("website")] string? website = null
    );

    /// <summary>
    /// Verify your app works
    /// Confirm that the app’s OAuth2 credentials work.
    /// Version: 4.4.0
    /// </summary>
    /// <returns>If the Authorization header was provided with a valid token, you should see your app returned as an Application entity.</returns>
    /// <see href="https://docs.joinmastodon.org/methods/apps/#verify">Mastodon API Documentation</see>
    [Get("/api/v1/apps/verify_credentials")]
    Task<CredentialApplication> GetVerifyCredentials();
}
