using CommunityToolkit.Mvvm.Messaging;
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

public sealed partial class MainWindow: WindowEx, IRecipient<ViewModel.Messages.LoginRequested> ,IRecipient<ViewModel.Messages.LoginCompleted> {
    public MainWindow() {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        AppWindow.TitleBar.PreferredHeightOption = Microsoft.UI.Windowing.TitleBarHeightOption.Tall;
        SetTitleBar(Navigation);

        // TODO: replace by the official PersistedStateId (2.0+)
        // https://github.com/microsoft/WindowsAppSDK/blob/afd4ac42a32a329f4fdfc76d7d443a0200774135/specs/Windowing/AppWindowPlacement.md
        //AppWindow.PersistedStateId = "MainWindow";
        //AppWindow.PlacementRestorationBehavior = PlacementRestorationBehavior.Automatic;
        PersistenceId = "MainWindow";

        WeakReferenceMessenger.Default.RegisterAll(this);
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

    private void Navigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args) {
        if (args.SelectedItem is NavigationViewItem item) {
            if (item.Tag is Type pageType) {
                Frame.Navigate(pageType);
            }
        }

        UpdateNonClientInputPassthrough();
    }

    private void Navigation_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args) {
        if (Frame.CanGoBack) {
            Frame.GoBack();
        }
    }

    private void Navigation_Loaded(object sender, RoutedEventArgs e) {
        navigationViewItem_SamplePage1.Tag = typeof(View.Pages.SamplePage1);

        var ScaleUnawareRightInset = AppWindow.TitleBar.RightInset / Content.XamlRoot.RasterizationScale;
        UserProfileButton.Margin = new Thickness(0, 0, ScaleUnawareRightInset + 16, 0);
        Navigation_RightPadding.Width = UserProfileButton.Margin.Right;

        foreach (var item in Navigation.MenuItems.OfType<NavigationViewItem>()) {
            item.Height = Navigation.CompactPaneLength - 8;
        }

        if (Navigation.MenuItems.OfType<NavigationViewItem>().First().Tag is Type pageType) {
            Frame.Navigate(pageType);
        }

        Navigation.SizeChanged += (_, _) => {
            UpdateNonClientInputPassthrough();
        };
    }

    private void Frame_Navigated(object sender, NavigationEventArgs e) {
        Navigation.IsBackEnabled = Frame.CanGoBack;

        foreach (var item in Navigation.MenuItems.OfType<NavigationViewItem>()) {
            if (item.Tag is Type pageType) {
                if (pageType == e.SourcePageType) {
                    Navigation.SelectedItem = item;
                    return;
                }
            }
        }

        Navigation.SelectedItem = null;
    }

    void IRecipient<ViewModel.Messages.LoginRequested>.Receive(ViewModel.Messages.LoginRequested _) {
        Frame.Navigate(typeof(Pages.Login));
    }

    void IRecipient<ViewModel.Messages.LoginCompleted>.Receive(ViewModel.Messages.LoginCompleted _) {
        Frame.Navigate(typeof(Pages.SamplePage1));
    }
}
