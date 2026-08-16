using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.Foundation;

namespace View.Controls.Components;

public sealed partial class RefreshListView: RefreshContainer {
    private ScrollViewer? scrollViewer = null;

    public object ItemsSource { set => ListView.ItemsSource = value; }
    public DataTemplate ItemTemplate { set => ListView.ItemTemplate = value; }
    public event TypedEventHandler<ListViewBase, ContainerContentChangingEventArgs> ContainerContentChanging {
        add => ListView.ContainerContentChanging += value;
        remove => ListView.ContainerContentChanging -= value;
    }

    public RefreshListView() {
        InitializeComponent();
    }

    public bool IsOnTop => scrollViewer?.VerticalOffset == 0;

    public void ScrollToTop() {
        if ((ListView.ContainerFromIndex(0) is not null) &&
            (ListView.ContainerFromIndex(1) is not null)) {
            scrollViewer?.ChangeView(null, 0, null, false);
        } else {
            ScrollToTopItem();
        }
    }

    private void ScrollToTopItem() {
        if ((scrollViewer is not null) &&
            (ListView.ItemsSource is IEnumerable<object> items) &&
            items.Any()) {
            ListView.ScrollIntoView(items.First(), ScrollIntoViewAlignment.Leading);
        }
    }

    private void ListView_Loaded(object sender, RoutedEventArgs e) {
        scrollViewer = ListView.FindDescendant<ScrollViewer>();
    }
}
