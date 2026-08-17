using CommunityToolkit.WinUI.Controls;
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
    public double AvatarScale { set => AvatarScaleTransform.ScaleX = AvatarScaleTransform.ScaleY = value; }
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

    private CornerRadius AvatarButtonCornerRadius => new(AvatarSize / 2);

    double PosterAccountFontSize => DisplayNameFontSize * 0.85;

    int ContentStackPanelGridColumn => PostBodyLeftPadding ? 2 : 0;

    bool LoadSpoilerTextRichTextBlock => ViewModel?.SpoilerText is not null;
    string GetShowSpointerButtonContent(bool collapsed) => collapsed ? "Show more" : "Show less";

    bool LoadQuote => ShowQuote && (ViewModel?.Quote is not null);

    bool LoadMediaAttachments => ViewModel?.MediaPreviewsRemote.Length > 0;
    bool LoadMediaAttachment1 => ViewModel?.MediaPreviewsRemote.Length > 1;
    bool LoadMediaAttachment2 => ViewModel?.MediaPreviewsRemote.Length > 2;
    bool LoadMediaAttachment3 => ViewModel?.MediaPreviewsRemote.Length > 3;
    bool LoadMediaAttachment3Overlay => ViewModel?.MediaPreviewsRemote.Length > 4;
    AspectRatio MediaAttachmentsGridAspectRatio => ViewModel?.MediaPreviewsRemote.Length > 1 ? ViewModel.FirstImageAspect : 1;
    Orientation MediaAttachmentsGridOrientation {
        get {
            if (ViewModel?.MediaPreviewsRemote?.Length >= 4) {
                return Orientation.Horizontal;
            } else if (ViewModel?.FirstImageAspect > 1) {
                return Orientation.Horizontal;
            } else {
                return Orientation.Vertical;
            }
        }
    }
    private int MediaAttachment0ColumnSpan {
        get {
            if (ViewModel?.MediaPreviewsRemote?.Length == 1) {
                return 2;
            } else if (ViewModel?.MediaPreviewsRemote?.Length >= 4) {
                return 1;
            } else if (ViewModel?.FirstImageAspect <= 1) {
                return 1;
            } else {
                return 2;
            }
        }
    }
    private int MediaAttachment0RowSpan {
        get {
            if (ViewModel?.MediaPreviewsRemote?.Length == 1) {
                return 2;
            } else if (ViewModel?.MediaPreviewsRemote?.Length >= 4) {
                return 1;
            } else if (ViewModel?.FirstImageAspect <= 1) {
                return 2;
            } else {
                return 1;
            }
        }
    }
    private int MediaAttachment1ColumnSpan {
        get {
            if (ViewModel?.MediaPreviewsRemote?.Length > 2) {
                return 1;
            } else if (ViewModel?.FirstImageAspect <= 1) {
                return 1;
            } else {
                return 2;
            }
        }
    }
    private int MediaAttachment1RowSpan {
        get {
            if (ViewModel?.MediaPreviewsRemote?.Length > 2) {
                return 1;
            } else if (ViewModel?.FirstImageAspect <= 1) {
                return 2;
            } else {
                return 1;
            }
        }
    }
    string MediaAttachment3OverlayText => $"+ {ViewModel?.MediaPreviewsRemote.Length - 4}";

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
