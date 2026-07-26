using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Model.Access;
using System.Runtime.Caching;
using System.Text;

namespace View.Controls.RichTextRenderer;

internal static class TextWithEmoji {
    private static readonly MemoryCache tokenCache = new(nameof(TextWithEmoji));
    private static readonly CacheItemPolicy tokenCachePolicy = new() {
        SlidingExpiration = TimeSpan.FromMinutes(30),
    };

    public static readonly DependencyProperty ViewModelProperty = DependencyProperty.RegisterAttached(
        "ViewModel",
        typeof(ViewModel.Controls.RichTextRenderer.TextWithEmoji),
        typeof(TextWithEmoji),
        new PropertyMetadata(null, OnViewModelChanged)
    );

    public static ViewModel.Controls.RichTextRenderer.TextWithEmoji? GetViewModel(DependencyObject obj) {
        return (ViewModel.Controls.RichTextRenderer.TextWithEmoji?)obj.GetValue(ViewModelProperty);
    }

    public static void SetViewModel(DependencyObject obj, ViewModel.Controls.RichTextRenderer.TextWithEmoji? value) {
        obj.SetValue(ViewModelProperty, value);
    }

    private static async void OnViewModelChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) {
        if (d is not RichTextBlock richTextBlock) {
            return;
        }

        if (e.OldValue == e.NewValue) {
            return;
        }

        if ((e.NewValue is ViewModel.Controls.RichTextRenderer.TextWithEmoji newVm) && (!String.IsNullOrEmpty(newVm.RawText))) {
            _ = UpdateContent(richTextBlock, newVm.RawText, newVm.Emojis);
        } else {
            richTextBlock.Blocks.Clear();
        }
    }

    private static async Task UpdateContent(RichTextBlock richTextBlock, string rawText, Dictionary<string, Uri> emojis) {
        if (tokenCache.Get(rawText) is not ContentToken tokenizedContent) {
            tokenizedContent = await Task.Run(() => TokenizeTextWithEmoji(rawText));
            tokenCache.Add(rawText, tokenizedContent, tokenCachePolicy);
        }

        RenderContent(richTextBlock, tokenizedContent, emojis);
    }

    private static void RenderContent(RichTextBlock richTextBlock, ContentToken content, Dictionary<string, Uri> emojis) {
        var blocks = richTextBlock.Blocks;
        blocks.Clear();
        var fontSize = richTextBlock.FontSize > 0 ? richTextBlock.FontSize : 16;

        foreach (var paragraphToken in content.Paragraphs) {
            var paragraph = new Paragraph();
            foreach (var inlineToken in paragraphToken.Inlines) {
                var inline = RenderInline(inlineToken, emojis, fontSize);
                if (inline != null) {
                    paragraph.Inlines.Add(inline);
                }
            }

            blocks.Add(paragraph);
        }
    }

    internal static ContentToken TokenizeTextWithEmoji(string rawText) {
        var tokens = new List<InlineToken>();
        var text = new StringBuilder();

        int index = 0;
        while (index < rawText.Length) {
            if ((rawText[index] != ':') ||
                (!TryReadEmojiPlaceholder(rawText, ref index, out var placeholder, out var shortCode))) {
                text.Append(rawText[index]);
                index++;
                continue;
            }

            FlushText();
            tokens.Add(new EmojiToken(shortCode, placeholder));
        }

        FlushText();
        return new ContentToken([
            new ParagraphToken(tokens),
        ]);

        void FlushText() {
            if (text.Length == 0) {
                return;
            }

            tokens.Add(new TextToken(text.ToString()));
            text.Clear();
        }
    }

    private static bool TryReadEmojiPlaceholder(string rawText, ref int index, out string placeholder, out string shortCode) {
        placeholder = string.Empty;
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

        var endIndex = cursor + 1;
        placeholder = rawText[startIndex..endIndex];
        shortCode = rawText[(startIndex + 1)..cursor];
        index = endIndex;
        return true;
    }

    internal static Inline? RenderInline(InlineToken token, Dictionary<string, Uri> emojis, double fontSize) {
        switch (token) {
        case TextToken text:
            return new Run { Text = text.Text };
        case EmojiToken emoji when emojis.TryGetValue(emoji.ShortCode, out var source):
            var emojiControl = new Emoji {
                Width = fontSize,
                Height = fontSize,
            };
            _ = DownloadEmoji(emojiControl, source);
            return new InlineUIContainer {
                Child = emojiControl,
            };
        case EmojiToken emoji:
            return new Run { Text = emoji.Placeholder };
        default:
            return null;
        }
    }

    private static async Task DownloadEmoji(Emoji control, Uri uri) {
        var downloaded = await Cache.Get(uri);
        control.Source = downloaded;
    }
}