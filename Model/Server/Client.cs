using Refit;

namespace Model.Server;

internal sealed class Client {
    public string InstanceUrl { get; }
    public string AccessToken { get; }

    public IAccountsApi Accounts { get; }
    public IInstanceApi Instance { get; }
    public ITimelinesApi Timelines { get; }

    public Client(string instance, string accessToken) {
        InstanceUrl = instance;
        AccessToken = accessToken;

        var httpClient = new HttpClient() {
            BaseAddress = new($"https://{InstanceUrl}")
        };
        httpClient.DefaultRequestHeaders.Authorization = new("Bearer", AccessToken);
        var refitSettings = Services.Get<RefitSettings>();

        Accounts = RestService.ForGenerated<IAccountsApi>(httpClient, refitSettings);
        Instance = RestService.ForGenerated<IInstanceApi>(httpClient, refitSettings);
        Timelines = RestService.ForGenerated<ITimelinesApi>(httpClient, refitSettings);
    }
}
