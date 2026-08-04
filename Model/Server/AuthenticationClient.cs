using Model.Server.Methods;
using Refit;

namespace Model.Server;

internal class AuthenticationClient {
    public IApps Apps { get; }
    public IOAuth OAuth { get; }

    public string Instance { get; }

    public AuthenticationClient(string instance) {
        Instance = instance;

        var httpClient = new HttpClient() {
            BaseAddress = new Uri($"https://{instance}"),
        };
        var refitSettings = Services.Get<RefitSettings>();

        Apps = RestService.For<IApps>(httpClient, refitSettings);
        OAuth = RestService.For<IOAuth>(httpClient, refitSettings);
    }
}
