using Microsoft.UI.Xaml;

namespace View.Interfaces;
public interface IWindowHelper {
    public bool TryGetWindow(UIElement element, out Window? window);
}
