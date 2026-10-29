using CommunityToolkit.WinUI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace View.Controls.Components;

internal sealed partial class RefreshListView: RefreshContainer {
    public object ItemsSource { set => ListView.ItemsSource = value; }
    public DataTemplate ItemTemplate { set => ListView.ItemTemplate = value; }
    public Func<Task>? LoadLatest { get; set; } = null;
    public event Action<int>? OnItemRealized;

    private ScrollViewer? scrollViewer = null;
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

    private async void RefreshListView_RefreshRequested(RefreshContainer sender, RefreshRequestedEventArgs args) {
        if (LoadLatest is null) {
            return;
        }

        var deferral = args.GetDeferral();
        await LoadLatest();
        ScrollToTop();
        deferral.Complete();
    }

    private void ListView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args) {
        if (args.InRecycleQueue) {
            return;
        }

        if (args.Phase == 0 && args.Item is ViewModel.Controls.Timeline status) {
            status.Index = args.ItemIndex;
            OnItemRealized?.Invoke(args.ItemIndex);
        }
    }

    public RefreshListView() {
        InitializeComponent();
    }
}
