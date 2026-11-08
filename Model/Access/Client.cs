using CommunityToolkit.Mvvm.Messaging;
using Model.DataPersistence;
using Model.Enumerations;
using Model.Server.Entities;

namespace Model.Access;

public sealed class Client {
    private readonly ApplicationStates applicationStates = Services.Get<ApplicationStates>();
    private Database.Client database = null!;
    private Server.Client server = null!;

    public bool SignedIn => server is not null && database is not null;

    public Client() {
        // Flushes the stores so the next launch doesn't rely on write-ahead log replay.
        AppDomain.CurrentDomain.ProcessExit += (_, _) => database?.Dispose();

        var userId = applicationStates.ActiveUser;
        if (string.IsNullOrWhiteSpace(userId)) {
            return;
        }
        database = new(userId);

        var split = userId.Split('@');
        if (split.Length != 2) {
            return;
        }
        var instanceId = split.Last();
        var accessToken = Credentials.GetAccessToken(userId);
        if (string.IsNullOrWhiteSpace(accessToken)) {
            return;
        }
        server = new(instanceId, accessToken);
    }

    internal async Task NewUser(string instanceId, string accessToken) {
        instanceId = instanceId.Trim().ToLowerInvariant();
        accessToken = accessToken.Trim();
        server = new(instanceId, accessToken);
        account = await server.Accounts.GetAccountVerifyCredentials();
        instance = await server.Instance.GetInstanceV2();
        var canonicalInstance = instance.Domain.Trim().ToLowerInvariant();
        var userId = $"{account.Username}@{canonicalInstance}";

        database?.Dispose();
        database = new(userId);
        database.AddAccount(account);

        Credentials.AddAccessToken(userId, accessToken);
        applicationStates.ActiveUser = userId;

        StrongReferenceMessenger.Default.Send(new Messages.SignInCompleted());
    }

    private Instance? instance;
    public async Task<Instance?> GetInstance() {
        instance ??= await server.Instance.GetInstanceV2();
        return instance;
    }

    private CredentialAccount? account;
    public async Task<CredentialAccount?> GetAccount() {
        if (account is null) {
            account = await server.Accounts.GetAccountVerifyCredentials();
            database.AddAccount(account);
        }
        return account;
    }

    private IReadOnlyList<CustomEmoji>? emojis = null;
    public async Task<IReadOnlyList<CustomEmoji>> GetCustomEmojis() {
        emojis ??= [.. await server.CustomEmojis.GetCustomEmojis()];
        return emojis;
    }

    public async Task<Entities.Timeline[]> GetTimelineFromServer(TimelineType type, string? afterId = null) {
        var serverStatuses = type switch {
            TimelineType.Home => await server.Timelines.GetTimelineHome(limit: Constants.StatusesCountPerLoad, max_id: afterId),
            TimelineType.Federated => await server.Timelines.GetTimelinePublic(limit: Constants.StatusesCountPerLoad, local: false, max_id: afterId),
            TimelineType.Local => await server.Timelines.GetTimelinePublic(limit: Constants.StatusesCountPerLoad, local: true, max_id: afterId),
            _ => throw new ArgumentException("Invalid timeline type", nameof(type)),
        };

        return [.. database.AddTimeline(serverStatuses, afterId)];
    }

    public Entities.Timeline[] GetTimelineFromDatabase(string? afterId = null, uint count = Constants.StatusesCountPerLoad) {
        return database.GetTimeline(count, afterId);
    }

    public Entities.Status? GetStatus(string id) {
        return database.GetStatus(id);
    }

    public Entities.Account? GetAccount(string id) {
        return database.GetAccount(id);
    }
}
