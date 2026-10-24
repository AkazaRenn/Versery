using Microsoft.Extensions.Caching.Memory;

namespace Model.Database;

internal sealed class Account(string id): IDisposable {
    private readonly MemoryCache cache = new(new MemoryCacheOptions { SizeLimit = 512 });
    private readonly MemoryCacheEntryOptions entryOptions = new() { Size = 1 };
    private readonly Store<Entities.Account> db = new(id, "Accounts", StoreJsonContext.Default.Account);

    public Entities.Account? Get(string id) {
        if (cache.TryGetValue(id, out Entities.Account? cached)) {
            return cached;
        }
        if (db.TryGet(id, out var account)) {
            cache.Set(id, account, entryOptions);
        }
        return account;
    }

    public void Add(Entities.Account account) {
        db.Upsert(account.Id, account);
        cache.Set(account.Id, account, entryOptions);
    }

    public void Add(IEnumerable<Entities.Account> accounts) {
        foreach (var account in accounts) {
            Add(account);
        }
    }

    public void Dispose() {
        db.Dispose();
    }
}
