
namespace Model.Database;

internal sealed class Client {
    private Account Account { get; }
    private Status Status { get; }
    private Timeline Timeline { get; }

    public Client(string account, string instance) {
        var hash = account.Sha256;
        Status = new Status(hash);
        Account = new Account(hash);
        Timeline = new(hash, "hometimeline");
        //FederatedTimeline = new(accountHash, "federatedtimeline");
        //LocalTimeline = new(accountHash, "localtimeline");
    }

    public Entities.Timeline[] GetTimeline(uint count, string? afterId = null) {
        var timelines = Timeline.Get(count, afterId).ToArray();
        foreach (var timeline in timelines) {
            if (GetStatus(timeline.Id) is Entities.Status status) {
                GetAccount(status.AccountId);
                if ((status.ReblogId is not null) && GetStatus(status.ReblogId) is Entities.Status reblogStatus) {
                    GetAccount(reblogStatus.AccountId);
                }
            }
        }
        return timelines;
    }

    public void AddTimeline(IEnumerable<Model.Server.Entities.Status> serverStatuses, string? afterId = null) {
        serverStatuses = serverStatuses.ToCollection();

        var dbTimeline = new List<Entities.Timeline>(serverStatuses.Count() + 1);
        foreach (var status in serverStatuses) {
            dbTimeline.Add(new Entities.Timeline(status));
        }
        if (serverStatuses.Count() >= Constants.StatusesCountPerLoad) {
            dbTimeline[^1].FollowedByGap = true;
        }
        Timeline.Add(dbTimeline, afterId);

        var flattened = serverStatuses.Flattened.DistinctBy(x => x.Id).ToCollection();
        Status.Add(Entities.Status.FromServer(flattened));
        Account.Add(Entities.Account.FromServer(flattened.Select(x => x.Account).DistinctBy(x => x.Id)));
    }

    public Entities.Status? GetStatus(string id) {
        return Status.Get(id);
    }

    public Entities.Account? GetAccount(string id) {
        return Account.Get(id);
    }

    public Entities.Account AddAccount(Model.Server.Entities.Account serverAccount) {
        var account = new Entities.Account(serverAccount);
        Account.Add(account);
        return account;
    }
}