using Model.Database.Abstractions;

namespace Model.Database.PaginatedEntityStores;

internal sealed class Timeline(string id)
    : PaginatedEntityStore<Entities.Timeline>(id, 1_000, StoreJsonContext.Default.Timeline);
