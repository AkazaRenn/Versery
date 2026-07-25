using Utilities;

namespace Model.Database;
internal sealed class Client {
    private Account Account { get; }
    private Status Status { get; }
    private Timeline Timeline { get; }

    public Client(string account, string instance) {
        var instanceHash = instance.Sha256;
        Status = new Status(instanceHash);
        Account = new Account(instanceHash);

        var accountHash = account.Sha256;
        Timeline = new(accountHash, "hometimeline");
        //FederatedTimeline = new(accountHash, "federatedtimeline");
        //LocalTimeline = new(accountHash, "localtimeline");
    }

    public async Task<Entities.Timeline[]> GetTimeline(int count, string? afterId = null) {
        var timelines = await Task.Run(() => Timeline.Get(count, afterId).ToArray());
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

    public async Task AddTimeline(IEnumerable<Mastonet.Entities.Status> serverStatuses, string? afterId = null) {
        await Task.Run(() => {
            serverStatuses = serverStatuses.ToCollection();

            var dbTimeline = new List<Entities.Timeline>(serverStatuses.Count() + 1);
            foreach (var status in serverStatuses) {
                dbTimeline.Add(new Entities.Timeline {
                    Id = status.Id,
                    CreatedAt = status.CreatedAt,
                });
            }
            if (serverStatuses.Count() >= Constants.StatusesCountPerLoad) {
                dbTimeline[^1].FollowedByGap = true;
            }
            Timeline.Add(dbTimeline, afterId);

            var flattened = serverStatuses.Flattened.ToCollection();
            Status.Add(Entities.Status.FromServer(flattened));
            Account.Add(Entities.Account.FromServer(flattened.Select(x => x.Account)));
        });
    }

    public Entities.Status? GetStatus(string id) {
        return Status.Get(id);
    }

    public Entities.Account? GetAccount(string id) {
        return Account.Get(id);
    }
}