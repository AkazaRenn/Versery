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

    public async Task<IEnumerable<Entities.Timeline>> GetTimeline(int count, string? afterId = null) {
        return await Task.Run(() => Timeline.Get(count, afterId).ToArray());
    }

    public async Task AddTimeline(IEnumerable<Mastonet.Entities.Status> serverStatuses, string? afterId = null) {
        await Task.Run(() => {
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
        });
    }

    public async Task<Entities.Status?> GetStatus(string id) {
        return await Task.Run(() => Status.Get(id));
    }

    public async Task AddStatuses(IEnumerable<Mastonet.Entities.Status> serverStatuses) {
        await Task.Run(() => {
            var flattened = serverStatuses.Flattened;
            Status.Add(Entities.Status.FromServer(flattened));
            Account.Add(Entities.Account.FromServer(flattened.Select(x => x.Account)));
        });
    }

    public async Task<Entities.Account?> GetAccount(string id) {
        return await Task.Run(() => Account.Get(id));
    }
}