namespace Model.Database;

internal sealed class Account(string id): IDisposable {
    private readonly Store<Entities.Account> db = new(id, "Accounts", StoreJsonContext.Default.DatabaseAccount);

    public Entities.Account? Get(string id) {
        if (db.TryGet(id, out var account)) {
            return account;
        }
        return null;
    }

    public void Add(Entities.Account account) {
        db.Upsert(account.Id, account);
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
