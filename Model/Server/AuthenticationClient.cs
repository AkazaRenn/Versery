using Model.Server.Api.Enumerations;
using Model.Server.Entities;
using System.Net;

namespace Model.Server;

internal class AuthenticationClient {
    public string Instance { get; }
    public CredentialApplication? AppRegistration { get; set; }

    public AuthenticationClient(string instance) {
        Instance = instance;
    }

    public AuthenticationClient(string instance, CredentialApplication app) {
        Instance = instance;
        AppRegistration = app;
    }

    #region Apps

    public Task<CredentialApplication> CreateApp(string appName, string? website = null, string? redirectUri = null, params Scope[] scope) {
        return CreateApp(appName, website, redirectUri, scope.AsEnumerable());
    }

    public async Task<CredentialApplication> CreateApp(string appName, string? website = null, string? redirectUri = null, IEnumerable<Scope>? scopes = null) {
        var scopeString = GetScopeParam(scopes);
        var data = new List<KeyValuePair<string, string>>() {
            new("client_name", appName),
            new("scopes", scopeString),
            new("redirect_uris", redirectUri?? "urn:ietf:wg:oauth:2.0:oob")
        };

        if (website != null) {
            data.Add(new KeyValuePair<string, string>("website", website));
        }

        AppRegistration = await Apps.("/api/v1/apps", data);
        return AppRegistration;
    }

    #endregion

    #region Auth

    public Task<Token> ConnectWithCode(string code, string? redirect_uri = null) {
        if (AppRegistration == null) {
            throw new InvalidOperationException("The app must be registered before you can connect");
        }

        var data = new List<KeyValuePair<string, string>>()
        {
            new("client_id", AppRegistration.ClientId),
            new("client_secret", AppRegistration.ClientSecret),
            new("grant_type", "authorization_code"),
            new("redirect_uri", redirect_uri ?? "urn:ietf:wg:oauth:2.0:oob"),
            new("code", code),
        };

        return Post<Token>("/oauth/token", data);
    }

    public string OAuthUrl(string? redirectUri = null) {
        if (AppRegistration == null) {
            throw new InvalidOperationException("The app must be registered before you can connect");
        }

        if (String.IsNullOrEmpty(Instance)) {
            throw new InvalidOperationException("The instance must be set before you can connect");
        }

        if (redirectUri != null) {
            redirectUri = WebUtility.UrlEncode(WebUtility.UrlDecode(redirectUri));
        } else {
            redirectUri = "urn:ietf:wg:oauth:2.0:oob";
        }

        return $"https://{Instance}/oauth/authorize?response_type=code&client_id={AppRegistration.ClientId}&scopes={GetScopeParam(AppRegistration.Scopes).Replace(" ", "%20")}&redirect_uri={redirectUri ?? "urn:ietf:wg:oauth:2.0:oob"}";
    }

    /// <summary>
    /// Revoke an access token to make it no longer valid for use.
    /// </summary>
    /// <param name="token">The previously obtained token, to be invalidated.</param>
    /// <exception cref="InvalidOperationException"></exception>
    public Task Revoke(string token) {
        if (AppRegistration == null) {
            throw new InvalidOperationException("You need to revoke a token with the app CclientId and ClientSecret used to obtain the Token");
        }

        var data = new List<KeyValuePair<string, string>>()
        {
            new("client_id", AppRegistration.ClientId),
            new("client_secret", AppRegistration.ClientSecret),
            new("token", token),
        };

        return Post("/oauth/revoke", data);
    }

    private static string GetScopeParam(IEnumerable<Scope>? scopes) {
        if (scopes == null) {
            return "";
        }

        scopes = scopes.ToHashSet();
        return String.Join(" ", scopes.Select(s => s.ToString().ToLowerInvariant().Replace("__", ":")));
    }

    #endregion
}
