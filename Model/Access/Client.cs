using CommunityToolkit.Mvvm.Messaging;
using Model.DataPersistence;
using Model.Enumerations;
using Model.Server;

namespace Model.Access;

public sealed class Client {
    private static readonly HttpClient httpClient = Services.Get<HttpClient>();
    private readonly ApplicationStates applicationStates = Services.Get<ApplicationStates>();
    private Database.Client? database;
    private Server.Client? server;
    private Entities.Account? account;

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
        server = new(instance, token);
        database = new(user, instance);
    }

    internal async Task NewUser(string instance, string accessToken) {
        server = new(instance, accessToken);
        var serverAccount = await server.Accounts.GetVerifyCredentials();
        var username = serverAccount.UserName;
        database = new Database.Client(serverAccount.UserName, instance);
        account = database.AddAccount(serverAccount);

        var userId = $"{username}@{instance}";
        Credentials.AddAccessToken(userId, server.AccessToken);
        applicationStates.ActiveUser = userId;

        WeakReferenceMessenger.Default.Send(new Messages.SignInCompleted());
    }

    public async Task<Entities.Account?> GetAccount() {
        if ((account is null) && SignedIn) {
            var serverAccount = await server!.Accounts.GetVerifyCredentials();
            account = database!.AddAccount(serverAccount);
        }

        return account;
    }

    public async Task<Entities.Timeline[]> GetTimelineFromServer(TimelineType type, string? afterId = null) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }

        var serverStatuses = type switch {
            TimelineType.Home => await server!.Timelines.GetHome(maxId: afterId, limit: Constants.StatusesCountPerLoad),
            TimelineType.Federated => await server!.Timelines.GetPublic(maxId: afterId, limit: Constants.StatusesCountPerLoad, local: false),
            TimelineType.Local => await server!.Timelines.GetPublic(maxId: afterId, limit: Constants.StatusesCountPerLoad, local: true),
            _ => throw new ArgumentException("Invalid timeline type", nameof(type)),
        };

        database!.AddTimeline(serverStatuses, afterId);

        if (!serverStatuses.Any()) {
            return [];
        }

        var timeline = new Entities.Timeline[serverStatuses.Count];
        for (int i = 0; i < serverStatuses.Count; i++) {
            timeline[i] = new Entities.Timeline() {
                Id = serverStatuses[i].Id,
            };
        }

        return timeline;
    }

    public Entities.Timeline[] GetTimelineFromDatabase(string? afterId = null) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }
        return database!.GetTimeline(Constants.StatusesCountPerLoad, afterId);
    }

    public Entities.Status? GetStatus(string id) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }
        return database!.GetStatus(id);
    }

    public Entities.Account? GetAccount(string id) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }
        return database!.GetAccount(id);
    }
}
