using CommunityToolkit.WinUI.Controls;
using FluentIcons.Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Shapes;

namespace View.Controls;

internal sealed partial class Status: Grid {
    public ViewModel.Controls.Status? ViewModel {
        get;
        set {
            if (field != value) {
                field = value;
                if (DispatcherQueue.HasThreadAccess) {
                    Update();
                } else {
                    _ = DispatcherQueue.TryEnqueue(Update);
                }
            }
        }
    }

    private void Update() {
        PostBodyRichTextBlockExpandedManually = false;
        Bindings.Update();
        PostBodyRichTextBlock.MaxHeight = double.PositiveInfinity;
        UpdateMediaSpans();
    }

    public double AvatarSize { get; set; } = 40;
    public double AvatarScale { set => AvatarScaleTransform.ScaleX = AvatarScaleTransform.ScaleY = value; }
    public double DisplayNameFontSize { get; set; } = 16;
    public bool PostBodyLeftPadding { get; set; } = false;
    public bool ShowQuote { get; set; } = false;
    public bool ReactButtons { get; set; } = false;

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
    string GetShowSpointerButtonContent(bool collapsed) => collapsed ? Localization.L("Controls_Status_ShowSpoilerButton/Text_ShowMore") : Localization.L("Controls_Status_ShowSpoilerButton/Text_ShowLess");

    bool LoadQuote => ShowQuote && (ViewModel?.Quote is not null);

    bool LoadImages => ViewModel?.ImagePreviewsRemote.Length > 0;
    bool LoadImage1 => ViewModel?.ImagePreviewsRemote.Length > 1;
    bool LoadImage2 => ViewModel?.ImagePreviewsRemote.Length > 2;
    bool LoadImage3 => ViewModel?.ImagePreviewsRemote.Length > 3;
    bool LoadImage3Overlay => ViewModel?.ImagePreviewsRemote.Length > 4;
    AspectRatio ImagesGridAspectRatio => ViewModel?.ImagePreviewsRemote.Length > 1 ? ViewModel.FirstImageAspect : 1;
    Orientation ImagesGridOrientation {
        get {
            if (ViewModel?.ImagePreviewsRemote?.Length >= 4) {
                return Orientation.Horizontal;
            } else if (ViewModel?.FirstImageAspect > 1) {
                return Orientation.Horizontal;
            } else {
                return Orientation.Vertical;
            }
        }
    }
    private int Image0ColumnSpan {
        get {
            if (ViewModel?.ImagePreviewsRemote?.Length == 1) {
                return 2;
            } else if (ViewModel?.ImagePreviewsRemote?.Length >= 4) {
                return 1;
            } else if (ViewModel?.FirstImageAspect <= 1) {
                return 1;
            } else {
                return 2;
            }
        }
    }
    private int Image0RowSpan {
        get {
            if (ViewModel?.ImagePreviewsRemote?.Length == 1) {
                return 2;
            } else if (ViewModel?.ImagePreviewsRemote?.Length >= 4) {
                return 1;
            } else if (ViewModel?.FirstImageAspect <= 1) {
                return 2;
            } else {
                return 1;
            }
        }
    }
    private int Image1ColumnSpan {
        get {
            if (ViewModel?.ImagePreviewsRemote?.Length > 2) {
                return 1;
            } else if (ViewModel?.FirstImageAspect <= 1) {
                return 1;
            } else {
                return 2;
            }
        }
    }
    private int Image1RowSpan {
        get {
            if (ViewModel?.ImagePreviewsRemote?.Length > 2) {
                return 1;
            } else if (ViewModel?.FirstImageAspect <= 1) {
                return 2;
            } else {
                return 1;
            }
        }
    }
    string Image3OverlayText => $"+ {ViewModel?.ImagePreviewsRemote.Length - 4}";

    private Brush ReactButtonBackground => Constants.Brush.Transparent;
    private Brush ReactButtonBorderBrush => Constants.Brush.Transparent;
    private Thickness ReactButtonBorderThickness => new(0, 0, 0, 0);
    private Thickness ReactButtonPadding => new(4, 4, 4, 4);
    private double ReactButtonFontSize => 16;
    private Icon GetReplyIcon(bool hasReplies) => hasReplies ? Icon.ArrowReplyAll : Icon.ArrowReply;
    private Icon GetReblogIcon(bool canBeReblogged) => canBeReblogged ? Icon.ArrowRepeatAll : Icon.ArrowRepeatAllOff;
    private IconVariant GetFavouriteIconVariant(bool favourited) => favourited ? IconVariant.Color : IconVariant.Regular;

    private void UpdateMediaSpans() {
        if (Image0 is not null) {
            Grid.SetColumnSpan(Image0, Image0ColumnSpan);
            Grid.SetRowSpan(Image0, Image0RowSpan);
        }
        if (Image1 is not null) {
            Grid.SetColumnSpan(Image1, Image1ColumnSpan);
            Grid.SetRowSpan(Image1, Image1RowSpan);
        }
    }

    bool PostBodyRichTextBlockExpandedManually = false;
    private void PostBodyRichTextBlock_SizeChanged(object sender, SizeChangedEventArgs e) {
        if (PostBodyRichTextBlockExpandedManually) {
            return;
        }
        if (PostBodyRichTextBlock.MaxHeight != double.PositiveInfinity) {
            return;
        }

        if (PostBodyRichTextBlock.ActualHeight > 400) {
            PostBodyRichTextBlock.MaxHeight = 360;
            CollapsePostBodyButton.Visibility = Visibility.Visible;
            OpacityMaskView.OpacityMask = new Rectangle() {
                Fill = Constants.Brush.CollapsedStatusBrush,
            };
        } else {
            CollapsePostBodyButton.Visibility = Visibility.Collapsed;
            OpacityMaskView.OpacityMask = null;
        }
    }

    private void CollapsePostBodyButton_Click(object sender, RoutedEventArgs e) {
        PostBodyRichTextBlockExpandedManually = true;
        PostBodyRichTextBlock.MaxHeight = double.PositiveInfinity;
        CollapsePostBodyButton.Visibility = Visibility.Collapsed;
        OpacityMaskView.OpacityMask = null;
    }

    public Status() {
        InitializeComponent();
    }
}
