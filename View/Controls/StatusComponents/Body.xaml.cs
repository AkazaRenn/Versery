using Microsoft.UI.Xaml.Controls;

namespace View.Controls.StatusComponents; 
public sealed partial class Body: StackPanel {
    public string TextContent {
        get => TextBlock.Html;
        set => TextBlock.Html = value;
    }

    public object AdditionalContent {
        get => AdditionalContentPresenter.Content;
        set => AdditionalContentPresenter.Content = value;
    }

    public Body() {
        InitializeComponent();
    }
}
