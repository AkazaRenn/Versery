using LiteDB;
using Mastonet.Entities;
using Microsoft.Extensions.DependencyInjection;
using Model.Database.Entities;
using Utilities;

namespace Model.Database;

internal sealed class Timeline {
    private readonly ILiteCollection<TimelineEntry> entries;
    private readonly ILiteCollection<Entities.StatusEntry> statuses;

    public Timeline(string account, string name) {
        var db = Utilities.Services.Provider.GetRequiredService<LiteDatabase>();
        entries = db.GetCollection<TimelineEntry>($"{account}:{name}");
        entries.EnsureIndex(x => x.Id);
        statuses = db.GetCollection<Entities.StatusEntry>($"{account}:statuses");
    }

    public IEnumerable<Entities.StatusEntry> Statuses(UInt16 count, UInt64? after = null) {
        var query = entries.Query().OrderByDescending(x => x);
        if (after is not null) {
            query = query.Where(x => x.Id < after);
        }

        foreach (var id in query.Limit((Int32)count).ToEnumerable()) {
            var statusOrNull = statuses.FindById(id.ToString());
            if (statusOrNull is Entities.StatusEntry status) {
                yield return status;
            }
        }
    }

    internal void Add(MastodonList<Mastonet.Entities.Status> apiStatuses, UInt64? afterId) {
        var timelineEntries = new List<TimelineEntry>(apiStatuses.Count + 1);
        var dbStatuses = new List<Entities.StatusEntry>(apiStatuses.Count);
        foreach (var apiStatus in apiStatuses) {
            if (!UInt64.TryParse(apiStatus.Id, out var statusId)) {
                continue;
            }
            timelineEntries.Add(new TimelineEntry {
                Id = statusId,
                FollowedByGap = false,
            });
            dbStatuses.Add(new Data.Entities.Status(apiStatus));
        }

        if (apiStatuses.Count >= Constants.StatusesCountPerLoad) {
            timelineEntries[^1].FollowedByGap = true;
        }

        if (afterId is not null) {
            var afterEntryOrNull = entries.FindById(afterId);
            if (afterEntryOrNull is { } afterEntry) {
                afterEntry.FollowedByGap = false;
                timelineEntries.Add(afterEntry);
            }
        }

        entries.Upsert(timelineEntries);
        statuses.Upsert(dbStatuses);
    }
}
