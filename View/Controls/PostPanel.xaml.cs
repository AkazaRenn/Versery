using Microsoft.UI.Xaml.Controls;

namespace View.Controls;

internal sealed partial class PostPanel: Grid {
    public ViewModel.Controls.PostPanel? ViewModel { get; set; } = null;
    public event Action? Dismissed;

    public PostPanel() {
        InitializeComponent();
    }

    private void DismissButton_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e) {
        Dismissed?.Invoke();
    }
}