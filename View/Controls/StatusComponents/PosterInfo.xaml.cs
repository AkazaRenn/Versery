using Microsoft.UI.Xaml.Controls;

namespace View.Controls.StatusComponents; 
public sealed partial class PosterInfo: Grid {
    public string DisplayName {
        get => DisplayNameTextBlock.Text;
        set => DisplayNameTextBlock.Text = value;
    }

    public string Id {
        get => IdTextBlock.Text;
        set => IdTextBlock.Text = value;
    }

    public PosterInfo() {
        InitializeComponent();
    }
}
