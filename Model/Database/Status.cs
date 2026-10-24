using Microsoft.Extensions.Caching.Memory;

namespace Model.Database;

internal sealed class Status(string id): IDisposable {
    private readonly MemoryCache cache = new(new MemoryCacheOptions { SizeLimit = 2048 });
    private readonly MemoryCacheEntryOptions entryOptions = new() { Size = 1 };
    private readonly Store<Entities.Status> db = new(id, "Statuses", StoreJsonContext.Default.Status);

    public Entities.Status? Get(string id) {
        if (cache.TryGetValue(id, out Entities.Status? cached)) {
            return cached;
        }
        if (db.TryGet(id, out var status)) {
            cache.Set(id, status, entryOptions);
        }
        return status;
    }

    public void Add(Entities.Status status) {
        db.Upsert(status.Id, status);
        cache.Set(status.Id, status, entryOptions);
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
