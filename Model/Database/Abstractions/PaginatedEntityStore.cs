using Model.Interfaces;
using System.Text.Json.Serialization.Metadata;

namespace Model.Database.Abstractions;

internal abstract class PaginatedEntityStore<T>(string id, int maxItemCount, JsonTypeInfo<T> typeInfo): IDisposable
    where T : class, IPaginatedEntity {
    private Store<T>? db = new(id, typeof(T).Name, maxItemCount, typeInfo);
    private Store<T> Db => db ?? throw new ObjectDisposedException(nameof(PaginatedEntityStore<>));

    internal IEnumerable<T> Get(uint count, string? after = null) {
        var entities = Db.GetDescending(after).Take((int)count);
        foreach (var entity in entities) {
            yield return entity;
        }
    }

    internal void Add(IEnumerable<T> entities, string? afterId, bool clear = false) {
        entities = entities.ToCollection();

        if ((afterId is null) && clear) {
            Drop();
            entities.Last().FollowedByGap = true;
        } else {
            if ((afterId is not null) && Db.TryGet(afterId, out var afterEntry)) {
                afterEntry.FollowedByGap = false;
                Db.Upsert(afterEntry.Id, afterEntry);
            }
            if (entities.Any() && !Db.Contains(entities.Last().Id)) {
                entities.Last().FollowedByGap = true;
            }
        }

        foreach (var entity in entities) {
            Db.Upsert(entity.Id, entity);
        }
    }

    private void Drop() {
        Db.Dispose();
        Directory.Delete(Db.DataDirectory, true);
        db = new(id, typeof(T).Name, maxItemCount, typeInfo);
    }

    public void Dispose() {
        Db.Dispose();
        db = null;
    }
}
