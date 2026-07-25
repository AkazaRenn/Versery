using CommunityToolkit.Mvvm.ComponentModel;

namespace ViewModel.Controls.RichTextRenderer;
public sealed partial class Html: ObservableObject {
    [ObservableProperty]
    public partial string RawText { get; set; } = string.Empty;
    [ObservableProperty]
    public partial Dictionary<string, Uri> Emojis { get; set; } = [];
}
