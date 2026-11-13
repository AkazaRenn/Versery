using Microsoft.Extensions.Caching.Memory;

namespace Model.Database;

internal sealed class Status(string id): IDisposable {
    private readonly Store<Entities.Status> db = new(id, "Statuses", StoreJsonContext.Default.DatabaseStatus);

    public Entities.Status? Get(string id) {
        if (db.TryGet(id, out var status)) {
            return status;
        }
        return null;
    }

    public void Add(Entities.Status status) {
        db.Upsert(status.Id, status);
    }

    public void Add(IEnumerable<Entities.Status> statuses) {
        foreach (var status in statuses) {
            Add(status);
        }
    }

    public void Dispose() {
        db.Dispose();
    }
}
