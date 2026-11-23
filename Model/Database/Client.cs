
using Model.Database.EntityStores;
using Model.Database.PaginatedEntityStores;

namespace Model.Database;

internal sealed class Client(string account): IDisposable {
    private Timeline? timeline = new(account);
    private Timeline Timeline => timeline ?? throw new ObjectDisposedException(nameof(Client));
    private Notification? notification = new(account);
    private Notification Notification => notification ?? throw new ObjectDisposedException(nameof(Client));
    private Status? status = new(account);
    private Status Status => status ?? throw new ObjectDisposedException(nameof(Client));
    private Account? accountEntity = new(account);
    private Account Account => accountEntity ?? throw new ObjectDisposedException(nameof(Client));

    public Entities.Timeline[] GetTimeline(uint count, string? afterId = null) {
        return [.. Timeline.Get(count, afterId)];
    }

    public Entities.Timeline[] AddTimeline(IEnumerable<Server.Entities.Status> serverStatuses, string? afterId = null) {
        serverStatuses = serverStatuses.ToCollection();
        var flattened = serverStatuses.Flattened.DistinctBy(x => x.Id).ToCollection();
        Status.Add(flattened.Select(x => new Entities.Status(x)));
        var accounts = flattened.DistinctBy(x => x.Account.Id).Select(x => new Entities.Account(x.Account));
        Account.Add(accounts);

        var dbTimelines = serverStatuses.Select(x => new Entities.Timeline(x)).ToArray();
        Timeline.Add(dbTimelines, afterId is null);
        return dbTimelines;
    }

    public Entities.Notification[] GetNotification(uint count, string? afterId = null) {
        return [.. Notification.Get(count, afterId)];
    }

    public Entities.Notification[] AddNotification(Server.Entities.GroupedNotificationsResults notifications, string? afterId = null) {
        var flattened = notifications.Statuses.Flattened.DistinctBy(x => x.Id).ToCollection();
        Status.Add(flattened.Select(x => new Entities.Status(x)));
        var accounts = notifications.Accounts.Concat(flattened.Select(x => x.Account)).DistinctBy(x => x.Id).Select(x => new Entities.Account(x));
        Account.Add(accounts);

        var dbNotifications = notifications.NotificationGroups.Select(x => new Entities.Notification(x)).ToArray();
        Notification.Add(dbNotifications, afterId is null);
        return dbNotifications;
    }

    public Entities.Status? GetStatus(string id) {
        return Status.Get(id);
    }

    public Entities.Account? GetAccount(string id) {
        return Account.Get(id);
    }

    public void Dispose() {
        Timeline.Dispose();
        timeline = null;
        Notification.Dispose();
        notification = null;
        Status.Dispose();
        status = null;
        Account.Dispose();
        accountEntity = null;
    }
}