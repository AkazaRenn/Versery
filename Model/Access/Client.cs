using CommunityToolkit.Mvvm.Messaging;
using Mastonet;
using Model.DataPersistence;
using Model.Enums;
using Utilities;

namespace Model.Access;
public sealed class Client {
    private readonly ApplicationStates applicationStates = Utilities.Services.Get<ApplicationStates>();
    private readonly HttpClient httpClient = Utilities.Services.Get<HttpClient>();
    private Database.Client? database;
    private MastodonClient? server;
    private Mastonet.Entities.Account? account;

    public bool SignedIn => server is not null && database is not null;

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
        server = new(instance, token, httpClient);
        database = new(user, instance);
    }

    internal async Task NewUser(string instance, string accessToken) {
        server = new(instance, accessToken, httpClient);
        account = await server.GetCurrentUser();
        var username = account.UserName;
        database = new Database.Client(account.UserName, instance);

        var userId = $"{username}@{instance}";
        Credentials.AddAccessToken(userId, server.AccessToken);
        applicationStates.ActiveUser = userId;

        WeakReferenceMessenger.Default.Send(new Messages.SignInCompleted());
    }

    public async Task<Mastonet.Entities.Account?> GetAccount() {
        if (account is null) {
            if (server is not null) {
                account = await server.GetCurrentUser();
            }
        }

        return account;
    }

    public async Task<List<Entities.Timeline>> GetTimelineFromServer(TimelineType type, string? afterId = null) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }

        ArrayOptions arrayOptions = new() {
            Limit = Constants.StatusesCountPerLoad,
            MaxId = afterId?.ToString(),
        };

        var serverStatuses = type switch {
            TimelineType.Home => await server!.GetHomeTimeline(arrayOptions),
            TimelineType.Federated => await server!.GetPublicTimeline(arrayOptions, local: false),
            TimelineType.Local => await server!.GetPublicTimeline(arrayOptions, local: true),
            _ => throw new ArgumentException("Invalid timeline type", nameof(type)),
        };

        await database!.AddTimeline(serverStatuses, afterId);
        await database.AddStatuses(serverStatuses);

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

    public async Task<IEnumerable<Entities.Timeline>> GetTimelineFromDatabase(string? afterId = null) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }
        return await database!.GetTimeline(Constants.StatusesCountPerLoad, afterId);
    }

    public async Task<Entities.Status?> GetStatus(string id) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }
        return await database!.GetStatus(id);
    }

    public async Task<Entities.Account?> GetAccount(string id) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }
        return await database!.GetAccount(id);
    }
}
