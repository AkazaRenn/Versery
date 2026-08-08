using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using System.Collections.Concurrent;
using System.Text;

namespace ViewModel.Controls.RichTextRenderer;

public sealed class Html {
    private static readonly ConcurrentDictionary<string, ContentToken> cache = [];

    public string RawText {
        get;
        set {
            if (field != value) {
                cache.TryRemove(field, out _);
                field = value;
                UpdateTokens();
            }
        }
    } = string.Empty;
    public ContentToken ContentToken { get; private set; } = new([]);
    public Dictionary<string, Task<Uri?>> Emojis { get; set; } = [];
    public bool IsPlainText { get; init; } = false;

    private void UpdateTokens() {
        if (String.IsNullOrEmpty(RawText)) {
            if (ContentToken.Paragraphs.Count > 0) {
                ContentToken = new([]);
            }
            return;
        }

        ContentToken = cache.GetOrAdd(RawText, rawText => TokenizeHtml(rawText, IsPlainText));
    }

    private static ContentToken TokenizeHtml(string rawText, bool isPlainText) {
        if (isPlainText) {
            return new ContentToken([
                new ParagraphToken(TokenizeTextWithEmoji(rawText)),
            ]);
        }

        var parser = new HtmlParser();
        var document = parser.ParseDocument(rawText);

        if (document.Body == null || document.Body.ChildNodes.Length == 0) {
            return new ContentToken([
                new ParagraphToken(TokenizeTextWithEmoji(rawText)),
            ]);
        }

        var paragraphs = new List<ParagraphToken>();
        var currentParagraphInlines = new List<InlineToken>();

        foreach (var child in document.Body.ChildNodes) {
            switch (child) {
            case IText textNode:
                currentParagraphInlines.AddRange(TokenizeTextWithEmoji(textNode.Text));
                break;
            case IElement element when IsBlockElement(element):
                FlushCurrentParagraph();
                paragraphs.Add(TokenizeParagraph(element));
                break;
            default:
                currentParagraphInlines.AddRange(TokenizeInline(child));
                break;
            }
        }

        FlushCurrentParagraph();

        return new ContentToken(paragraphs);

        void FlushCurrentParagraph() {
            if (currentParagraphInlines.Count == 0) {
                return;
            }

            paragraphs.Add(new ParagraphToken(currentParagraphInlines.ToList()));
            currentParagraphInlines.Clear();
        }
    }

    private static bool IsBlockElement(IElement element) {
        return element.TagName.ToLower() switch {
            "address" or "article" or "aside" or "blockquote" or "dd" or "div" or "dl" or "dt" or "fieldset" or "figcaption" or "figure" or "footer" or "form" or "h1" or "h2" or "h3" or "h4" or "h5" or "h6" or "header" or "hr" or "li" or "main" or "nav" or "ol" or "p" or "pre" or "section" or "table" or "tbody" or "td" or "tfoot" or "th" or "thead" or "tr" or "ul" => true,
            _ => false,
        };
    }

    private static ParagraphToken TokenizeParagraph(IElement element) {
        return element.TagName.ToLower() switch {
            "br" => new ParagraphToken([
                new LineBreakToken(),
            ]),
            _ => new ParagraphToken(TokenizeInlineChildren(element)),
        };
    }

    private static List<InlineToken> TokenizeInlineChildren(IElement element) {
        var tokens = new List<InlineToken>(element.ChildNodes.Length);

        foreach (var child in element.ChildNodes) {
            tokens.AddRange(TokenizeInline(child));
        }

        return tokens;
    }

    private static IEnumerable<InlineToken> TokenizeInline(INode node) {
        switch (node) {
        case IText text:
            return TokenizeTextWithEmoji(text.Text);
        case IElement element:
            var cls = element.GetAttribute("class");
            if (cls is not null && cls.Contains("invisible")) {
                return [];
            }

            switch (element.TagName.ToLower()) {
            case "b":
            case "strong":
                return [new BoldToken(TokenizeInlineChildren(element))];
            case "i":
            case "em":
                return [new ItalicToken(TokenizeInlineChildren(element))];
            case "a":
                var href = element.GetAttribute("href");
                if (string.IsNullOrEmpty(href)) {
                    return TokenizeTextWithEmoji(element.TextContent);
                }

                return [new HyperlinkToken(href, TokenizeInlineChildren(element))];
            case "br":
                return [new LineBreakToken()];
            default:
                return TokenizeTextWithEmoji(element.TextContent);
            }
        default:
            return [];
        }
    }

    private static List<InlineToken> TokenizeTextWithEmoji(string rawText) {
        var tokens = new List<InlineToken>();
        var text = new StringBuilder();

        int index = 0;
        while (index < rawText.Length) {
            if ((rawText[index] != ':') ||
                (!TryReadEmojiShortCode(rawText, ref index, out var shortCode))) {
                text.Append(rawText[index]);
                index++;
                continue;
            }

            FlushText();
            tokens.Add(new EmojiToken(shortCode));
        }

        FlushText();
        return tokens;

        void FlushText() {
            if (text.Length == 0) {
                return;
            }

            tokens.Add(new TextToken(text.ToString()));
            text.Clear();
        }
    }

    private static bool TryReadEmojiShortCode(string rawText, ref int index, out string shortCode) {
        shortCode = string.Empty;

        int startIndex = index;
        int cursor = startIndex + 1;
        while (cursor < rawText.Length) {
            var ch = rawText[cursor];
            if (!(char.IsLetterOrDigit(ch) || ch is '_' or '-')) {
                break;
            }

            cursor++;
        }

        if (cursor == startIndex + 1) {
            return false;
        }

        var hasTrailingColon = cursor < rawText.Length && rawText[cursor] == ':';
        if (!hasTrailingColon) {
            return false;
        }

        shortCode = rawText[(startIndex + 1)..cursor];
        index = cursor + 1;
        return true;
    }
}
