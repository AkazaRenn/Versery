using Model.DataPersistence;
using Model.Server;
using Model.Server.Api.Enumerations;
using Model.Server.Entities;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Model.Access;

public sealed partial class Authentication(string instance) {
    private static readonly Regex oAuthRegex = OAuthRegex();

    private readonly AuthenticationClient authenticationClient = new(instance);
    private readonly Client client = Services.Get<Client>();

    public async Task<string> OAuthUrl() {
        var appRegistrationJson = Credentials.GetAppRegistration(instance);
        if (!string.IsNullOrWhiteSpace(appRegistrationJson)) {
            var appRegistration = JsonSerializer.Deserialize<CredentialApplication>(appRegistrationJson);
            if (appRegistration is not null) {
                authenticationClient.AppRegistration = appRegistration;
                return authenticationClient.OAuthUrl();
            }
        }

        var newAppRegistration = await authenticationClient.CreateApp(Constants.AppName, Constants.ProjectLink, null, Scope.Read, Scope.Write, Scope.Follow);
        Credentials.AddAppRegistration(instance, JsonSerializer.Serialize(newAppRegistration));
        return authenticationClient.OAuthUrl();
    }

    public async Task CheckSignInUrl(string url) {
        var match = oAuthRegex.Match(url);
        if (match.Success) {
            var code = match.Groups[1].Value;
            var auth = await authenticationClient.ConnectWithCode(code);
            await client.NewUser(instance, auth.AccessToken);
        }
    }

    [GeneratedRegex(@"/oauth/authorize/native\?code=([a-zA-Z0-9_-]+)", RegexOptions.Compiled)]
    private static partial Regex OAuthRegex();
}
