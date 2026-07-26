using AngleSharp.Dom;
using AngleSharp.Html.Parser;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using System.Runtime.Caching;

namespace View.Controls.RichTextRenderer;

internal static class Html {
    private static readonly MemoryCache htmlCache = new(nameof(Html));
    private static readonly CacheItemPolicy htmlCachePolicy = new() {
        SlidingExpiration = TimeSpan.FromMinutes(30),
    };

    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.RegisterAttached(
        "ViewModel",
        typeof(ViewModel.Controls.RichTextRenderer.Html),
        typeof(Html),
        new PropertyMetadata(null, OnViewModelChangedAsync)
    );

    public static ViewModel.Controls.RichTextRenderer.Html? GetViewModel(DependencyObject obj) {
        return (ViewModel.Controls.RichTextRenderer.Html?)obj.GetValue(ViewModelProperty);
    }

    public static void SetViewModel(DependencyObject obj, ViewModel.Controls.RichTextRenderer.Html? value) {
        obj.SetValue(ViewModelProperty, value);
    }

    private static async void OnViewModelChangedAsync(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if (d is not RichTextBlock richTextBlock) {
            return;
        }

        if (e.OldValue == e.NewValue) {
            return;
        }

        if ((e.NewValue is ViewModel.Controls.RichTextRenderer.Html newVm) && (!String.IsNullOrEmpty(newVm.RawText))) {
            _ = UpdateContent(richTextBlock, newVm.RawText, newVm.Emojis);
        } else {
            richTextBlock.Blocks.Clear();
        }
    }

    private static async Task UpdateContent(RichTextBlock richTextBlock, string rawText, Dictionary<string, Uri> emojis) {
        if (htmlCache.Get(rawText) is not ContentToken tokenizedContent) {
            tokenizedContent = await Task.Run(() => TokenizeHtmlAsync(rawText));
            htmlCache.Add(rawText, tokenizedContent, htmlCachePolicy);
        }

        RenderContent(richTextBlock, tokenizedContent, emojis);
    }

    private static void RenderContent(RichTextBlock richTextBlock, ContentToken content, Dictionary<string, Uri> emojis) {
        var blocks = richTextBlock.Blocks;
        blocks.Clear();

        foreach (var paragraphToken in content.Paragraphs) {
            blocks.Add(RenderParagraph(paragraphToken, emojis, richTextBlock.FontSize));
        }

        for (int i = 0; i < blocks.Count - 1; i++) {
            var block = blocks[i];
            block.Margin = new Thickness(0, 0, 0, block.FontSize);
        }
    }

    private static ContentToken TokenizeHtmlAsync(string rawText) {
        var parser = new HtmlParser();
        var document = parser.ParseDocument(rawText);

        if (document.Body == null || document.Body.ChildElementCount == 0) {
            return new ContentToken([
                new ParagraphToken([
                    new TextToken(rawText),
                ]),
            ]);
        }

        var paragraphs = new List<ParagraphToken>(document.Body.ChildElementCount);
        foreach (var child in document.Body.Children) {
            paragraphs.Add(TokenizeParagraph(child));
        }

        return new ContentToken(paragraphs);
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
            var token = TokenizeInline(child);
            if (token is not null) {
                tokens.Add(token);
            }
        }

        return tokens;
    }

    private static InlineToken? TokenizeInline(INode node) {
        switch (node) {
        case IText text:
            return new TextToken(text.Text);
        case IElement element:
            var cls = element.GetAttribute("class");
            if (cls is not null && cls.Contains("invisible")) {
                return null;
            }

            switch (element.TagName.ToLower()) {
            case "b":
            case "strong":
                return new BoldToken(TokenizeInlineChildren(element));
            case "i":
            case "em":
                return new ItalicToken(TokenizeInlineChildren(element));
            case "a":
                var href = element.GetAttribute("href");
                if (string.IsNullOrEmpty(href)) {
                    return new TextToken(element.TextContent);
                }

                return new HyperlinkToken(href, TokenizeInlineChildren(element));
            case "br":
                return new LineBreakToken();
            default:
                return new TextToken(element.TextContent);
            }
        default:
            return null;
        }
    }

    private static Paragraph RenderParagraph(ParagraphToken paragraphToken, Dictionary<string, Uri> emojis, double fontSize) {
        var paragraph = new Paragraph();

        foreach (var inlineToken in paragraphToken.Inlines) {
            var inline = RenderInline(inlineToken, emojis, fontSize);
            if (inline != null) {
                paragraph.Inlines.Add(inline);
            }
        }

        return paragraph;
    }

    private static Inline RenderInline(InlineToken token, Dictionary<string, Uri> emojis, double fontSize) {
        return token switch {
            TextToken text => RenderTokenizedText(text.Text, emojis, fontSize),
            LineBreakToken => new LineBreak(),
            BoldToken bold => RenderInlineChildren(new Bold(), bold.Children, emojis, fontSize),
            ItalicToken italic => RenderInlineChildren(new Italic(), italic.Children, emojis, fontSize),
            // Todo: replace by HyperlinkButton for tags
            // https://github.com/microsoft/microsoft-ui-xaml/discussions/11251
            HyperlinkToken hyperlink => RenderInlineChildren(new Hyperlink {
                NavigateUri = new Uri(hyperlink.Href),
            }, hyperlink.Children, emojis, fontSize),
            _ => new Run(),
        };
    }

    private static Span RenderTokenizedText(string rawText, Dictionary<string, Uri> emojis, double fontSize) {
        var tokenizedContent = TextWithEmoji.TokenizeTextWithEmoji(rawText);
        var span = new Span();

        foreach (var paragraph in tokenizedContent.Paragraphs) {
            foreach (var token in paragraph.Inlines) {
                span.Inlines.Add(TextWithEmoji.RenderInline(token, emojis, fontSize));
            }
        }

        return span;
    }

    private static Span RenderInlineChildren(Span span, IReadOnlyList<InlineToken> tokens, Dictionary<string, Uri> emojis, double fontSize) {
        foreach (var token in tokens) {
            span.Inlines.Add(RenderInline(token, emojis, fontSize));
        }

        return span;
    }

}