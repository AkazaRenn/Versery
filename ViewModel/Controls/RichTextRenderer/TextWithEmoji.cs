namespace ViewModel.Controls.RichTextRenderer;
public sealed class TextWithEmoji {
    public string RawText { get; set; } = string.Empty;
    public Dictionary<string, Uri> Emojis { get; set; } = [];
}
