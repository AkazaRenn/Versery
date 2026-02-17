using Microsoft.UI.Xaml;
using WinUIEx;

namespace View;

public sealed partial class MainWindow: WindowEx {
    public MainWindow() {
        InitializeComponent();

        ExtendsContentIntoTitleBar = true;
        AppWindow.TitleBar.PreferredHeightOption = Microsoft.UI.Windowing.TitleBarHeightOption.Tall;
        SetTitleBar(titleBar);
        titleBar.Margin = new Thickness(mainNavigation.CompactPaneLength, 0, 0, 0);
        titleBar.Height = mainNavigation.CompactPaneLength;

        MinWidth = 400;
        MinHeight = 400;
    }
}
