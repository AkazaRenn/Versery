using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.UI.Composition;
using Microsoft.UI.Composition;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Graphics;
using Windows.UI.WindowManagement;
using WinUIEx;
using View.Interfaces;

namespace View;
public sealed partial class MainWindow: WindowEx, ICompositionGraphicsDeviceProvider {
    private NavigationViewItemBase? activeNavigationItem = null;
    private readonly ViewModel.MainWindow viewModel = new();

    public CompositionGraphicsDevice CompositionGraphicsDevice { get; }

    public MainWindow() {
        InitializeComponent();
        InitializeShare();

        CompositionGraphicsDevice = CanvasComposition.CreateCompositionGraphicsDevice(Compositor, CanvasDevice.GetSharedDevice());

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
        if (activeNavigationItem == args.InvokedItemContainer) {
            if (Frame.Content is INavigationPage navigationPage) {
                navigationPage.OnNavigationReInvoke();
            }
        } else {
            if (args.InvokedItemContainer is NavigationViewItem item) {
                if (item.Tag is Type pageType) {
                    Frame.Navigate(pageType);
                }
            }

            UpdateNonClientInputPassthrough();
        }
    }

    private void Navigation_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) {
        if (Frame.CanGoBack) {
            Frame.GoBack();
        }
    }

    private void Navigation_Loaded(object sender, RoutedEventArgs e) {
        var ScaleUnawareRightInset = AppWindow.TitleBar.RightInset / Content.XamlRoot.RasterizationScale;
        UserProfileButton.Margin = new Thickness(0, 0, ScaleUnawareRightInset + 16, 0);
        Navigation_RightPadding.Width = UserProfileButton.Margin.Right;

        foreach (var item in Navigation.MenuItems.OfType<NavigationViewItem>()) {
            item.Height = Navigation.CompactPaneLength - 8;
        }

        Navigation.SizeChanged += (_, _) => {
            UpdateNonClientInputPassthrough();
        };

        Frame.Navigate(typeof(Pages.Home));
    }

    private void Frame_Navigated(object sender, NavigationEventArgs e) {
        Navigation.IsBackEnabled = Frame.CanGoBack;

        if (Navigation.SelectedItem is NavigationViewItem navigationViewItem &&
            navigationViewItem.Tag as Type == e.SourcePageType) {
        } else {
            Navigation.SelectedItem = Navigation.MenuItems.OfType<NavigationViewItem>().FirstOrDefault((NavigationViewItem item) => item.Tag as Type == e.SourcePageType);
        }
        activeNavigationItem = Navigation.SelectedItem as NavigationViewItem;
    }

    private void ViewModel_NavigationRequested(ViewModel.Enumerations.Page page, string? obj) {
        switch (page) {
            case ViewModel.Enumerations.Page.Home:
                Frame.Navigate(typeof(Pages.Home));
                break;
            case ViewModel.Enumerations.Page.SignIn:
                Frame.Navigate(typeof(Pages.SignIn));
                break;
        }
    }
}
