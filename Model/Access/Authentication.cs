using Model.DataPersistence;
using Model.Server;
using Model.Server.Entities;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Model.Access;

public sealed partial class Authentication(string instance) {
    private const string Scopes = "read write follow";
    private const string RedirectUri = "urn:ietf:wg:oauth:2.0:oob";
    private static readonly Regex oAuthRegex = OAuthRegex();

    private readonly AuthenticationClient authenticationClient = new(instance);
    private readonly Client client = Services.Get<Client>();

    public async Task<string> OAuthUrl() {
        var appRegistration = await GetOrCreateAppRegistration();
        return BuildOAuthUrl(authenticationClient.Instance, appRegistration);
    }

    public async Task CheckSignInUrl(string url) {
        var match = oAuthRegex.Match(url);
        if (match.Success) {
            var code = match.Groups[1].Value;
            var appRegistration = GetStoredAppRegistration() ?? throw new InvalidOperationException("The app must be registered before you can connect");

            var auth = await authenticationClient.OAuth.PostOauthToken(new PostOauthTokenRequest(
                clientId: appRegistration.ClientId,
                clientSecret: appRegistration.ClientSecret,
                code: code,
                codeVerifier: null,
                grantType: "authorization_code",
                redirectUri: RedirectUri,
                scope: Scopes
            ));
            await client.NewUser(instance, auth.AccessToken);
        }
    }

    private CredentialApplication? GetStoredAppRegistration() {
        var appRegistrationJson = Credentials.GetAppRegistration(instance);
        if (string.IsNullOrWhiteSpace(appRegistrationJson)) {
            return null;
        }

        return JsonSerializer.Deserialize(appRegistrationJson, MastodonAPISerializerContext.Default.CredentialApplication);
    }

    private async Task<CredentialApplication> GetOrCreateAppRegistration() {
        var storedAppRegistration = GetStoredAppRegistration();
        if (storedAppRegistration is not null) {
            return storedAppRegistration;
        }

        var newAppRegistration = await authenticationClient.Apps.CreateApp(new CreateAppRequest(
            clientName: Constants.AppName,
            redirectUris: [new Uri(RedirectUri)],
            scopes: Scopes,
            website: Constants.ProjectLink
        ));

        Credentials.AddAppRegistration(instance, JsonSerializer.Serialize(newAppRegistration, MastodonAPISerializerContext.Default.CredentialApplication));
        return newAppRegistration;
    }

    private static string BuildOAuthUrl(string instance, CredentialApplication credentialApplication, string redirectUri = RedirectUri) {
        if (string.IsNullOrWhiteSpace(instance)) {
            throw new InvalidOperationException("The instance must be set before you can connect");
        }

        var queryParameters = new Dictionary<string, string> {
            ["response_type"] = "code",
            ["client_id"] = credentialApplication.ClientId,
            ["scope"] = Scopes,
            ["redirect_uri"] = redirectUri
        };

        var builder = new UriBuilder($"https://{instance}/oauth/authorize") {
            Query = string.Join("&", queryParameters.Select(parameter =>
                $"{Uri.EscapeDataString(parameter.Key)}={Uri.EscapeDataString(parameter.Value)}"))
        };

        return builder.Uri.ToString();
    }

    [GeneratedRegex(@"/oauth/authorize/native\?code=([a-zA-Z0-9_-]+)", RegexOptions.Compiled)]
    private static partial Regex OAuthRegex();
}
