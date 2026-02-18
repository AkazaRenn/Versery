using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Graphics;
using Windows.UI.WindowManagement;
using WinUIEx;

namespace View;

public sealed partial class MainWindow: WindowEx {
    public MainWindow() {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        AppWindow.TitleBar.PreferredHeightOption = Microsoft.UI.Windowing.TitleBarHeightOption.Tall;
        SetTitleBar(MainNavigation);

        MinWidth = 400;
        MinHeight = 400;

        navigationViewItem_SamplePage1.Tag = typeof(View.Pages.SamplePage1);
        navigationViewItem_Login.Tag = typeof(View.Pages.Login);

        foreach (var item in MainNavigation.MenuItems.OfType<NavigationViewItem>()) {
            item.Height = MainNavigation.CompactPaneLength - 8;
        }

        if (MainNavigation.MenuItems.OfType<NavigationViewItem>().First().Tag is Type pageType) {
            ContentFrame.Navigate(pageType);
        }
        MainNavigation.SizeChanged += (_, _) => {
            UpdateNonClientInputPassthrough();
        };
    }

    private GridLength TitleBarRightInset {
        get {
            return new GridLength(AppWindow.TitleBar.RightInset - 80);
        }
    }

    private double MainNavigation_RightPadding {
        get {
            return AppWindow.TitleBar.RightInset - 120 + ProfileButton.Width;
        }
    }

    private void UpdateNonClientInputPassthrough() {
        if (Content.XamlRoot == null) {
            return;
        }

        List<RectInt32> regionRects = [
            GetRegionRect(8, 8, 36, 36), // back button
            //GetRegionRect(ProfileButton),
        ];

        foreach (var item in MainNavigation.MenuItems.OfType<NavigationViewItem>()) {
            if (item.ActualWidth <= 0) {
                continue;
            }
            regionRects.Add(GetRegionRect(item, 4));
        }

        var nonClientInput = InputNonClientPointerSource.GetForWindowId(AppWindow.Id);
        nonClientInput.ClearRegionRects(NonClientRegionKind.Passthrough);
        nonClientInput.SetRegionRects(NonClientRegionKind.Passthrough, [.. regionRects]);
    }

    private RectInt32 GetRegionRect(FrameworkElement element, double offset = 0) {
        var transform = element.TransformToVisual(null);
        Rect bounds = transform.TransformBounds(new Rect(0, 0, element.ActualWidth, element.ActualHeight));
        return GetRegionRect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
    }

    private RectInt32 GetRegionRect(double X, double Y, double Width, double Height, double offset = 0) {
        double dpiScale = Content.XamlRoot.RasterizationScale;
        return new RectInt32(
                (int)(dpiScale * (X + offset)),
                (int)(dpiScale * (Y + offset)),
                (int)(dpiScale * (Width - offset)),
                (int)(dpiScale * (Height - offset)));
    }

    private void MainNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args) {
        if (args.SelectedItem is NavigationViewItem item) {
            if (item.Tag is Type pageType) {
                ContentFrame.Navigate(pageType);
            }
        }

        UpdateNonClientInputPassthrough();
    }

    private void MainNavigation_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) {
        if (ContentFrame.CanGoBack) {
            ContentFrame.GoBack();
        }
    }

    private void ContentFrame_Navigated(object sender, NavigationEventArgs e) {
        MainNavigation.IsBackEnabled = ContentFrame.CanGoBack;

        foreach (var item in MainNavigation.MenuItems.OfType<NavigationViewItem>()) {
            if (item.Tag is Type pageType) {
                if (pageType == e.SourcePageType) {
                    MainNavigation.SelectedItem = item;
                    return;
                }
            }
        }

        MainNavigation.SelectedItem = null;
    }
}
