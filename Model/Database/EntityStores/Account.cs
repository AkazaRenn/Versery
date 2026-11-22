using Model.Database.Abstractions;

namespace Model.Database.EntityStores;

internal sealed class Account(string id)
    : EntityStore<Entities.Account>(id, 2_000, StoreJsonContext.Default.DatabaseAccount);
