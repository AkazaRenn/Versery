namespace ViewModel.Controls.RichTextRenderer;

public sealed class Html {
    public string RawText { get; set; } = string.Empty;
    public Dictionary<string, Uri> Emojis { get; set; } = [];
}
