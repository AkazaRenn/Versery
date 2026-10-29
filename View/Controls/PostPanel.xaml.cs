using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;

namespace View.Controls;

internal sealed partial class PostPanel: ContentDialog {
    public ViewModel.Controls.PostPanel? ViewModel { get; set; } = null;
    public new UIElement? Content {
       get => Container.Child;
       set => Container.Child = value;
    }

    public PostPanel() {
        InitializeComponent();
        //Bindings.Initialize();

        containerTranslationStoryboardAnimation = new() {
            Duration = Utilities.Transitions.DefaultDuration,
            EasingFunction = new CubicEase(),
        };
        transitionStoryboard.Children.Add(containerTranslationStoryboardAnimation);
        Storyboard.SetTarget(containerTranslationStoryboardAnimation, ContainerTranslateTransform);
        Storyboard.SetTargetProperty(containerTranslationStoryboardAnimation, "Y");

        smokeLayerOpacityStoryboardAnimation = new() {
            Duration = containerTranslationStoryboardAnimation.Duration,
            EasingFunction = containerTranslationStoryboardAnimation.EasingFunction,
        };
        transitionStoryboard.Children.Add(smokeLayerOpacityStoryboardAnimation);
        Storyboard.SetTargetProperty(smokeLayerOpacityStoryboardAnimation, "Opacity");
    }

    public async void HideGracefully() {
        var AnimationCompletedTask = new TaskCompletionSource();

        transitionStoryboard.Completed += (s, e) => {
            AnimationCompletedTask.SetResult();
        };

        AnimateTransition(false);
        await AnimationCompletedTask.Task;
        Hide();
    }

    public new void Hide() {
        base.Hide();
        Popup.IsOpen = false;
    }

    private void ContentDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args) {
        // ContentDialog's smoke popup is the open root popup whose child is a bare Rectangle.
        var smokeLayer = VisualTreeHelper.GetOpenPopupsForXamlRoot(XamlRoot)
            .Select(p => p.Child)
            .OfType<Rectangle>()
            .FirstOrDefault();
        if (smokeLayer is not null) {
            smokeLayer.Opacity = 0;
            Storyboard.SetTarget(smokeLayerOpacityStoryboardAnimation, smokeLayer);
            smokeLayer.SizeChanged += SmokeLayer_SizeChanged;
        }

        Container.Height = XamlRoot.Size.Height;
        Container.Width = XamlRoot.Size.Width;
        Popup.XamlRoot = XamlRoot;
        Popup.IsOpen = true;
    }

    private void SmokeLayer_SizeChanged(object sender, SizeChangedEventArgs e) {
        Container.Height = e.NewSize.Height;
        Container.Width = e.NewSize.Width;
    }

    private void Container_Loaded(object sender, RoutedEventArgs e) {
        AnimateTransition(true);
    }

    private async void Content_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e) {
        HideGracefully();
    }

    private readonly Storyboard transitionStoryboard = new();
    private readonly DoubleAnimation containerTranslationStoryboardAnimation;
    private readonly DoubleAnimation smokeLayerOpacityStoryboardAnimation;
    private void AnimateTransition(bool enter) {
        transitionStoryboard.Stop();

        if (enter) {
            containerTranslationStoryboardAnimation.EasingFunction.EasingMode = EasingMode.EaseOut;

            containerTranslationStoryboardAnimation.From = ContentInternal.ActualHeight + ContentInternal.Margin.Bottom;
            containerTranslationStoryboardAnimation.To = 0;

            smokeLayerOpacityStoryboardAnimation.From = 0;
            smokeLayerOpacityStoryboardAnimation.To = 1;
        } else {
            containerTranslationStoryboardAnimation.EasingFunction.EasingMode = EasingMode.EaseIn;

            containerTranslationStoryboardAnimation.From = 0;
            containerTranslationStoryboardAnimation.To = ContentInternal.ActualHeight + ContentInternal.Margin.Bottom;

            smokeLayerOpacityStoryboardAnimation.From = 1;
            smokeLayerOpacityStoryboardAnimation.To = 0;
        }

        transitionStoryboard.Begin();
    }
}