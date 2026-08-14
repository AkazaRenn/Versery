using FluentIcons.Common;
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
                if (DispatcherQueue.HasThreadAccess) {
                    Bindings.Update();
                } else {
                    _ = DispatcherQueue.TryEnqueue(Bindings.Update);
                }
            }
        }
    }

    public double AvatarSize { get; set; } = 40;
    public double AvatarScale { 
        get => AvatarScaleTransform.ScaleX;
        set => AvatarScaleTransform.ScaleX = AvatarScaleTransform.ScaleY = value;
    }
    public double DisplayNameFontSize { get; set; } = 16;
    public bool PostBodyLeftPadding { get; set; } = false;
    public bool ShowQuote { get; set; } = true;
    public bool ReactButtons { get; set; } = true;

    private GridLength GridColumnWidth0 => new(AvatarSize);
    private GridLength GridRowHeight0 => new(AvatarSize / 2);
    private GridLength GridRowHeight1 => GridRowHeight0;

    private bool LoadAboveLine => false;
    private bool LoadBelowLine => false;
    private double LineWidth => 4;

    private double AvatarButtonSize => AvatarSize * AvatarScale;
    private CornerRadius AvatarButtonCornerRadius => new(AvatarButtonSize / 2);

    double PosterAccountFontSize => DisplayNameFontSize * 0.85;

    int ContentStackPanelGridColumn => PostBodyLeftPadding ? 2 : 0;

    bool LoadQuote => ShowQuote && (ViewModel?.Quote is not null);

    bool LoadMediaAttachments => (ViewModel?.MediaAttachments is not null) && (ViewModel.MediaAttachments.Count > 0);
    private int MediaAttachment0ColumnSpan => ViewModel?.MediaAttachments?.Count > 1 ? 1 : 2;
    private int MediaAttachment0RowSpan => ViewModel?.MediaAttachments?.Count > 3 ? 1 : 2;
    private Uri? MediaAttachment0Source => ViewModel?.MediaAttachments?.ElementAtOrDefault(0);
    private int MediaAttachment1RowSpan => ViewModel?.MediaAttachments?.Count > 2 ? 1 : 2;
    private Uri? MediaAttachment1Source => ViewModel?.MediaAttachments?.ElementAtOrDefault(1);
    private int MediaAttachment2Row => ViewModel?.MediaAttachments?.Count > 3 ? 0 : 1;
    private Uri? MediaAttachment2Source => ViewModel?.MediaAttachments?.ElementAtOrDefault(2);
    private Uri? MediaAttachment3Source => ViewModel?.MediaAttachments?.ElementAtOrDefault(3);

    private Brush ReactButtonBackground => Constants.Brush.Transparent;
    private Brush ReactButtonBorderBrush => Constants.Brush.Transparent;
    private Thickness ReactButtonBorderThickness => new(0, 0, 0, 0);
    private Thickness ReactButtonPadding => new(4, 4, 4, 4);
    private double ReactButtonFontSize => 16;
    private Icon GetReplyIcon(bool hasReplies) => hasReplies ? Icon.ArrowReplyAll : Icon.ArrowReply;
    private Icon GetReblogIcon(bool canBeReblogged) => canBeReblogged ? Icon.ArrowRepeatAll : Icon.ArrowRepeatAllOff;
    private IconVariant GetFavouriteIconVariant(bool favourited) => favourited ? IconVariant.Color : IconVariant.Regular;

    public Status() {
        InitializeComponent();
    }
}
