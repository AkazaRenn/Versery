using Mastonet;
using Microsoft.Extensions.DependencyInjection;
using Model.Database.Entities;
using Model.DataPersistence;
using Utilities;

namespace Model.Api;
public sealed class Client {
    private readonly Database database = Utilities.Services.Provider.GetRequiredService<Database>();
    private readonly ApplicationStates applicationStates = Utilities.Services.Provider.GetRequiredService<ApplicationStates>();
    private MastodonClient? mastodonClient = null;

    public Client() {
        var user = applicationStates.ActiveUser;
        if (string.IsNullOrWhiteSpace(user)) {
            return;
        }

        var token = Credentials.GetAccessToken(applicationStates.ActiveUser);
        if (string.IsNullOrWhiteSpace(token)) {
            return;
        }

        var instance = user.Split('@').Last();
        mastodonClient = new MastodonClient(instance, token);
    }

    internal async Task NewUser(string instance, string accessToken) {
        mastodonClient = new(instance, accessToken);
        var userId = await mastodonClient.GetFullUserId();
        Credentials.AddAccessToken(userId, mastodonClient.AccessToken);
        applicationStates.ActiveUser = userId;
    }

    public async Task<ICollection<StatusEntry>> HomeTimeline(UInt64? afterId) {
        if (mastodonClient is null) {
            return Array.Empty<StatusEntry>();
        }

        var mastodonList = await mastodonClient.GetHomeTimeline();
        database.HomeTimeline.Add(mastodonList, afterId);

        if (mastodonList.Count == 0) {
            return Array.Empty<StatusEntry>();
        }
    }
}
