using LiteDB;

namespace Model.DataPersistence;

public record struct Status {
    [BsonId]
    public string Id { get; set; }
    public bool Favourited { get; set; }
    public bool Reblogged { get; set; }
    public bool Muted { get; set; }
    public bool Bookmarked { get; set; }
    public bool Pinned { get; set; }

    public Status(Mastonet.Entities.Status apiStatus) {
        Id = apiStatus.Id;
        Favourited = apiStatus.Favourited ?? false;
        Reblogged = apiStatus.Reblogged ?? false;
        Muted = apiStatus.Muted ?? false;
        Bookmarked = apiStatus.Bookmarked ?? false;
        Pinned = apiStatus.Pinned ?? false;
    }
}

public class Timeline(LiteDatabase db, string account, string name) {
    private readonly ILiteCollection<Status?> statuses = db.GetCollection<Status?>($"{account}:statuses");
    private readonly ILiteCollection<string> statusKeys = db.GetCollection<string>($"{account}:{name}");

    public IEnumerable<Status> StatusIterator {
        get {
            foreach (var id in statusKeys.FindAll()) {
                var statusOrNull = statuses.FindById(id);
                if (statusOrNull is Status status) {
                    yield return status;
                }
            }
        }
    }
}

public class Account(LiteDatabase db, string id) {
    public string Id => id;
    public Timeline HomeTimeline { get; } = new(db, id, "home_timeline");
    public Timeline FederatedTimeline { get; } = new(db, id, "federated_timeline");
    public Timeline LocalTimeline { get; } = new(db, id, "local_timeline");
}
