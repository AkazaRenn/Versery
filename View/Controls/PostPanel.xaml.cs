using View.Controls.Components;

namespace View.Controls;

internal sealed partial class PostPanel: SlideDialog {
    public ViewModel.Controls.PostPanel? ViewModel { get; set; } = null;

    public PostPanel() {
        InitializeComponent();
    }

    private void DismissButton_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e) {
        HideGracefully();
    }

    private void SlideDialog_Entered() {
        PanelRoot.IsHitTestVisible = true;
    }
}