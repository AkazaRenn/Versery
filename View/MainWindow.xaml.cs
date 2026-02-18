using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Navigation;
using System.Security.AccessControl;
using View.Pages;
using Windows.UI;
using Windows.UI.WindowManagement;
using WinUIEx;

namespace View;

public sealed partial class MainWindow: WindowEx {
    public MainWindow() {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        AppWindow.TitleBar.PreferredHeightOption = Microsoft.UI.Windowing.TitleBarHeightOption.Tall;
        SetTitleBar(TitleBar);
        TitleBar.Margin = new Thickness(MainNavigation.CompactPaneLength, 0, 0, 0);
        TitleBar.Height = MainNavigation.CompactPaneLength;

        MinWidth = 400;
        MinHeight = 400;

        navigationViewItem_SamplePage1.Tag = typeof(View.Pages.SamplePage1);
        navigationViewItem_Login.Tag = typeof(View.Pages.Login);
    }

    private void MainNavigation_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args) {
        if (args.SelectedItem is NavigationViewItem item) {
            if (item.Tag is Type pageType) {
                ContentFrame.Navigate(pageType);
            }
        }
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
