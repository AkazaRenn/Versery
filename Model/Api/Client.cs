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
            mastodonClient?.GetFullUserId().ContinueWith(task => {
                if (task.IsCompletedSuccessfully) {
                    var userId = task.Result;
                    Credentials.AddAccessToken(userId, mastodonClient.AccessToken);
                    applicationStates.ActiveUser = userId;
                }
            });
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
        MastodonClient = new MastodonClient(instance, token);
    }
}
