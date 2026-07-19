using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Text;

namespace View.Controls; 
public sealed partial class AccountDisplayName: StackPanel {
    public FontWeight FontWeight { get; set; }
    public double FontSize { get; set; } = 14;
    public TextTrimming TextTrimming { get; set; }

    public Dictionary<string, string> Emojis { get; set; } = [];
    public string DisplayName { get; set; } = string.Empty;

    public AccountDisplayName() {
        InitializeComponent();
    }

    private void Test() {
        base.Children.Add(new TextBlock());
    }
}
