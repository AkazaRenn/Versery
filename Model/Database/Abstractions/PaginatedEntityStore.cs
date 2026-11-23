using Model.Interfaces;
using System.Text.Json.Serialization.Metadata;

namespace Model.Database.Abstractions;

internal abstract class PaginatedEntityStore<T>(string id, JsonTypeInfo<T> typeInfo): IDisposable
    where T : class, IDatabaseEntity {
    private Store<T>? db = new(id, typeof(T).Name, 0, typeInfo);
    private Store<T> Db => db ?? throw new ObjectDisposedException(nameof(PaginatedEntityStore<>));

    internal IEnumerable<T> Get(uint count, string? after = null) {
        var entities = Db.GetDescending(after).Take((int)count);
        foreach (var entity in entities) {
            yield return entity;
        }
    }

    internal void Add(IEnumerable<T> entities, string? afterId) {
        if (afterId is null) {
            Drop();
        }

        foreach (var entity in entities) {
            Db.Upsert(entity.Id, entity);
        }
    }

    private void Drop() {
        Db.Dispose();
        Directory.Delete(Db.DataDirectory, true);
        db = new(id, typeof(T).Name, 0, typeInfo);
    }

    public void Dispose() {
        Db.Dispose();
        db = null;
    }
}
