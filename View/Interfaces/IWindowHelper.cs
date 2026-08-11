using Microsoft.UI.Xaml;

namespace View.Interfaces;

public interface IWindowHelper {
    public T CreateWindow<T>() where T : Window, new();
    public bool TryGetWindow(UIElement element, out Window? window);
}
