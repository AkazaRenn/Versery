using Mastonet;
using Mastonet.Entities;

namespace Model; 
public class Client {
    private AuthenticationClient? client;
    private AppRegistration? app;

    public Client() {
    }

    public async Task<string> OAuthUrl(string instanceUrl) {
        client = new AuthenticationClient(instanceUrl);
        app = await client.CreateApp("Versery", "https://github.com/AkazaRenn/Versery/", null, GranularScope.Read, GranularScope.Write, GranularScope.Follow);
        return client.OAuthUrl();
    }
}
