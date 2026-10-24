namespace Model.Database;

internal sealed class Timeline(string id): IDisposable {
    private readonly Store<Entities.Timeline> db = new(id, "Timelines", StoreJsonContext.Default.Timeline);

    internal IEnumerable<Entities.Timeline> Get(uint count, string? after = null) {
        var timelines = db.GetDescending(after).Take((int)count);
        foreach (var status in timelines) {
            yield return status;
        }
    }

    internal void Add(IEnumerable<Entities.Timeline> timelines, string? afterId) {
        if ((afterId is not null) && db.TryGet(afterId, out var afterEntry)) {
            afterEntry.FollowedByGap = false;
            db.Upsert(afterEntry.Id, afterEntry);
        }

        timelines = timelines.ToCollection();
        if (timelines.Any() && !db.Contains(timelines.Last().Id)) {
            timelines.Last().FollowedByGap = true;
        }

        foreach (var timeline in timelines) {
            db.Upsert(timeline.Id, timeline);
        }
    }

    public void Dispose() {
        db.Dispose();
    }
}
