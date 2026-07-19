using LiteDB;

namespace Utilities.Abstract; 
public class CachedDb<TKey, TVal>(string collectionName) where TKey: notnull, BsonValue {
    private readonly ILiteCollection<TVal> db = Services.Get<LiteDatabase>().GetCollection<TVal>(collectionName);
    private readonly Dictionary<TKey, TVal> cache = [];

    public bool TryGet(TKey id, out TVal item) {
        if (cache.TryGetValue(id, out item)) {
            return true;
        }
        var dbItem = db.FindById(id);
        if (dbItem is not null) {
            cache[id] = dbItem;
            item = dbItem;
            return true;
        }
        item = default!;
        return false;
    }

    public IEnumerable<TVal> Get(IEnumerable<TKey> ids) {
        foreach (var id in ids) {
            if (TryGet(id, out var item)) {
                yield return item;
            }
        }
    }

    public void Add(TVal item, TKey id) {
        db.Insert(item);
        cache[id] = item;
    }

    
}
