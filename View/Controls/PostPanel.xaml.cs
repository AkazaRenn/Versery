using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;
using View.Utilities;

namespace View.Controls;

internal sealed partial class PostPanel: ContentDialog {
    private readonly DialogService dialogService = Services.Get<DialogService>();

    public ViewModel.Controls.PostPanel? ViewModel { get; set; } = null;

    public PostPanel() {
        InitializeComponent();
        Bindings.Initialize();
        InitializeDialogTranslationStoryboard();
    }

    private void ContentDialog_Opened(ContentDialog sender, ContentDialogOpenedEventArgs args) {
        // ContentDialog's smoke popup is the open root popup whose child is a bare Rectangle.
        VisualTreeHelper.GetOpenPopupsForXamlRoot(XamlRoot)
            .Select(p => p.Child)
            .OfType<Rectangle>()
            .FirstOrDefault()?.Visibility = Visibility.Collapsed;

        SmokeLayer.Height = XamlRoot.Size.Height;
        SmokeLayer.Width = XamlRoot.Size.Width;
        DialogTranslateTransform.Y = SmokeLayer.Height;

        Popup.XamlRoot = XamlRoot;
        Popup.IsOpen = true;
    }

    private void Dialog_SizeChanged(object sender, SizeChangedEventArgs e) {
        if (e.PreviousSize.Height == 0) {
            AnimateDialogTranslation(true);
            SmokeLayerBackground.Opacity = 1;
        }
    }

    private async void Dialog_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e) {
        AnimateDialogTranslation(false);
        SmokeLayerBackground.Opacity = 0;
    }

    private void SmokeLayer_Loaded(object sender, RoutedEventArgs e) {
        XamlRoot.Changed -= XamlRoot_Changed;
        XamlRoot.Changed += XamlRoot_Changed;
    }

    private void SmokeLayer_Unloaded(object sender, RoutedEventArgs e) {
        XamlRoot.Changed -= XamlRoot_Changed;
    }

    private void XamlRoot_Changed(XamlRoot sender, XamlRootChangedEventArgs args) {
        SmokeLayer.Height = sender.Size.Height;
        SmokeLayer.Width = sender.Size.Width;
    }

    private readonly Storyboard DialogTranslationStoryboard = new();
    private readonly DoubleAnimation DialogTranslationStoryboardAnimation = new() {
        Duration = Utilities.Transitions.DefaultDuration,
        EasingFunction = new CubicEase(),
    };
    private void InitializeDialogTranslationStoryboard() {
        DialogTranslationStoryboardAnimation.Completed += DialogTranslationStoryboardAnimation_Completed;
        DialogTranslationStoryboard.Children.Add(DialogTranslationStoryboardAnimation);
        Storyboard.SetTarget(DialogTranslationStoryboardAnimation, DialogTranslateTransform);
        Storyboard.SetTargetProperty(DialogTranslationStoryboardAnimation, "Y");
    }
    private void DialogTranslationStoryboardAnimation_Completed(object? sender, object e) {
        if (DialogTranslateTransform.Y != 0) {
            Hide();
            Popup.IsOpen = false;
        }
    }
    private void AnimateDialogTranslation(bool enter) {
        DialogTranslationStoryboard.Stop();

        if (enter) {
            DialogTranslationStoryboardAnimation.From = Dialog.ActualHeight;
            DialogTranslationStoryboardAnimation.To = 0;
            DialogTranslationStoryboardAnimation.EasingFunction.EasingMode = EasingMode.EaseOut;
        } else {
            DialogTranslationStoryboardAnimation.From = 0;
            DialogTranslationStoryboardAnimation.To = Dialog.ActualHeight;
            DialogTranslationStoryboardAnimation.EasingFunction.EasingMode = EasingMode.EaseIn;
        }

        DialogTranslationStoryboard.Begin();
    }
}