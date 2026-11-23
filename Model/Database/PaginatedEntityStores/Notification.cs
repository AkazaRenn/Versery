using Model.Database.Abstractions;

namespace Model.Database.PaginatedEntityStores;

internal sealed class Notification(string id)
    : PaginatedEntityStore<Entities.Notification>(id, StoreJsonContext.Default.DatabaseNotification);
