using CommunityToolkit.WinUI.Controls;
using FluentIcons.Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;
using System.Numerics;
using View.Controls.Components;
using View.Strings;

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

    public double AvatarSize { get; set; } = 36;
    public double AvatarScale { set => AvatarScaleTransform.ScaleX = AvatarScaleTransform.ScaleY = value; }
    public double DisplayNameFontSize { get; set; } = 14;
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

    internal static string GetStatusTimeString(System.DateTime dateTime) => dateTime.ToRelativeStringShort();

    private int ContentStackPanelGridColumn => PostBodyLeftPadding ? 2 : 0;

    private void ShowSpoilerButtonIcon_SizeChanged(object sender, SizeChangedEventArgs e) {
        ShowSpoilerButtonIcon.CenterPoint = new Vector3((float)ShowSpoilerButtonIcon.ActualWidth / 2, (float)ShowSpoilerButtonIcon.ActualHeight / 2, 0);
    }
    internal static Vector3 GetShowSpoilerButtonIconScale(bool collapsed) => collapsed ? new(1, 1, 1) : new(1, -1, 1);
    private void ShowSpoilerButton_Click(object sender, RoutedEventArgs e) {
        if (ViewModel?.Collapsed is true) {
            ViewModel?.Collapsed = false;
            AnimatePostBodyGridHeight(PostBodyRichTextBlock.ActualHeight);
        } else {
            ViewModel?.Collapsed = true;
            AnimatePostBodyGridHeight(0);
        }
    }

    internal static double GetPostBodyGridInitialHeight(bool collapsed) => collapsed ? 0 : double.NaN;
    private bool PostBodyRichTextBlockExpandedManually = false;
    private void PostBodyRichTextBlock_SizeChanged(object sender, SizeChangedEventArgs e) {
        if (ViewModel?.SpoilerText is not null) {
            return;
        }
        if (PostBodyRichTextBlockExpandedManually) {
            return;
        }

        if (PostBodyRichTextBlock.ActualHeight > 500) {
            PostBodyGrid.Height = 360;
            CollapsePostBodyButton.Visibility = Visibility.Visible;
             PostBodyRichTextBlockOpacityMaskForeground.Opacity = 0;
        } else {
            PostBodyGrid.Height = double.NaN;
            CollapsePostBodyButton.Visibility = Visibility.Collapsed;
             PostBodyRichTextBlockOpacityMaskForeground.Opacity = 1;
        }
    }
    private void CollapsePostBodyButton_Click(object sender, RoutedEventArgs e) {
        PostBodyRichTextBlockExpandedManually = true;
        CollapsePostBodyButton.Visibility = Visibility.Collapsed;
        PostBodyRichTextBlockOpacityMaskForeground.OpacityTransition = View.Transitions.ScalarTransition;
        PostBodyRichTextBlockOpacityMaskForeground.Opacity = 1;
        PostBodyRichTextBlockOpacityMaskForeground.OpacityTransition = null;
        AnimatePostBodyGridHeight(PostBodyRichTextBlock.ActualHeight);
    }
    private readonly Storyboard PostBodyGridHeightStoryboard = new();
    private readonly DoubleAnimation PostBodyGridHeightStoryboardAnimation = new() {
        Duration = View.Transitions.Vector3Transition.Duration,
        EnableDependentAnimation = true,
        EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut },
    };
    private void PostBodyGridHeightStoryboard_Completed(object? sender, object e) {
        if (PostBodyGrid.Height > 0) {
            PostBodyGrid.Height = double.NaN;
        }
    }
    private void PostBodyGrid_Loaded(object sender, RoutedEventArgs e) {
        PostBodyGridHeightStoryboard.Completed -= PostBodyGridHeightStoryboard_Completed;
        PostBodyGridHeightStoryboard.Completed += PostBodyGridHeightStoryboard_Completed;
        PostBodyGridHeightStoryboard.Children.Add(PostBodyGridHeightStoryboardAnimation);
        Storyboard.SetTarget(PostBodyGridHeightStoryboardAnimation, PostBodyGrid);
        Storyboard.SetTargetProperty(PostBodyGridHeightStoryboardAnimation, "Height");
    }
    private void AnimatePostBodyGridHeight(double targetHeight) {
        double currentHeight = PostBodyGrid.ActualHeight;
        PostBodyGridHeightStoryboard.Stop();
        PostBodyGrid.Height = currentHeight;

        PostBodyGridHeightStoryboardAnimation.From = currentHeight;
        PostBodyGridHeightStoryboardAnimation.To = targetHeight;
        PostBodyGridHeightStoryboard.Begin();
    }

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
    internal static MediaPreview.Blur? GetMediaPreviewBackgroundBlurHash(ViewModel.Controls.Status.MediaPreview? mediaPreview) {
        if (mediaPreview is null) {
            return null;
        }
        return new(mediaPreview.BlurHash, mediaPreview.AspectRatio);
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
    private string Image3OverlayText => $"+ {ViewModel?.ImagesRemote.Length - 4}";
    internal static Icon GetToggleMediaPreviewsButtonIcon(bool imagePreviewHidden) => imagePreviewHidden ? Icon.EyeOff : Icon.Eye;

    internal static Thickness ReactButtonPadding => new(4, 4, 4, 4);
    internal static double ReactButtonFontSize => 16;
    internal static Icon GetReplyIcon(bool isReply) => isReply ? Icon.ArrowReplyAll : Icon.ArrowReply;
    internal static Icon GetReblogIcon(bool canBeReblogged) => canBeReblogged ? Icon.ArrowRepeatAll : Icon.ArrowRepeatAllOff;
    internal static IconVariant GetFavouriteIconVariant(bool favourited) => favourited ? IconVariant.Color : IconVariant.Regular;

    public Status() {
        InitializeComponent();
    }
}
