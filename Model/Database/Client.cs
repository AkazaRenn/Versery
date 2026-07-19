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

    public IEnumerable<Entities.Timeline> GetTimeline(int count, string? afterId = null) {
        return Timeline.Get(count, afterId);
    }

    public void AddTimeline(IEnumerable<Mastonet.Entities.Status> serverStatuses, string? afterId = null) {
        var dbTimeline = new List<Entities.Timeline>(serverStatuses.Count() + 1);
        foreach (var status in serverStatuses) {
            dbTimeline.Add(new Entities.Timeline {
                Id = status.Id,
                CreatedAt = status.CreatedAt,
                FollowedByGap = false,
            });
        }
        if (serverStatuses.Count() >= Constants.StatusesCountPerLoad) {
            dbTimeline[^1].FollowedByGap = true;
        }
        Timeline.Add(dbTimeline, afterId);
    }

    public Entities.Status? GetStatus(string id) {
        return Status.Get(id);
    }

    public void AddStatuses(IEnumerable<Mastonet.Entities.Status> serverStatuses) {
        var flattened = serverStatuses.Flattened;
        Status.Add(Entities.Status.FromServer(flattened));
        Account.Add(Entities.Account.FromServer(flattened.Select(x => x.Account)));
    }

    public Entities.Account? GetAccount(string id) {
        return Account.Get(id);
    }
}