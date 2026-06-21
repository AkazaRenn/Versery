using LiteDB;
using Model.Entities;

namespace Model.Database;

internal sealed class Client {

    internal string UserId;
    internal ILiteCollection<Status> Statuses { get; }
    internal Timeline HomeTimeline { get; }
    internal Timeline FederatedTimeline { get; }
    internal Timeline LocalTimeline { get; }

    public Client(string userId) {
        UserId = userId;
        Statuses = Utilities.Services.Get<LiteDatabase>().GetCollection<Status>($"{userId}:statuses");
        HomeTimeline = new(userId, "home_timeline", Statuses);
        FederatedTimeline = new(userId, "federated_timeline", Statuses);
        LocalTimeline = new(userId, "local_timeline", Statuses);
    }
}
