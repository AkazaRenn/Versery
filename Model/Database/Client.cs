
namespace Model.Database;

internal sealed class Client: IDisposable {
    private Account Account { get; }
    private Status Status { get; }
    private Timeline Timeline { get; }

    public Client(string account) {
        Status = new(account);
        Account = new(account);
        Timeline = new(account);
    }

    public Entities.Timeline[] GetTimeline(uint count, string? afterId = null) {
        var timelines = Timeline.Get(count, afterId).ToArray();
        foreach (var timeline in timelines) {
            if (GetStatus(timeline.Id) is Entities.Status status) {
                GetAccount(status.Account);
                if ((status.Reblog is not null) && GetStatus(status.Reblog) is Entities.Status reblogStatus) {
                    GetAccount(reblogStatus.Account);
                }
            }
        }
        return timelines;
    }

    public List<Entities.Timeline> AddTimeline(IEnumerable<Server.Entities.Status> serverStatuses, string? afterId = null) {
        serverStatuses = serverStatuses.ToCollection();

        var dbTimeline = new List<Entities.Timeline>(serverStatuses.Count() + 1);
        foreach (var status in serverStatuses) {
            dbTimeline.Add(new Entities.Timeline(status));
        }

        Timeline.Add(dbTimeline, afterId);
        var flattened = serverStatuses.Flattened.DistinctBy(x => x.Id).ToCollection();
        Status.Add(flattened.Select(x => new Entities.Status(x)));
        Account.Add(flattened.Select(x => x.Account).DistinctBy(x => x.Id));

        return dbTimeline;
    }

    public Entities.Status? GetStatus(string id) {
        return Status.Get(id);
    }

    public Server.Entities.Account? GetAccount(string id) {
        return Account.Get(id);
    }

    public void Dispose() {
        Timeline.Dispose();
        Status.Dispose();
        Account.Dispose();
    }
}