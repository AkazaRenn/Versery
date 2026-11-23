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
        var account = await AccountTask;
        var instance = await InstanceTask;
        var canonicalInstance = instance.Domain.Trim().ToLowerInvariant();
        var userId = $"{account.Username}@{canonicalInstance}";

        database?.Dispose();
        database = new(userId);

        Credentials.AddAccessToken(userId, accessToken);
        applicationStates.ActiveUser = userId;

        StrongReferenceMessenger.Default.Send(new Messages.SignInCompleted());
    }

    private readonly Lock instanceLock = new();
    public Task<Instance> InstanceTask {
        get {
            lock (instanceLock) {
                return field ??= server.Instance.GetInstanceV2();
            }
        }
    } = null;

    private readonly Lock accountLock = new();
    public Task<CredentialAccount> AccountTask {
        get {
            lock (accountLock) {
                return field ??= server.Accounts.GetAccountVerifyCredentials();
            }
        }
    } = null;

    private readonly Lock emojisLock = new();
    public Task<IReadOnlyList<CustomEmoji>> EmojisTask {
        get {
            lock (emojisLock) {
                return field ??= GetCustomEmojisVisibleInPicker();
            }
        }
    } = null;
    private async Task<IReadOnlyList<CustomEmoji>> GetCustomEmojisVisibleInPicker() {
        return [.. (await server.CustomEmojis.GetCustomEmojis()).Where(x => x.VisibleInPicker)];
    }

    public async Task<Entities.Timeline[]> GetTimelineFromServer(TimelineType type, uint count, string? afterId = null) {
        var serverStatuses = type switch {
            TimelineType.Home => await server.Timelines.GetTimelineHome(limit: (int)count, max_id: afterId),
            TimelineType.Federated => await server.Timelines.GetTimelinePublic(limit: (int)count, local: false, max_id: afterId),
            TimelineType.Local => await server.Timelines.GetTimelinePublic(limit: (int)count, local: true, max_id: afterId),
            _ => throw new ArgumentException("Invalid timeline type", nameof(type)),
        };

        return [.. database.AddTimeline(serverStatuses, afterId)];
    }

    public Entities.Timeline[] GetTimelineFromDatabase(uint count, string? afterId = null) {
        return database.GetTimeline(count, afterId);
    }

    public async Task<Entities.Notification[]> GetNotificationFromServer(uint count, string? afterId = null) {
        var serverNotifications = await server.Notifications.GetNotificationsV2(limit: (int)count, max_id: afterId);
        return [.. database.AddNotification(serverNotifications, afterId)];
    }

    public Entities.Notification[] GetNotificationFromDatabase(uint count, string? afterId = null) {
        return database.GetNotification(count, afterId);
    }

    public Entities.Status? GetStatus(string id) {
        return database.GetStatus(id);
    }

    public async Task PostStatus(TextStatus textStatus) {
        await server.Status.CreateStatus(textStatus);
    }

    public async Task<Status> FavouriteStatus(string id, bool state) {
        return state ? await server.Status.PostStatusFavourite(id) : await server.Status.PostStatusUnfavourite(id);
    }

    public async Task<Status> ReblogStatus(string id, bool state) {
        return state ? await server.Status.PostStatusReblog(id) : await server.Status.PostStatusUnreblog(id);
    }

    public Account? GetAccount(string id) {
        return database.GetAccount(id);
    }
}
