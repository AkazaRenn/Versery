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

    private double PosterAccountFontSize => DisplayNameFontSize * 0.85;

    private int ContentStackPanelGridColumn => PostBodyLeftPadding ? 2 : 0;

    private bool LoadSpoilerTextRichTextBlock => ViewModel?.SpoilerText is not null;
    internal static string GetShowSpointerButtonContent(bool collapsed) => collapsed ? Localization.L("Controls_Status_ShowSpoilerButton/Text_ShowMore") : Localization.L("Controls_Status_ShowSpoilerButton/Text_ShowLess");

    private bool LoadQuote => ShowQuote && (ViewModel?.Quote is not null);

    private bool LoadImages => ViewModel?.ImagesRemote.Length > 0;
    private bool LoadImage1 => ViewModel?.ImagesRemote.Length > 1;
    private bool LoadImage2 => ViewModel?.ImagesRemote.Length > 2;
    private bool LoadImage3 => ViewModel?.ImagesRemote.Length > 3;
    private bool LoadImage3Overlay => ViewModel?.ImagesRemote.Length > 4;
    private AspectRatio ImagesGridAspectRatio => ViewModel?.ImagesRemote.Length > 1 ? ViewModel.FirstImageAspect : 1;
    private Orientation ImagesGridOrientation {
        get {
            if (ViewModel?.ImagesRemote.Length >= 4) {
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
            if (ViewModel?.ImagesRemote.Length == 1) {
                return 2;
            } else if (ViewModel?.ImagesRemote.Length >= 4) {
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
            if (ViewModel?.ImagesRemote.Length == 1) {
                return 2;
            } else if (ViewModel?.ImagesRemote.Length >= 4) {
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
            if (ViewModel?.ImagesRemote.Length > 2) {
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
            if (ViewModel?.ImagesRemote.Length > 2) {
                return 1;
            } else if (ViewModel?.FirstImageAspect <= 1) {
                return 2;
            } else {
                return 1;
            }
        }
    }
    private string Image3OverlayText => $"+ {ViewModel?.ImagesRemote.Length - 4}";
    internal static Icon GetToggleMediaPreviewsButtonIcon(bool imagePreviewHidden) => imagePreviewHidden ? Icon.EyeOff : Icon.Eye;

    internal static Thickness ReactButtonPadding => new(4, 4, 4, 4);
    internal static double ReactButtonFontSize => 16;
    internal static Icon GetReplyIcon(bool hasReplies) => hasReplies ? Icon.ArrowReplyAll : Icon.ArrowReply;
    internal static Icon GetReblogIcon(bool canBeReblogged) => canBeReblogged ? Icon.ArrowRepeatAll : Icon.ArrowRepeatAllOff;
    internal static IconVariant GetFavouriteIconVariant(bool favourited) => favourited ? IconVariant.Color : IconVariant.Regular;

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

    private bool PostBodyRichTextBlockExpandedManually = false;
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
                Fill = Brushes.CollapsedStatusBrush,
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
