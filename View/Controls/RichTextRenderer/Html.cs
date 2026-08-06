using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using ViewModel.Controls.RichTextRenderer;

namespace View.Controls.RichTextRenderer;

internal static class Html {

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

        richTextBlock.Blocks.Clear();
        if ((e.NewValue is ViewModel.Controls.RichTextRenderer.Html newVm)) {
            RenderContent(richTextBlock, newVm.ContentToken, newVm.Emojis);
        }
    }

    private static void RenderContent(RichTextBlock richTextBlock, ContentToken content, Dictionary<string, Task<Uri?>> emojis) {
        var blocks = richTextBlock.Blocks;

        foreach (var paragraphToken in content.Paragraphs) {
            blocks.Add(RenderParagraph(paragraphToken, emojis, richTextBlock.FontSize));
        }

        for (int i = 0; i < blocks.Count - 1; i++) {
            var block = blocks[i];
            block.Margin = new Thickness(0, 0, 0, block.FontSize);
        }
    }

    private static Paragraph RenderParagraph(ParagraphToken paragraphToken, Dictionary<string, Task<Uri?>> emojis, double fontSize) {
        var paragraph = new Paragraph();

        foreach (var inlineToken in paragraphToken.Inlines) {
            var inline = RenderInline(inlineToken, emojis, fontSize);
            if (inline != null) {
                paragraph.Inlines.Add(inline);
            }
        }

        return paragraph;
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
            return new Run { Text = shortCode };
        }
    }
}