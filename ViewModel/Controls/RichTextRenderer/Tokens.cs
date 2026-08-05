namespace ViewModel.Controls.RichTextRenderer;

public abstract record InlineToken;
public sealed record TextToken(string Text): InlineToken;
public sealed record EmojiToken(string ShortCode, string Placeholder): InlineToken;
public sealed record LineBreakToken: InlineToken;
public sealed record BoldToken(IReadOnlyCollection<InlineToken> Children): InlineToken;
public sealed record ItalicToken(IReadOnlyCollection<InlineToken> Children): InlineToken;
public sealed record HyperlinkToken(string Href, IReadOnlyCollection<InlineToken> Children): InlineToken;

public sealed record ParagraphToken(IReadOnlyCollection<InlineToken> Inlines);
public sealed record ContentToken(IReadOnlyCollection<ParagraphToken> Paragraphs);