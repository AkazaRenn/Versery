using AngleSharp;
using AngleSharp.Dom;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using System.Runtime.Caching;

namespace View.Controls.RichTextRenderer;

internal static class HtmlRenderer {
    private static readonly MemoryCache htmlCache = new(nameof(HtmlRenderer));
    private static readonly CacheItemPolicy htmlCachePolicy = new() {
        SlidingExpiration = TimeSpan.FromMinutes(30),
    };

    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.RegisterAttached(
        "ViewModel",
        typeof(ViewModel.Controls.RichTextRenderer.HtmlRenderer),
        typeof(HtmlRenderer),
        new PropertyMetadata(null, OnViewModelChanged)
    );

    public static ViewModel.Controls.RichTextRenderer.HtmlRenderer? GetViewModel(DependencyObject obj) {
        return (ViewModel.Controls.RichTextRenderer.HtmlRenderer?)obj.GetValue(ViewModelProperty);
    }

    public static void SetViewModel(DependencyObject obj, ViewModel.Controls.RichTextRenderer.HtmlRenderer? value) {
        obj.SetValue(ViewModelProperty, value);
    }

    private static void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if (d is not RichTextBlock richTextBlock) {
            return;
        }

        if (e.NewValue is ViewModel.Controls.RichTextRenderer.HtmlRenderer newVm) {
            _ = UpdateContent(richTextBlock, newVm.Html, newVm.Emojis);
            return;
        }

        richTextBlock.Blocks.Clear();
    }

    private static async Task UpdateContent(RichTextBlock richTextBlock, string html, Dictionary<string, Uri> emojis) {
        if (htmlCache.Get(html) is not HtmlContentToken tokenizedContent) {
            tokenizedContent = await TokenizeHtmlAsync(html);
            htmlCache.Add(html, tokenizedContent, htmlCachePolicy);
        }

        RenderContent(richTextBlock, tokenizedContent, emojis);
    }

    private static void RenderContent(RichTextBlock richTextBlock, HtmlContentToken content, Dictionary<string, Uri> emojis) {
        var blocks = richTextBlock.Blocks;
        blocks.Clear();

        foreach (var paragraphToken in content.Paragraphs) {
            blocks.Add(RenderParagraph(paragraphToken));
        }

        for (int i = 0; i < blocks.Count - 1; i++) {
            var block = blocks[i];
            block.Margin = new Thickness(0, 0, 0, block.FontSize);
        }
    }

    private static async Task<HtmlContentToken> TokenizeHtmlAsync(string html) {
        var browsingContext = new BrowsingContext();
        var document = await browsingContext.OpenAsync(req => req.Content(html));

        if (document.Body == null || document.Body.ChildElementCount == 0) {
            return new HtmlContentToken([
                new ParagraphToken([
                    new TextToken(html),
                ]),
            ]);
        }

        var paragraphs = new List<ParagraphToken>(document.Body.ChildElementCount);
        foreach (var child in document.Body.Children) {
            paragraphs.Add(TokenizeParagraph(child));
        }

        return new HtmlContentToken(paragraphs);
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

    private static Paragraph RenderParagraph(ParagraphToken paragraphToken) {
        var paragraph = new Paragraph();

        foreach (var inlineToken in paragraphToken.Inlines) {
            paragraph.Inlines.Add(RenderInline(inlineToken));
        }

        return paragraph;
    }

    private static Inline RenderInline(InlineToken token) {
        return token switch {
            TextToken text => new Run { Text = text.Text },
            LineBreakToken => new LineBreak(),
            BoldToken bold => RenderInlineChildren(new Bold(), bold.Children),
            ItalicToken italic => RenderInlineChildren(new Italic(), italic.Children),
            // Todo: replace by HyperlinkButton
            // https://github.com/microsoft/microsoft-ui-xaml/discussions/11251
            HyperlinkToken hyperlink => RenderInlineChildren(new Hyperlink {
                NavigateUri = new Uri(hyperlink.Href),
                UnderlineStyle = UnderlineStyle.None,
            }, hyperlink.Children),
            _ => new Run(),
        };
    }

    private static Span RenderInlineChildren(Span span, IReadOnlyList<InlineToken> tokens) {
        foreach (var token in tokens) {
            span.Inlines.Add(RenderInline(token));
        }

        return span;
    }

    private abstract record InlineToken;
    private sealed record TextToken(string Text): InlineToken;
    private sealed record LineBreakToken: InlineToken;
    private sealed record BoldToken(IReadOnlyList<InlineToken> Children): InlineToken;
    private sealed record ItalicToken(IReadOnlyList<InlineToken> Children): InlineToken;
    private sealed record HyperlinkToken(string Href, IReadOnlyList<InlineToken> Children): InlineToken;
    private sealed record ParagraphToken(IReadOnlyList<InlineToken> Inlines);
    private sealed record HtmlContentToken(IReadOnlyList<ParagraphToken> Paragraphs);
}