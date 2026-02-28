using Mastonet;
using Mastonet.Entities;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Model; 
public partial class Authentication(string instance) {
    private static readonly Regex oAuthRegex = OAuthRegex();

    private readonly AuthenticationClient authenticationClient = new(instance);

    public string OAuthUrl => authenticationClient!.OAuthUrl();

    public async Task Register() {
        var appRegistrationJson = Credentials.GetAppRegistration(authenticationClient.Instance);
        if (!string.IsNullOrEmpty(appRegistrationJson)) {
            var appRegistration = JsonSerializer.Deserialize<AppRegistration>(appRegistrationJson);
            if (appRegistration is not null) {
                authenticationClient.AppRegistration = appRegistration;
                return;
            }
        }

        var newAppRegistration = await authenticationClient.CreateApp("Versery", "https://github.com/AkazaRenn/Versery/", null, GranularScope.Read, GranularScope.Write, GranularScope.Follow);
        Credentials.AddAppRegistration(authenticationClient.Instance, JsonSerializer.Serialize(newAppRegistration));
}

    public async Task<bool> CheckLoginUrl(string url) {
        var match = oAuthRegex.Match(url);

        if (match.Success) {
            var code = match.Groups[1].Value;
            var auth = await authenticationClient.ConnectWithCode(code);
            var mastodonClient = new MastodonClient(authenticationClient.Instance, auth.AccessToken);
            var account = await mastodonClient.GetCurrentUser();
            var instance = await mastodonClient.GetInstanceV2();
            Credentials.AddAccessToken($"{account.UserName}@{instance.Domain}", mastodonClient.AccessToken);
            return true;
        }

        return false;
    }

    [GeneratedRegex(@"/oauth/authorize/native\?code=([a-zA-Z0-9_-]+)", RegexOptions.Compiled)]
    private static partial Regex OAuthRegex();
}
