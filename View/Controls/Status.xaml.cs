using FluentIcons.Common;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace View.Controls;

internal sealed partial class Status: Grid {
    public ViewModel.Controls.Status? ViewModel {
        get;
        set {
            if (field != value) {
                field = value;
                _ = DispatcherQueue.TryEnqueue(Bindings.Update);
            }
        }
    }

    public Status() {
        InitializeComponent();
    }

    private readonly double lineWidth = 4;
    private double AboveLineHeight => Padding.Top + RowDefinitions[1].Height.Value / 2;
    private Thickness AboveLineMargin => new(0, -Padding.Top, 0, 0);
    private int BelowLineRowSpan => RowDefinitions.Count - 1;
    private Thickness BelowLineMargin => new(0, RowDefinitions[1].Height.Value / 2, 0, -Padding.Bottom);
    private double AvatarButtonSize => ViewModel?.RebloggerId is null ? ColumnDefinitions[0].Width.Value : ColumnDefinitions[0].Width.Value * 0.9;
    private CornerRadius AvatarButtonCornerRadius => new(AvatarButtonSize / 2);
    private double RebloggerAvatarSize => ColumnDefinitions[0].Width.Value / 2;
    private CornerRadius RebloggerAvatarCornerRadius => new(RebloggerAvatarSize / 2);
    private double PosterIdFontSize => DisplayNameTextBlock.FontSize * 0.85;

    private readonly Brush reactButtonBackground = new SolidColorBrush(Colors.Transparent);
    private readonly Brush reactButtonBorderBrush = new SolidColorBrush(Colors.Transparent);
    private readonly Thickness reactButtonBorderThickness = new(0, 0, 0, 0);
    private readonly Thickness reactButtonPadding = new(4, 4, 4, 4);
    private readonly double reactButtonFontSize = 16;
    private Icon GetReplyIcon(bool hasReplies) => hasReplies ? Icon.ArrowReplyAll : Icon.ArrowReply;
    private Icon GetReblogIcon(bool canBeReblogged) => canBeReblogged ? Icon.ArrowRepeatAll : Icon.ArrowRepeatAllOff;
    private IconVariant GetFavouriteIconVariant(bool favourited) => favourited ? IconVariant.Color : IconVariant.Regular;

    private Thickness LoadMoreButtonBorderThickness => new(0, BorderThickness.Bottom, 0, 0);
    private Thickness LoadMoreButtonMargin => new(-Padding.Left, Padding.Bottom, -Padding.Right, -Padding.Bottom);
}
