using CommunityToolkit.Mvvm.Messaging;
using Mastonet;
using Model.DataPersistence;
using Model.Enums;
using Utilities;

namespace Model.Access;
public sealed class Client {
    private readonly ApplicationStates applicationStates = Utilities.Services.Get<ApplicationStates>();
    private readonly HttpClient httpClient = Utilities.Services.Get<HttpClient>();
    private Database.Client? databaseClient = null;
    private MastodonClient? mastodonClient = null;

    public bool SignedIn => mastodonClient is not null && databaseClient is not null;

    public Client() {
        var user = applicationStates.ActiveUser;
        if (string.IsNullOrWhiteSpace(user)) {
            return;
        }

        var token = Credentials.GetAccessToken(applicationStates.ActiveUser);
        if (string.IsNullOrWhiteSpace(token)) {
            return;
        }

        var split = user.Split('@');
        if (split.Length != 2) {
            return;
        }

        var instance = split.Last();
        mastodonClient = new(instance, token, httpClient);
        databaseClient = new(user, instance);
    }

    internal async Task NewUser(string instance, string accessToken) {
        mastodonClient = new(instance, accessToken, httpClient);
        var username = (await mastodonClient.GetCurrentUser()).UserName;
        databaseClient = new Database.Client(username, instance);

        var userId = $"{username}@{instance}";
        Credentials.AddAccessToken(userId, mastodonClient.AccessToken);
        applicationStates.ActiveUser = userId;

        WeakReferenceMessenger.Default.Send(new Messages.SignInCompleted());
    }

    public async Task<IEnumerable<Entities.Timeline>> GetTimelineFromServer(TimelineType type, string? afterId = null) {
        if (!SignedIn) {
            return [];
        }

        Mastonet.ArrayOptions arrayOptions = new() {
            Limit = Constants.StatusesCountPerLoad,
            MaxId = afterId?.ToString(),
        };

        var (serverStatuses, databaseTimeline) = type switch {
            TimelineType.Home => (await mastodonClient!.GetHomeTimeline(arrayOptions), databaseClient!.HomeTimeline),
            TimelineType.Federated => (await mastodonClient!.GetPublicTimeline(arrayOptions, local: false), databaseClient!.FederatedTimeline),
            TimelineType.Local => (await mastodonClient!.GetPublicTimeline(arrayOptions, local: true), databaseClient!.LocalTimeline),
            _ => throw new ArgumentException("Invalid timeline type", nameof(type)),
        };

        databaseTimeline.Add(serverStatuses, afterId);
        databaseClient.AddAccounts(serverStatuses.Flattened);

        if (!serverStatuses.Any()) {
            return [];
        }

        List<Entities.Timeline> timeline = [];
        foreach (var serverStatus in serverStatuses) {
            timeline.Add(new Entities.Timeline() {
                Id = serverStatus.Id,
            });
        }

        return timeline;
    }

    public IEnumerable<Entities.Timeline> GetTimelineFromDatabase(TimelineType type, string? afterId = null) {
        if (!SignedIn) {
            return [];
        }

        Database.Timeline databaseTimeline = type switch {
            TimelineType.Home => databaseClient!.HomeTimeline,
            TimelineType.Federated => databaseClient!.FederatedTimeline,
            TimelineType.Local => databaseClient!.LocalTimeline,
            _ => throw new ArgumentException("Invalid timeline type", nameof(type)),
        };

        return databaseTimeline.Get(Constants.StatusesCountPerLoad, afterId);
    }

    public Entities.Status? GetStatus(string id) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }
        return databaseClient!.Statuses.FindById(id);
    }

    public Entities.Account? GetAccount(string id) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }
        return databaseClient!.Accounts.FindById(id);
    }
}
