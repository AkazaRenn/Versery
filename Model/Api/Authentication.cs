using Mastonet;
using Mastonet.Entities;
using Microsoft.Extensions.DependencyInjection;
using Model.Api;
using Model.DataPersistence;
using System.Text.Json;
using System.Text.RegularExpressions;
using Utilities;

namespace Model.Api;
public sealed partial class Authentication(string instance) {
    private static readonly Regex oAuthRegex = OAuthRegex();

    private readonly AuthenticationClient authenticationClient = new(instance);
    private readonly Client client = Utilities.Services.Provider.GetRequiredService<Client>();

    public async Task<string> OAuthUrl() {
        var appRegistrationJson = Credentials.GetAppRegistration(authenticationClient.Instance);
        if (!string.IsNullOrWhiteSpace(appRegistrationJson)) {
            var appRegistration = JsonSerializer.Deserialize<AppRegistration>(appRegistrationJson);
            if (appRegistration is not null) {
                authenticationClient.AppRegistration = appRegistration;
                return string.Empty;
            }
        }

        var newAppRegistration = await authenticationClient.CreateApp(Constants.AppName, Constants.ProjectLink, null, GranularScope.Read, GranularScope.Write, GranularScope.Follow);
        Credentials.AddAppRegistration(authenticationClient.Instance, JsonSerializer.Serialize(newAppRegistration));
        return authenticationClient.OAuthUrl();
}

    public async Task<bool> CheckSignInUrl(string url) {
        var match = oAuthRegex.Match(url);

        if (match.Success) {
            var code = match.Groups[1].Value;
            var auth = await authenticationClient.ConnectWithCode(code);
            await client.NewUser(authenticationClient.Instance, auth.AccessToken);
            return true;
        }

        return false;
    }

    [GeneratedRegex(@"/oauth/authorize/native\?code=([a-zA-Z0-9_-]+)", RegexOptions.Compiled)]
    private static partial Regex OAuthRegex();
}
