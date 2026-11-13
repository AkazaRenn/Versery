using Microsoft.Extensions.Caching.Memory;

namespace Model.Database;

internal sealed class Account(string id): IDisposable {
    private readonly Store<Server.Entities.Account> db = new(id, "Accounts", Server.Entities.MastodonAPISerializerContext.Default.Account);

    public Server.Entities.Account? Get(string id) {
        if (db.TryGet(id, out var account)) {
            return account;
        }
        return null;
    }

    public void Add(Server.Entities.Account account) {
        db.Upsert(account.Id, account);
    }

    public void Add(IEnumerable<Server.Entities.Account> accounts) {
        foreach (var account in accounts) {
            Add(account);
        }
    }

    public void Dispose() {
        db.Dispose();
    }
}
