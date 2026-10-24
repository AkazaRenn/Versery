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
        // Flushes the stores so the next launch doesn't rely on write-ahead log replay.
        AppDomain.CurrentDomain.ProcessExit += (_, _) => database?.Dispose();

        var user = applicationStates.ActiveUser;
        if (string.IsNullOrWhiteSpace(user)) {
            return;
        }

        var token = Credentials.GetAccessToken(user);
        if (string.IsNullOrWhiteSpace(token)) {
            return;
        }

        var split = user.Split('@');
        if (split.Length != 2) {
            return;
        }

        var instance = split.Last();
        server = new(instance, token);
        database = new(user);
    }

    internal async Task NewUser(string instance, string accessToken) {
        server = new(instance, accessToken);
        var serverAccount = await server.Accounts.GetAccountVerifyCredentials();
        var instanceInfo = await server.Instance.GetInstanceV2();
        var canonicalInstance = instanceInfo.Domain.Trim().ToLowerInvariant();
        var userId = $"{serverAccount.Username}@{canonicalInstance}";

        database?.Dispose();
        database = new Database.Client(userId);
        account = database.AddAccount(serverAccount);

        Credentials.AddAccessToken(userId, server.AccessToken);
        applicationStates.ActiveUser = userId;

        StrongReferenceMessenger.Default.Send(new Messages.SignInCompleted());
    }

    public async Task<Entities.Account?> GetAccount() {
        if ((account is null) && SignedIn) {
            var serverAccount = await server!.Accounts.GetAccountVerifyCredentials();
            account = database!.AddAccount(serverAccount);
        }

        return account;
    }

    public async Task<Entities.Timeline[]> GetTimelineFromServer(TimelineType type, string? afterId = null) {
        if (!SignedIn) {
            throw new InvalidOperationException("Client is not signed in");
        }

        var serverStatuses = type switch {
            TimelineType.Home => await server!.Timelines.GetTimelineHome(limit: Constants.StatusesCountPerLoad, max_id: afterId),
            TimelineType.Federated => await server!.Timelines.GetTimelinePublic(limit: Constants.StatusesCountPerLoad, local: false, max_id: afterId),
            TimelineType.Local => await server!.Timelines.GetTimelinePublic(limit: Constants.StatusesCountPerLoad, local: true, max_id: afterId),
            _ => throw new ArgumentException("Invalid timeline type", nameof(type)),
        };

        return [.. database!.AddTimeline(serverStatuses, afterId)];
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
