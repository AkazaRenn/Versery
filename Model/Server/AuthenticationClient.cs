using Model.Server.Methods;
using Refit;

namespace Model.Server;

internal class AuthenticationClient {
    private static readonly RefitSettings settings = new() {
        ContentSerializer = new SystemTextJsonContentSerializer(JsonContext.Default.Options)
    };
    private readonly HttpClient httpClient = Services.Get<HttpClient>();

    public IApps Apps { get; }
    public IOAuth OAuth { get; }

    public string Instance { get; }

    public AuthenticationClient(string instance) {
        Instance = instance;
        httpClient.BaseAddress = new Uri($"https://{instance}");

        Apps = RestService.For<IApps>(httpClient, settings);
        OAuth = RestService.For<IOAuth>(httpClient, settings);
    }
}
