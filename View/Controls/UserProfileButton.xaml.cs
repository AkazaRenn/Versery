using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace View.Controls; 
public sealed partial class UserProfileButton: Button {
    public UserProfileButton() {
        InitializeComponent();
    }

    public Frame? Frame { get; set; }

    private void MenuFlyoutItem_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e) {
        _ = new LoginDialog(XamlRoot);
    }
}
