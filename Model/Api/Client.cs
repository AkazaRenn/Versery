using Mastonet;
using Model.DataPersistence;

namespace Model.Api;
public class Client {
    private readonly ApplicationStates applicationStates;

    private MastodonClient? mastodonClient = null;
    public MastodonClient? MastodonClient {
        get => mastodonClient;
        set {
            mastodonClient = value;
            _ = UpdateUserId();
        }
    }

    public bool Ready => MastodonClient != null;

    public Client(ApplicationStates _applicationStates) {
        applicationStates = _applicationStates;

        var user = applicationStates.ActiveUser;
        if (string.IsNullOrEmpty(user)) {
            return;
        }

        var token = Credentials.GetAccessToken(applicationStates.ActiveUser);
        if (string.IsNullOrEmpty(token)) {
            return;
        }

        var instance = user.Split('@').Last();
        mastodonClient = new MastodonClient(instance, token);
    }

    private async Task UpdateUserId() {
        if (mastodonClient == null) {
            return;
        }
        var userId = await mastodonClient.GetFullUserId();
        Credentials.AddAccessToken(userId, mastodonClient.AccessToken);
        applicationStates.ActiveUser = userId;
    }
}
