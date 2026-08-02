using Model.Server.Methods;
using Refit;
using System.Text.Json;

namespace Model.Server;

public class Client {
    private readonly RefitSettings settings = new() {
        ContentSerializer = new SystemTextJsonContentSerializer(JsonContext.Default.Options)
    };
    private readonly HttpClient apiHttpClient;

    public string InstanceUrl { get; }
    public string AccessToken { get; }

    public IAccounts Accounts { get; }
    public IInstance Instance { get; }
    public ITimelines Timelines { get; }

    public Client(string instance, string accessToken) {
        InstanceUrl = instance;
        AccessToken = accessToken;

        apiHttpClient = new() {
            BaseAddress = new Uri($"https://{InstanceUrl}")
        };
        apiHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);

        Accounts = RestService.For<IAccounts>(apiHttpClient, settings);
        Instance = RestService.For<IInstance>(apiHttpClient, settings);
        Timelines = RestService.For<ITimelines>(apiHttpClient, settings);
    }
}
