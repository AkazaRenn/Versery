using Model.DataPersistence;
using Model.Server;
using Model.Server.Entities;
using Model.Server.OauthScopes;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Model.Access;

public sealed partial class Authentication(string instance) {
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

            var auth = await authenticationClient.OAuth.Token(
                "authorization_code",
                code,
                appRegistration.ClientId,
                appRegistration.ClientSecret,
                "urn:ietf:wg:oauth:2.0:oob"
            );
            await client.NewUser(instance, auth.AccessToken);
        }
    }

    private CredentialApplication? GetStoredAppRegistration() {
        var appRegistrationJson = Credentials.GetAppRegistration(instance);
        if (string.IsNullOrWhiteSpace(appRegistrationJson)) {
            return null;
        }

        return JsonSerializer.Deserialize<CredentialApplication>(appRegistrationJson);
    }

    private async Task<CredentialApplication> GetOrCreateAppRegistration() {
        var storedAppRegistration = GetStoredAppRegistration();
        if (storedAppRegistration is not null) {
            return storedAppRegistration;
        }

        var scopes = FormatScopes([Scope.Read, Scope.Write, Scope.Follow]);
        var newAppRegistration = await authenticationClient.Apps.Create(
            Constants.AppName,
            ["urn:ietf:wg:oauth:2.0:oob"],
            scopes,
            Constants.ProjectLink
        );

        Credentials.AddAppRegistration(instance, JsonSerializer.Serialize(newAppRegistration));
        return newAppRegistration;
    }

    private static string FormatScopes(IEnumerable<Scope> scopes) {
        var normalizedScopes = scopes.ToHashSet();
        return string.Join(" ", normalizedScopes.Select(s => s.ToString().ToLowerInvariant().Replace("__", ":")));
    }

    private static string BuildOAuthUrl(string instance, CredentialApplication appRegistration, string redirectUri = "urn:ietf:wg:oauth:2.0:oob") {
        if (string.IsNullOrWhiteSpace(instance)) {
            throw new InvalidOperationException("The instance must be set before you can connect");
        }

        var queryParameters = new Dictionary<string, string> {
            ["response_type"] = "code",
            ["client_id"] = appRegistration.ClientId,
            ["scope"] = string.Join(" ", appRegistration.Scopes),
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
