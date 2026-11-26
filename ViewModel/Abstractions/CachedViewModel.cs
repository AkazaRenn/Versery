using CommunityToolkit.Mvvm.ComponentModel;
using Model.Utilities;
using ViewModel.Interfaces;

namespace ViewModel.Abstractions;

public abstract class CachedViewModel<TSelf>: ObservableObject
    where TSelf : CachedViewModel<TSelf>, ICachedViewModel<TSelf> {
    private static readonly Dictionary<string, WeakReference<TSelf>> cache = [];

    private readonly Sentinel<string, TSelf> sentinel;

    public string Id { get; }

    protected CachedViewModel(string id) {
        Id = id;
        sentinel = new(id, cache);
    }

    internal static TSelf Create(string id) => Get(id) ?? Add(TSelf.New(id));

    private static TSelf? Get(string id) {
        lock (cache) {
            return (cache.TryGetValue(id, out var reference) && reference.TryGetTarget(out var obj)) ? obj : null;
        }
    }

    private static TSelf Add(TSelf obj) {
        lock (cache) {
            if (cache.TryGetValue(obj.Id, out var reference) && reference.TryGetTarget(out var existing)) {
                return existing;
            }
            cache[obj.Id] = new(obj);
            return obj;
        }
    }
}
