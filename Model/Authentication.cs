using Mastonet;
using Mastonet.Entities;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Model; 
public partial class Authentication {
    private readonly AuthenticationClient authenticationClient;

    public Authentication(string instance) {
        authenticationClient = new AuthenticationClient(instance);

        var appRegistrationJson = Credentials.GetAppRegistration(instance);
        if (!string.IsNullOrEmpty(appRegistrationJson)) {
            var appRegistration = JsonSerializer.Deserialize<AppRegistration>(appRegistrationJson);
            if (appRegistration is not null) {
                authenticationClient.AppRegistration = appRegistration;
            }
        }

        if (authenticationClient.AppRegistration is null) {
            _ = CreateApp();
        }
    }

    public string OAuthUrl => authenticationClient!.OAuthUrl();

    private async Task CreateApp() {
        var appRegistration = await authenticationClient.CreateApp("Versery", "https://github.com/AkazaRenn/Versery/", null, GranularScope.Read, GranularScope.Write, GranularScope.Follow);
        Credentials.AddAppRegistration(authenticationClient.Instance, JsonSerializer.Serialize(appRegistration));
    }

    public async Task CheckLoginUrl(string url) {
        var match = OAuthRegex().Match(url);

        if (authenticationClient is not null && match.Success) {
            var code = match.Groups[1].Value;
            var auth = await authenticationClient!.ConnectWithCode(code);
            var mastodonClient = new MastodonClient(authenticationClient.Instance, auth.AccessToken);
            await SaveAccessToken(mastodonClient);
        }
    }

    private static async Task SaveAccessToken(MastodonClient client) {
        var account = await client.GetCurrentUser();
        Credentials.AddAccessToken(account.Id, client.AccessToken);
    }

    [GeneratedRegex(@"/oauth/authorize/native\?code=([a-zA-Z0-9_-]+)", RegexOptions.Compiled)]
    private static partial Regex OAuthRegex();
}
