using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using Model.Access;
using System.Runtime.Caching;
using System.Text;
using View.Controls;

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

        if (e.NewValue is ViewModel.Controls.RichTextRenderer.TextWithEmoji newVm) {
            foreach (var item in newVm.Emojis.ToArray()) {
                if (await Cache.Get(item.Value) is Uri uri) {
                    newVm.Emojis[item.Key] = uri;
                }
            }
            UpdateContent(richTextBlock, newVm.RawText, newVm.Emojis);
            return;
        }

        richTextBlock.Blocks.Clear();
    }

    private static void UpdateContent(RichTextBlock richTextBlock, string rawText, Dictionary<string, Uri> emojis) {
        if (tokenCache.Get(rawText) is not DisplayNameContentToken tokenizedContent) {
            tokenizedContent = TokenizeDisplayName(rawText);
            tokenCache.Add(rawText, tokenizedContent, tokenCachePolicy);
        }

        RenderContent(richTextBlock, tokenizedContent, emojis);
    }

    private static void RenderContent(RichTextBlock richTextBlock, DisplayNameContentToken content, Dictionary<string, Uri> emojis) {
        var blocks = richTextBlock.Blocks;
        blocks.Clear();

        var paragraph = new Paragraph();
        var fontSize = richTextBlock.FontSize > 0 ? richTextBlock.FontSize : 16;

        foreach (var inlineToken in content.Inlines) {
            paragraph.Inlines.Add(RenderInline(inlineToken, emojis, fontSize));
        }

        blocks.Add(paragraph);
    }

    private static DisplayNameContentToken TokenizeDisplayName(string rawText) {
        var tokens = new List<InlineToken>();
        var text = new StringBuilder();

        int index = 0;
        while (index < rawText.Length) {
            if ((rawText[index] != ':') ||
                (!TryReadEmojiPlaceholder(rawText, ref index, out var placeholder, out var shortcode))) {
                text.Append(rawText[index]);
                index++;
                continue;
            }

            FlushText();
            tokens.Add(new EmojiToken(shortcode, placeholder));
        }

        FlushText();
        return new DisplayNameContentToken(tokens);

        void FlushText() {
            if (text.Length == 0) {
                return;
            }

            tokens.Add(new TextToken(text.ToString()));
            text.Clear();
        }
    }

    private static bool TryReadEmojiPlaceholder(string rawText, ref int index, out string placeholder, out string shortcode) {
        placeholder = string.Empty;
        shortcode = string.Empty;

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
        var endIndex = hasTrailingColon ? cursor + 1 : cursor;
        placeholder = rawText[startIndex..endIndex];
        shortcode = rawText[(startIndex + 1)..cursor];
        index = endIndex;
        return true;
    }

    private static Inline RenderInline(InlineToken token, Dictionary<string, Uri> emojis, double fontSize) {
        return token switch {
            TextToken text => new Run { Text = text.Text },
            EmojiToken emoji when emojis.TryGetValue(emoji.Shortcode, out var source) => new InlineUIContainer {
                Child = new Emoji {
                    Source = source,
                    Width = fontSize,
                    Height = fontSize,
                    VerticalAlignment = VerticalAlignment.Center,
                },
            },
            EmojiToken emoji => new Run { Text = emoji.Placeholder },
            _ => new Run(),
        };
    }

    private abstract record InlineToken;
    private sealed record TextToken(string Text): InlineToken;
    private sealed record EmojiToken(string Shortcode, string Placeholder): InlineToken;
    private sealed record DisplayNameContentToken(IReadOnlyList<InlineToken> Inlines);
}