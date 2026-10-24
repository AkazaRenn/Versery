using Refit;

namespace Model.Server;

internal class AuthenticationClient {
    public IAppsApi Apps { get; }
    public IOauthApi OAuth { get; }

    public string Instance { get; }

    public AuthenticationClient(string instance) {
        Instance = instance;

        var httpClient = new HttpClient() {
            BaseAddress = new Uri($"https://{instance}"),
        };
        var refitSettings = Services.Get<RefitSettings>();

        Apps = RestService.ForGenerated<IAppsApi>(httpClient, refitSettings);
        OAuth = RestService.ForGenerated<IOauthApi>(httpClient, refitSettings);
    }
}
