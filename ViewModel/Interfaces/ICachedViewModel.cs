using ViewModel.Abstractions;

namespace ViewModel.Interfaces;

public interface ICachedViewModel<TSelf>
    where TSelf : CachedViewModel<TSelf>, ICachedViewModel<TSelf> {
    public static abstract TSelf New(string id);
}
