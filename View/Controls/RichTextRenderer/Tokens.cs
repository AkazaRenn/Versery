namespace View.Controls.RichTextRenderer;

internal abstract record InlineToken;
internal sealed record TextToken(string Text): InlineToken;
internal sealed record EmojiToken(string Shortcode, string Placeholder): InlineToken;
internal sealed record LineBreakToken: InlineToken;
internal sealed record BoldToken(IReadOnlyList<InlineToken> Children): InlineToken;
internal sealed record ItalicToken(IReadOnlyList<InlineToken> Children): InlineToken;
internal sealed record HyperlinkToken(string Href, IReadOnlyList<InlineToken> Children): InlineToken;

internal sealed record ParagraphToken(IReadOnlyList<InlineToken> Inlines);
internal sealed record ContentToken(IReadOnlyList<ParagraphToken> Paragraphs);