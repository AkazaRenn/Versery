using LiteDB;
using Utilities;

namespace Model.Database; 
internal class Account(string hash) {
    private readonly ILiteCollection<Entities.Account> db = Utilities.Services.Get<LiteDatabase>().GetCollection<Entities.Account>($"instance_{hash}_accounts");
    private readonly Dictionary<string, Entities.Account> cache = [];

    public Entities.Account? Get(string id) {
        if (cache.TryGetValue(id, out var account)) {
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
