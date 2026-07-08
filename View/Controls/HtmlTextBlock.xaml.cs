using AngleSharp;
using AngleSharp.Dom;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;

namespace View.Controls;

internal sealed partial class HtmlTextBlock: UserControl {
    public TextWrapping TextWrapping { 
        get => RichTextBlock.TextWrapping;
        set => RichTextBlock.TextWrapping = value;
    }
    public bool IsTextSelectionEnabled {
        get => RichTextBlock.IsTextSelectionEnabled;
        set => RichTextBlock.IsTextSelectionEnabled = value;
    }

    public string Html {
        get; set {
            if (field != value) {
                field = value;
                UpdateContent(field);
            }
        }
    } = string.Empty;

    public HtmlTextBlock() {
        InitializeComponent();
    }

    private async void UpdateContent(string html) {
        var browsingContext = new BrowsingContext();
        var document = await browsingContext.OpenAsync(req => req.Content(html));
        var blocks = RichTextBlock.Blocks;
        blocks.Clear();

        if (document.Body == null || document.Body.ChildElementCount == 0) {
            blocks.Add(new Paragraph {
                Inlines = {
                    new Run {
                        Text = html,
                    },
                },
            });
            return;
        } 

        foreach (var child in document.Body.Children) {
            var paragraph = Parse(child);
            if (paragraph is not null) {
                blocks.Add(paragraph);
            }
        }

        for (int i = 0; i < blocks.Count - 1; i++) {
            var block = blocks[i];
            block.Margin = new Thickness(0, 0, 0, block.FontSize);
        }
    }

    private Paragraph? Parse(IElement element) {
        if (element is IText text) {
            return new Paragraph {
                Inlines = {
                    new Run {
                        Text = text.Text,
                    },
                },
            };
        }

        return element.TagName.ToLower() switch {
            "br" => new Paragraph {
                Inlines = {
                    new LineBreak(),
                },
            },
            "p" or "div" or _ => ParseParagraph(element),
        };
    }

    private Paragraph ParseParagraph(IElement element) {
        var paragraph = new Paragraph();

        foreach (var child in element.ChildNodes) {
            var inline = ParseInline(child);
            if (inline is not null) {
                paragraph.Inlines.Add(inline);
            }
        }

        return paragraph;
    }

    private Inline? ParseInline(INode node) {
        switch (node) {
        case IText text:
            return new Run { Text = text.Text };
        case IElement element:
            var cls = element.GetAttribute("class");
            if (cls is not null && cls.Contains("invisible")) {
                return null; 
            }

            switch (element.TagName.ToLower()) {
            case "b":
            case "strong":
                return ParseInlineChildren(new Bold(), element);
            case "i":
            case "em":
                return ParseInlineChildren(new Italic(), element);
            case "a":
                var href = element.GetAttribute("href");
                if (string.IsNullOrEmpty(href)) {
                    // fallback: treat <a> without href as plain text
                    return new Run { Text = element.TextContent };
                }

                return ParseInlineChildren(new Hyperlink {
                    NavigateUri = new Uri(href)
                }, element);
            case "br":
                return new LineBreak();
            default:
                // fallback: treat unknown inline elements as plain text
                return new Run { Text = element.TextContent };
            }
        default:
            return null;
        }
    }

    private Span ParseInlineChildren(Span span, IElement element) {
        foreach (var child in element.ChildNodes) {
            var inline = ParseInline(child);
            if (inline is not null) {
                span.Inlines.Add(inline);
            }
        }

        return span;
    }
}
