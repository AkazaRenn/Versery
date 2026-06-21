using LiteDB;
using Model.Entities;

namespace Model.Database;

internal sealed class Client {
    private readonly string username;
    private readonly string instance;

    internal ILiteCollection<Account> Accounts { get; }
    internal ILiteCollection<Status> Statuses { get; }
    internal Timeline HomeTimeline { get; }
    internal Timeline FederatedTimeline { get; }
    internal Timeline LocalTimeline { get; }

    public Client(string _username, string _instance) {
        username = _username;
        instance = _instance;
        Accounts = Utilities.Services.Get<LiteDatabase>().GetCollection<Account>($"{instance}:accounts");
        Statuses = Utilities.Services.Get<LiteDatabase>().GetCollection<Status>($"{instance}:statuses");

        HomeTimeline = new(username, "home_timeline", Statuses);
        FederatedTimeline = new(username, "federated_timeline", Statuses);
        LocalTimeline = new(username, "local_timeline", Statuses);
    }
}
