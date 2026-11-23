using Model.Interfaces;
using System.Text.Json.Serialization.Metadata;

namespace Model.Database.Abstractions;

internal abstract class EntityStore<T>(string id, int maxItemCount, JsonTypeInfo<T> typeInfo): IDisposable
    where T : class, IDatabaseEntity {
    private Store<T>? db = new(id, typeof(T).Name, maxItemCount, typeInfo);
    protected Store<T> Db => db ?? throw new ObjectDisposedException(nameof(EntityStore<>));

    public T? Get(string id) {
        if (Db.TryGet(id, out var entity)) {
            return entity;
        }
        return null;
    }

    public void Add(T entity) {
        Db.Upsert(entity.IndexId, entity);
    }

    public void Add(IEnumerable<T> entities) {
        foreach (var entity in entities) {
            Add(entity);
        }
    }

    public void Dispose() {
        Db.Dispose();
        db = null;
    }

    protected void Drop() {
        Db.Dispose();
        Directory.Delete(Db.DataDirectory, true);
        db = new(id, typeof(T).Name, 0, typeInfo);
    }
}
