using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;

namespace View.Controls;

internal sealed partial class PostPanel: ContentDialog {
    public ViewModel.Controls.PostPanel? ViewModel { get; set; } = null;

    public PostPanel() {
        InitializeComponent();
    }

    private void ContentDialog_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e) {
        // ContentDialog's smoke popup is the open root popup whose child is a bare Rectangle.
        var smoke = VisualTreeHelper.GetOpenPopupsForXamlRoot(XamlRoot)
            .Select(p => p.Child)
            .OfType<Rectangle>()
            .LastOrDefault();

        if (smoke is not null) {
            smoke.Scale = new(0);
            smoke.SizeChanged += Smoke_SizeChanged;

            Grid.Height = smoke.Height;
            Grid.Width = smoke.Width;

            Popup.XamlRoot = XamlRoot;
            Popup.IsOpen = true;
        }
    }

    private void Smoke_SizeChanged(object sender, SizeChangedEventArgs e) {
        Grid.Width = e.NewSize.Width;
        Grid.Height = e.NewSize.Height;
    }

    private void Dialog_Loaded(object sender, RoutedEventArgs e) {
        InitializeDialogTranslationStoryboard();
        DialogTranslateTransform.Y = Grid.Height;
    }

    private void Dialog_SizeChanged(object sender, SizeChangedEventArgs e) {
        if (e.PreviousSize.Height == 0) {
            AnimateDialogTranslation(0);
            SmokeLayerBackground.Opacity = 1;
        }
    }

    private readonly Storyboard DialogTranslationStoryboard = new();
    private readonly DoubleAnimation DialogTranslationStoryboardAnimation = new() {
        Duration = View.Transitions.DefaultDuration,
        EasingFunction = new CubicEase(),
    };
    private void InitializeDialogTranslationStoryboard() {
        DialogTranslationStoryboard.Children.Add(DialogTranslationStoryboardAnimation);
        Storyboard.SetTarget(DialogTranslationStoryboardAnimation, DialogTranslateTransform);
        Storyboard.SetTargetProperty(DialogTranslationStoryboardAnimation, "Y");
    }
    private void AnimateDialogTranslation(double targetTranslation) {
        DialogTranslationStoryboard.Stop();

        if (targetTranslation > 0) {
            DialogTranslationStoryboardAnimation.From = DialogTranslateTransform.Y;
            DialogTranslationStoryboardAnimation.To = 0;
            DialogTranslationStoryboardAnimation.EasingFunction.EasingMode = EasingMode.EaseOut;
        } else {
            DialogTranslationStoryboardAnimation.From = Dialog.ActualHeight;
            DialogTranslationStoryboardAnimation.To = 0;
            DialogTranslationStoryboardAnimation.EasingFunction.EasingMode = EasingMode.EaseIn;
        }

        DialogTranslationStoryboard.Begin();
    }

    private void Dialog_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e) {
        Hide();
        Popup.IsOpen = false;
    }
}