using FluentIcons.Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;
using System.Numerics;
using View.Strings;
using View.Utilities;

namespace View.Controls;

internal sealed partial class Status: Grid {
    public ViewModel.Controls.Status? ViewModel {
        get;
        set {
            if (field != value) {
                postBodyGridStoryboard.Stop();
                field = value;
                if (DispatcherQueue.HasThreadAccess) {
                    Bindings.Update();
                } else {
                    _ = DispatcherQueue.TryEnqueue(Bindings.Update);
                }
            }
        }
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

    internal static string GetStatusTimeString(System.DateTimeOffset dateTime) => dateTime.ToRelativeStringShort();

    private int ContentStackPanelGridColumn => PostBodyLeftPadding ? 2 : 0;

    private void ShowSpoilerButtonIcon_SizeChanged(object sender, SizeChangedEventArgs e) {
        ShowSpoilerButtonIcon.CenterPoint = new Vector3((float)ShowSpoilerButtonIcon.ActualWidth / 2, (float)ShowSpoilerButtonIcon.ActualHeight / 2, 0);
    }
    internal static Vector3 GetShowSpoilerButtonIconScale(bool collapsed) => collapsed ? new(1, 1, 1) : new(1, -1, 1);
    private void ShowSpoilerButton_Click(object sender, RoutedEventArgs e) {
        if (ViewModel?.Collapsed is true) {
            ViewModel?.Collapsed = false;
            AnimatePostBodyGrid(true);
        } else {
            ViewModel?.Collapsed = true;
            AnimatePostBodyGrid(false);
        }
    }

    internal static double GetPostBodyGridInitialHeight(bool collapsed) => collapsed ? 0 : double.PositiveInfinity;
    internal static double GetPostBodyGridInitialOpacity(bool collapsed) => collapsed ? 0 : 1;
    private void PostBodyRichTextBlock_SizeChanged(object sender, SizeChangedEventArgs e) {
        if (ViewModel?.SpoilerText is not null) {
            return;
        }
        if (!CollapsePostBodyButton.IsEnabled) {
            return;
        }

        const double MaxPostBodyHeight = 500;
        const double CollapsedPostBodyHeight = 360;

        if (PostBodyRichTextBlock.DesiredSize.Height > MaxPostBodyHeight) {
            PostBodyGrid.MaxHeight = CollapsedPostBodyHeight;
            CollapsePostBodyButton.Visibility = Visibility.Visible;
            CollapsePostBodyButton.Opacity = 1;
            PostBodyGridOpacityMaskView.OpacityMask ??= new Rectangle() {
                Fill = Brushes.CollapsedStatusOpacityMaskBrush,
            };
        } else {
            PostBodyGrid.MaxHeight = double.PositiveInfinity;
            CollapsePostBodyButton.Visibility = Visibility.Collapsed;
            CollapsePostBodyButton.Opacity = 0;
            PostBodyGridOpacityMaskView.OpacityMask = null;
        }
    }
    private void CollapsePostBodyButton_Click(object sender, RoutedEventArgs e) {
        CollapsePostBodyButton.IsEnabled = false;
        AnimatePostBodyGrid(true);
    }
    private readonly Storyboard postBodyGridStoryboard = new();
    private readonly CubicEase postBodyGridEasingFunction = new() {
        EasingMode = EasingMode.EaseOut,
    };
    private readonly DoubleAnimation postBodyGridMaxHeightAnimation;
    private readonly DoubleAnimation postBodyGridOpacityAnimation;
    private readonly DoubleAnimation collapsePostBodyButtonOpacityAnimation;
    private void PostBodyGridStoryboard_Completed(object? sender, object e) {
        if (PostBodyGrid.MaxHeight != 0) {
            PostBodyGrid.MaxHeight = double.PositiveInfinity;
            CollapsePostBodyButton.Visibility = Visibility.Collapsed;
            PostBodyGridOpacityMaskView.OpacityMask = null;
        }
    }
    private void AnimatePostBodyGrid(bool expand) {
        double currentGridHeight = PostBodyGrid.ActualHeight;
        double currentGridMaxHeight = PostBodyGrid.MaxHeight;
        double currentGridOpacity = PostBodyGrid.Opacity;
        double currentCollapsePostBodyButtonOpacity = CollapsePostBodyButton.Opacity;

        postBodyGridStoryboard.Stop();
        if (expand) {
            double targetHeight = PostBodyRichTextBlock.DesiredSize.Height;

            postBodyGridMaxHeightAnimation.From = Math.Max(currentGridMaxHeight, targetHeight - Utilities.Transitions.MaxTransitionDistance);
            postBodyGridMaxHeightAnimation.To = targetHeight;

            postBodyGridOpacityAnimation.From = currentGridOpacity;
            postBodyGridOpacityAnimation.To = 1;

            collapsePostBodyButtonOpacityAnimation.From = currentCollapsePostBodyButtonOpacity;

            ((PostBodyGridOpacityMaskView.OpacityMask as Rectangle)?.Fill as Brushes.FadeOutBrush)?.MoveOut();
        } else {
            postBodyGridMaxHeightAnimation.From = Math.Min(currentGridHeight, Utilities.Transitions.MaxTransitionDistance);
            postBodyGridMaxHeightAnimation.To = 0;

            postBodyGridOpacityAnimation.From = currentGridOpacity;
            postBodyGridOpacityAnimation.To = 0;

            collapsePostBodyButtonOpacityAnimation.From = 0;
        }

        postBodyGridStoryboard.Begin();
    }

    private bool LoadQuote => ShowQuote && (ViewModel?.Quote is not null);

    private bool LoadMedias => ViewModel?.Medias.Length > 0;
    internal static double GetImageOpacity(bool imagePreviewsHidden) => imagePreviewsHidden ? 0 : 1;

    public Status() {
        InitializeComponent();

        postBodyGridMaxHeightAnimation = new() {
            Duration = Utilities.Transitions.DefaultDuration,
            EasingFunction = postBodyGridEasingFunction,
            EnableDependentAnimation = true,
        };
        Storyboard.SetTarget(postBodyGridMaxHeightAnimation, PostBodyGrid);
        Storyboard.SetTargetProperty(postBodyGridMaxHeightAnimation, "MaxHeight");
        postBodyGridStoryboard.Children.Add(postBodyGridMaxHeightAnimation);

        postBodyGridOpacityAnimation = new() {
            Duration = Utilities.Transitions.DefaultDuration,
            EasingFunction = postBodyGridEasingFunction,
        };
        Storyboard.SetTarget(postBodyGridOpacityAnimation, PostBodyGrid);
        Storyboard.SetTargetProperty(postBodyGridOpacityAnimation, "Opacity");
        postBodyGridStoryboard.Children.Add(postBodyGridOpacityAnimation);

        collapsePostBodyButtonOpacityAnimation = new() {
            Duration = Utilities.Transitions.DefaultDuration,
            EasingFunction = postBodyGridEasingFunction,
            To = 0,
        };
        Storyboard.SetTarget(collapsePostBodyButtonOpacityAnimation, CollapsePostBodyButton);
        Storyboard.SetTargetProperty(collapsePostBodyButtonOpacityAnimation, "Opacity");
        postBodyGridStoryboard.Children.Add(collapsePostBodyButtonOpacityAnimation);

        postBodyGridStoryboard.Completed += PostBodyGridStoryboard_Completed;
    }
}
