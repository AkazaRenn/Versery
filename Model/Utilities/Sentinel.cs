namespace Model.Utilities;

public sealed class Sentinel<TKey, TValue>(TKey key, IDictionary<TKey, WeakReference<TValue>> cache) where TValue : class {
    ~Sentinel() {
        lock (cache) {
            if (cache.TryGetValue(key, out var reference) &&
                !reference.TryGetTarget(out var _)) {
                cache.Remove(key);
            }
        }
    }
}
