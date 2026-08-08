using LiteDB;
using System.Runtime.Caching;

namespace Model.Database;

internal class Account(string hash) {
    private static readonly MemoryCache cache = new(typeof(Account).FullName);
    private readonly ILiteCollection<Entities.Account> db = Services.Get<LiteDatabase>().GetCollection<Entities.Account>($"instance_{hash}_accounts");

    public Entities.Account? Get(string id) {
        if (cache[id] is Entities.Account account) {
            return account;
        }
        account = db.FindById(id);
        if (account != null) {
            cache[id] = account;
        }
        return account;
    }

    public void Add(Entities.Account account) {
        db.Upsert(account);
        cache[account.Id] = account;
    }

    public void Add(IEnumerable<Entities.Account> accounts) {
        accounts = accounts.ToCollection();

        db.Upsert(accounts);
        foreach (var account in accounts) {
            cache[account.Id] = account;
        }
    }
}
