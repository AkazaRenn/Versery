using CommunityToolkit.Mvvm.Messaging;
using Model.DataPersistence;
using Model.Enumerations;

namespace Model.Access;

public sealed class Client {
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
        var serverAccount = await server.Accounts.VerifyCredentials();
        var username = serverAccount.Username;
        database = new Database.Client(serverAccount.Username, instance);
        account = database.AddAccount(serverAccount);

        var userId = $"{username}@{instance}";
        Credentials.AddAccessToken(userId, server.AccessToken);
        applicationStates.ActiveUser = userId;

        WeakReferenceMessenger.Default.Send(new WeakMessages.SignInCompleted());
    }

    public async Task<Entities.Account?> GetAccount() {
        if ((account is null) && SignedIn) {
            var serverAccount = await server!.Accounts.VerifyCredentials();
            account = database!.AddAccount(serverAccount);
        }

        return account;
    }

    public async Task<Entities.Timeline[]> GetTimelineFromServer(TimelineType type, string? afterId = null) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }

        var serverStatuses = type switch {
            TimelineType.Home => await server!.Timelines.Home(maxId: afterId, limit: Constants.StatusesCountPerLoad),
            TimelineType.Federated => await server!.Timelines.Public(maxId: afterId, limit: Constants.StatusesCountPerLoad, local: false),
            TimelineType.Local => await server!.Timelines.Public(maxId: afterId, limit: Constants.StatusesCountPerLoad, local: true),
            _ => throw new ArgumentException("Invalid timeline type", nameof(type)),
        };

        database!.AddTimeline(serverStatuses, afterId);

        if (serverStatuses.Count == 0) {
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

    public Entities.Timeline[] GetTimelineFromDatabase(string? afterId = null, uint count = Constants.StatusesCountPerLoad) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }
        return database!.GetTimeline(count, afterId);
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
