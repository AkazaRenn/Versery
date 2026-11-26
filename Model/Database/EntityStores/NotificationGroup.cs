using Model.Database.Abstractions;

namespace Model.Database.EntityStores;

internal sealed class NotificationGroup(string id)
    : EntityStore<Entities.NotificationGroup>(id, 2_000, StoreJsonContext.Default.DatabaseNotificationGroup);
