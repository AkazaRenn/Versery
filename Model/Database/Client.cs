using LiteDB;
using Model.Entities;
using Utilities;

namespace Model.Database;

internal sealed class Client {
    internal ILiteCollection<Account> Accounts { get; }
    internal ILiteCollection<Status> Statuses { get; }
    internal Timeline HomeTimeline { get; }
    internal Timeline FederatedTimeline { get; }
    internal Timeline LocalTimeline { get; }

    public Client(string account, string instance) {
        var sha256 = instance.Sha256;
        Statuses = Utilities.Services.Get<LiteDatabase>().GetCollection<Status>($"instance_{sha256}_statuses");
        Accounts = Utilities.Services.Get<LiteDatabase>().GetCollection<Account>($"instance_{sha256}_accounts");

        HomeTimeline = new(account, "hometimeline", Statuses);
        FederatedTimeline = new(account, "federatedtimeline", Statuses);
        LocalTimeline = new(account, "localtimeline", Statuses);
    }

    public void AddAccounts(IEnumerable<Mastonet.Entities.Status> statuses) {
        var accounts = statuses.Select(x => new Account(x.Account));
        Accounts.Upsert(accounts);
    }
}
