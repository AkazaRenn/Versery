using LiteDB;
using Mastonet.Entities;
using Model.Entities;
using Utilities;

namespace Model.Database;

internal sealed class Timeline {
    private readonly ILiteCollection<Entities.Timeline> entries;
    private readonly ILiteCollection<Entities.Status> statuses;

    public Timeline(string account, string name, ILiteCollection<Entities.Status> _statuses) {
        var db = Utilities.Services.Get<LiteDatabase>();
        entries = db.GetCollection<Entities.Timeline>($"{account}:{name}");
        entries.EnsureIndex(x => x.Id);
        statuses = _statuses;
    }

    //public IEnumerable<Entities.Status> Get(int count, UInt64? after = null) {
    //    var query = entries.Query().OrderByDescending(x => x);
    //    if (after is not null) {
    //        query = query.Where(x => x.Id < after);
    //    }

    //    foreach (var id in query.Limit(count).ToEnumerable()) {
    //        var statusOrNull = statuses.FindById(id.ToString());
    //        if (statusOrNull is Entities.Status status) {
    //            yield return status;
    //        }
    //    }
    //}

    internal IEnumerable<Entities.Timeline> Get(int count, UInt64? after = null) {
        var query = entries.Query().OrderByDescending(x => x);
        if (after is not null) {
            query = query.Where(x => x.Id < after);
        }
        return query.Limit(count).ToEnumerable();
    }

    internal void Add(MastodonList<Mastonet.Entities.Status> serverStatuses, UInt64? afterId) {
        var timelineEntries = new List<Entities.Timeline>(serverStatuses.Count + 1);
        var dbStatuses = new List<Entities.Status>(serverStatuses.Count);
        foreach (var serverStatus in serverStatuses) {
            if (!UInt64.TryParse(serverStatus.Id, out var statusId)) {
                continue;
            }
            timelineEntries.Add(new Entities.Timeline {
                Id = statusId,
                FollowedByGap = false,
            });
            dbStatuses.Add(new(serverStatus));
        }

        if (serverStatuses.Count >= Constants.StatusesCountPerLoad) {
            timelineEntries[^1].FollowedByGap = true;
        }

        if (afterId is not null) {
            entries.TryUpdate(afterId, x => x.FollowedByGap = false);
        }

        entries.Upsert(timelineEntries);
        statuses.Upsert(dbStatuses);
    }
}
