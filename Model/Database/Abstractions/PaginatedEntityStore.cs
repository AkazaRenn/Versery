using Model.Interfaces;
using System.Text.Json.Serialization.Metadata;

namespace Model.Database.Abstractions;

internal abstract class PaginatedEntityStore<T>(string id, JsonTypeInfo<T> typeInfo): EntityStore<T>(id, 0, typeInfo)
    where T : class, IDatabaseEntity {
    public IEnumerable<T> Get(uint count, string? after = null) {
        var entities = Db.GetDescending(after).Take((int)count);
        foreach (var entity in entities) {
            yield return entity;
        }
    }

    public void Add(IEnumerable<T> entities, bool drop = false) {
        if (drop) {
            Drop();
        }

        base.Add(entities);
    }
}
