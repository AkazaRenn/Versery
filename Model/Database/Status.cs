using LiteDB;
using System.Runtime.Caching;

namespace Model.Database;

internal class Status(string hash) {
    private static readonly MemoryCache cache = new(typeof(Status).FullName);
    private readonly ILiteCollection<Entities.Status> db = Services.Get<LiteDatabase>().GetCollection<Entities.Status>($"instance_{hash}_statuses");

    public Entities.Status? Get(string id) {
        if (cache[id] is Entities.Status status) {
            return status;
        }
        status = db.FindById(id);
        if (status != null) {
            cache[id] = status;
        }
        return status;
    }

    public void Add(Entities.Status status) {
        db.Upsert(status);
        cache[status.Id] = status;
    }

    public void Add(IEnumerable<Entities.Status> statuses) {
        statuses = statuses.ToCollection();

        db.Upsert(statuses);
        foreach (var status in statuses) {
            cache[status.Id] = status;
        }
    }
}
