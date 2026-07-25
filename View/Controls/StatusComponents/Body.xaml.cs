using Microsoft.UI.Xaml.Controls;

namespace View.Controls.StatusComponents;

internal sealed partial class Body: StackPanel {
    public string TextContent {
        get => TextBlock.Html;
        set => TextBlock.Html = value;
    }

    public Body() {
        InitializeComponent();
    }
}
