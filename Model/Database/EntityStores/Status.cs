using Model.Database.Abstractions;

namespace Model.Database.EntityStores;

internal sealed class Status(string id)
    : EntityStore<Entities.Status>(id, 2_000, StoreJsonContext.Default.DatabaseStatus);
