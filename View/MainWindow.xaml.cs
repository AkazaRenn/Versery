using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Composition;
using Microsoft.UI.Composition;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using View.Controls;
using View.Interfaces;
using Windows.Foundation;
using Windows.Graphics;
using Windows.UI.WindowManagement;
using WinUIEx;

namespace View;

public sealed partial class MainWindow: WindowEx, IGraphicsDeviceProvider {
    private readonly Dictionary<Type, NavigationViewItem> navigationItems = [];
    private readonly ViewModel.MainWindow viewModel = new();

    public CanvasDevice CanvasDevice { get; }
    public CompositionGraphicsDevice CompositionGraphicsDevice { get; }

    public MainWindow() {
        InitializeComponent();

        CanvasDevice = CanvasDevice.GetSharedDevice();
        CompositionGraphicsDevice = CanvasComposition.CreateCompositionGraphicsDevice(Compositor, CanvasDevice);

        ExtendsContentIntoTitleBar = true;
        AppWindow.TitleBar.PreferredHeightOption = TitleBarHeightOption.Tall;
        SetTitleBar(Navigation);

        // TODO: replace by the official PersistedStateId (2.0+)
        // https://github.com/microsoft/WindowsAppSDK/blob/afd4ac42a32a329f4fdfc76d7d443a0200774135/specs/Windowing/AppWindowPlacement.md
        //AppWindow.PersistedStateId = "MainWindow";
        //AppWindow.PlacementRestorationBehavior = PlacementRestorationBehavior.Automatic;
        PersistenceId = "MainWindow";

        viewModel.NavigationRequested += ViewModel_NavigationRequested;
    }

    private void Navigate(Type type) {
        if (Frame.Content?.GetType() == type) {
            return;
        }

        // Comment out for now, the overflow menu movement does not update the index making it hard to use
        //var currentPageIndex = Navigation.MenuItems.OfType<NavigationViewItem>().ToList().FindIndex(item => item.Tag as Type == Frame.Content?.GetType());
        //var targetPageIndex = Navigation.MenuItems.OfType<NavigationViewItem>().ToList().FindIndex(item => item.Tag as Type == type);

        NavigationTransitionInfo? navigationTransitionInfo = null;
        //if ((currentPageIndex >= 0) && (targetPageIndex >= 0)) {
        //    if (currentPageIndex < targetPageIndex) {
        //        navigationTransitionInfo = new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromRight };
        //    } else if (currentPageIndex > targetPageIndex) {
        //        navigationTransitionInfo = new SlideNavigationTransitionInfo() { Effect = SlideNavigationTransitionEffect.FromLeft };
        //    }
        //}

        Frame.Navigate(type, null, navigationTransitionInfo);
    }

    private void UpdateNonClientInputPassthrough() {
        if (Content.XamlRoot == null) {
            return;
        }

        List<RectInt32> regionRects = [
            GetRegionRect(8, 8, 36, 36), // back button
            //GetRegionRect(UserProfileButton),
        ];

        foreach (var item in Navigation.MenuItems.OfType<NavigationViewItem>()) {
            if (item.ActualWidth <= 0) {
                continue;
            }
            regionRects.Add(GetRegionRect(item, 8));
        }

        var nonClientInput = InputNonClientPointerSource.GetForWindowId(AppWindow.Id);
        nonClientInput.ClearRegionRects(NonClientRegionKind.Passthrough);
        nonClientInput.SetRegionRects(NonClientRegionKind.Passthrough, [.. regionRects]);
    }

    private RectInt32 GetRegionRect(FrameworkElement element, double offset = 0) {
        var transform = element.TransformToVisual(null);
        Rect bounds = transform.TransformBounds(new Rect(0, 0, element.ActualWidth, element.ActualHeight));
        return GetRegionRect(bounds.X, bounds.Y, bounds.Width, bounds.Height, offset);
    }

    private RectInt32 GetRegionRect(double X, double Y, double Width, double Height, double offset = 0) {
        double dpiScale = Content.XamlRoot.RasterizationScale;
        return new RectInt32(
            (int)(dpiScale * (X + offset)),
            (int)(dpiScale * (Y + offset)),
            (int)(dpiScale * (Width - offset)),
            (int)(dpiScale * (Height - offset)));
    }

    private void Navigation_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args) {
        if (args.InvokedItemContainer.Tag is not Type pageType) {
            return;
        }

        if (pageType == Frame.Content.GetType()) {
            if (Frame.Content is INavigationPage navigationPage) {
                navigationPage.OnNavigationReInvoke();
            }
        } else {
            Navigate(pageType);
        }
    }

    private void Navigation_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) {
        if (Frame.CanGoBack) {
            Frame.GoBack();
        }
    }

    private void Navigation_Loaded(object sender, RoutedEventArgs e) {
        var ScaleUnawareRightInset = AppWindow.TitleBar.RightInset / Content.XamlRoot.RasterizationScale;
        UserProfileButton.Margin = new(0, 0, ScaleUnawareRightInset + 16, 0);
        Navigation_RightPadding.Width = ScaleUnawareRightInset + 16 + UserProfileButton.Width;

        foreach (var item in Navigation.MenuItems.OfType<NavigationViewItem>()) {
            item.Height = Navigation.CompactPaneLength - 8;
        }

        foreach (var item in Navigation.MenuItems.OfType<NavigationViewItem>()) {
            if (item.Tag is Type pageType) {
                navigationItems[pageType] = item;
            }
        }

        Navigate(typeof(Pages.Home));
    }

    private void Frame_Navigated(object sender, NavigationEventArgs e) {
        Navigation.IsBackEnabled = Frame.CanGoBack;

        if (((Navigation.SelectedItem is not NavigationViewItem navigationViewItem) ||
            (navigationViewItem.Tag as Type != e.SourcePageType)) &&
            navigationItems.TryGetValue(e.SourcePageType, out var menuItem)) {
            Navigation.SelectedItem = menuItem;
        }
        UpdateNonClientInputPassthrough();
    }

    private void ViewModel_NavigationRequested(ViewModel.Enumerations.Page page, string? obj) {
        switch (page) {
        case ViewModel.Enumerations.Page.Home:
            Navigate(typeof(Pages.Home));
            break;
        case ViewModel.Enumerations.Page.Notifications:
            Navigate(typeof(Pages.Notifications));
            break;
        case ViewModel.Enumerations.Page.SignIn:
            Navigate(typeof(Pages.SignIn));
            break;
        }
    }

    private void Navigation_SizeChanged(object sender, SizeChangedEventArgs e) {
        UpdateNonClientInputPassthrough();
    }

    private void PostButton_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e) {
        _ = new PostPanel() {
            XamlRoot = Content.XamlRoot,
        }.ShowAsync();
    }
}
