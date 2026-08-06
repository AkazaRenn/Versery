using LiteDB;

namespace Model.Database;

internal sealed class Timeline {
    private readonly ILiteCollection<Entities.Timeline> db;
    private readonly HashSet<string> accessedTimeline = [];

    public IReadOnlyCollection<string> AccessedTimeline => accessedTimeline;

    public Timeline(string hash, string name) {
        db = Services.Get<LiteDatabase>().GetCollection<Entities.Timeline>($"account_{hash}_{name}");
        db.EnsureIndex(x => x.CreatedAt);
    }

    internal IEnumerable<Entities.Timeline> Get(uint count, string? after = null) {
        var query = db.Query();

        if (!String.IsNullOrEmpty(after)) {
            var afterEntry = db.FindById(after);
            if (afterEntry is not null) {
                query = query.Where(x => x.CreatedAt <= afterEntry.CreatedAt);
            }
        }

        var statuses = query.OrderByDescending(x => x.CreatedAt).Limit((int)count).ToEnumerable();
        foreach (var status in statuses) {
            if (accessedTimeline.Contains(status.Id)) {
                continue;
            }
            accessedTimeline.Add(status.Id);
            yield return status;
        }
    }

    internal void Add(IEnumerable<Entities.Timeline> statuses, string? afterId) {
        if (afterId is not null) {
            db.TryUpdate(afterId, x => x.FollowedByGap = false);
        }

        statuses = statuses.ToCollection();

        db.Upsert(statuses);
        foreach (var status in statuses) {
            accessedTimeline.Add(status.Id);
        }
    }
}
