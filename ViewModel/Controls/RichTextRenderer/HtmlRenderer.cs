using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.Controls.RichTextRenderer;
public sealed partial class HtmlRenderer: ObservableObject {
    [ObservableProperty]
    public partial string Html { get; set; } = string.Empty;
    [ObservableProperty]
    public partial Dictionary<string, Uri> Emojis { get; set; } = [];
}
