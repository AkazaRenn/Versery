using Model.Database.Abstractions;

namespace Model.Database.PaginatedEntityStores;

internal sealed class Timeline(string id)
    : PaginatedEntityStore<Entities.Timeline>(id, StoreJsonContext.Default.Timeline);
