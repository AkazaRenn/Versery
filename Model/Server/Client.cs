using Model.Server.Methods;
using Refit;

namespace Model.Server;

internal sealed class Client {
    public string InstanceUrl { get; }
    public string AccessToken { get; }

    public IAccounts Accounts { get; }
    public IInstance Instance { get; }
    public ITimelines Timelines { get; }

    public Client(string instance, string accessToken) {
        InstanceUrl = instance;
        AccessToken = accessToken;

        var httpClient = new HttpClient() {
            BaseAddress = new($"https://{InstanceUrl}")
        };
        httpClient.DefaultRequestHeaders.Authorization = new("Bearer", AccessToken);
        var refitSettings = Services.Get<RefitSettings>();

        Accounts = RestService.For<IAccounts>(httpClient, refitSettings);
        Instance = RestService.For<IInstance>(httpClient, refitSettings);
        Timelines = RestService.For<ITimelines>(httpClient, refitSettings);
    }
}
