using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Documents;
using View.Controls.Components;
using ViewModel.Extensions;

namespace View.Extensions;

internal static partial class RichTextBlock {
    public static readonly DependencyProperty HtmlProperty = DependencyProperty.RegisterAttached(
        "Html",
        typeof(Html),
        typeof(RichTextBlock),
        new PropertyMetadata(null, OnHtmlChangedAsync)
    );

    public static Html? GetHtml(DependencyObject obj) {
        return (Html?)obj.GetValue(HtmlProperty);
    }

    public static void SetHtml(DependencyObject obj, Html? value) {
        obj.SetValue(HtmlProperty, value);
    }

    private static async void OnHtmlChangedAsync(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if ((e.OldValue == e.NewValue) ||
            (e.NewValue is not Html newVm)) {
            return;
        }

        if (d is Microsoft.UI.Xaml.Controls.RichTextBlock richTextBlock) {
            richTextBlock.Blocks.Clear();
            RenderRichContent(richTextBlock, newVm.ContentToken, newVm.Emojis);
        } else if ((d is Span span) && (newVm.ContentToken.Paragraphs.Count > 0)) {
            foreach (var inline in RenderParagraph(newVm.ContentToken.Paragraphs.First(), newVm.Emojis, span.FontSize)) {
                span.Inlines.Add(inline);
            }
        }
    }

    private static void RenderRichContent(Microsoft.UI.Xaml.Controls.RichTextBlock richTextBlock, ContentToken content, Dictionary<string, Task<Uri?>> emojis) {
        var blocks = richTextBlock.Blocks;

        foreach (var paragraphToken in content.Paragraphs) {
            var paragraph = new Paragraph();
            foreach (var inline in RenderParagraph(paragraphToken, emojis, richTextBlock.FontSize)) {
                paragraph.Inlines.Add(inline);
            }
            blocks.Add(paragraph);
        }

        for (int i = 0; i < blocks.Count - 1; i++) {
            var block = blocks[i];
            block.Margin = new Thickness(0, 0, 0, block.FontSize);
        }
    }

    private static IEnumerable<Inline> RenderParagraph(ParagraphToken paragraphToken, Dictionary<string, Task<Uri?>> emojis, double fontSize) {
        foreach (var inlineToken in paragraphToken.Inlines) {
            var inline = RenderInline(inlineToken, emojis, fontSize);
            if (inline != null) {
                yield return inline;
            }
        }
    }

    private static Inline? RenderInline(InlineToken token, Dictionary<string, Task<Uri?>> emojis, double fontSize) {
        return token switch {
            TextToken text => new Run { Text = text.Text },
            EmojiToken emoji => CreateEmoji(emoji.ShortCode, emojis, fontSize),
            LineBreakToken => new LineBreak(),
            BoldToken bold => RenderInlineChildren(new Bold(), bold.Children, emojis, fontSize),
            ItalicToken italic => RenderInlineChildren(new Italic(), italic.Children, emojis, fontSize),
            HyperlinkToken hyperlink => RenderInlineChildren(new Hyperlink {
                NavigateUri = new Uri(hyperlink.Href),
            }, hyperlink.Children, emojis, fontSize),
            _ => null,
        };
    }

    private static Span RenderInlineChildren(Span span, IReadOnlyCollection<InlineToken> tokens, Dictionary<string, Task<Uri?>> emojis, double fontSize) {
        foreach (var token in tokens) {
            var inline = RenderInline(token, emojis, fontSize);
            if (inline != null) {
                span.Inlines.Add(inline);
            }
        }

        return span;
    }

    private static async Task WaitForDownloadComplete(Emoji control, Task<Uri?> task) {
        var downloaded = await task;
        control.Source = downloaded;
    }

    private static Inline CreateEmoji(string shortCode, Dictionary<string, Task<Uri?>> emojis, double fontSize) {
        if (emojis.TryGetValue(shortCode, out var sourceTask)) {
            var emojiControl = new Emoji {
                Width = fontSize,
                Height = fontSize,
            };
            if (sourceTask.IsCompletedSuccessfully) {
                emojiControl.Source = sourceTask.Result;
            } else if (!sourceTask.IsCompleted) {
                _ = WaitForDownloadComplete(emojiControl, sourceTask);
            }
            return new InlineUIContainer {
                Child = emojiControl,
            };
        } else {
            return new Run { Text = $":{shortCode}:" };
        }
    }
}