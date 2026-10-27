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

        Grid.Height = XamlRoot.Size.Height;
        Grid.Width = XamlRoot.Size.Width;
        DialogTranslateTransform.Y = Grid.Height;

        Popup.XamlRoot = XamlRoot;
        Popup.IsOpen = true;
    }

    private void Dialog_SizeChanged(object sender, SizeChangedEventArgs e) {
        if (e.PreviousSize.Height == 0) {
            AnimateDialogTranslation(0);
            SmokeLayerBackground.Opacity = 1;
        }
    }

    private async void Dialog_PointerPressed(object sender, Microsoft.UI.Xaml.Input.PointerRoutedEventArgs e) {
        _ = await dialogService.Show(
            new ContentDialog() {
                XamlRoot = XamlRoot,
                Title = "Save your work?",
                PrimaryButtonText = "Save",
                SecondaryButtonText = "Don't Save",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
            }
        );

        Hide();
        Popup.IsOpen = false;
    }

    private void Grid_Loaded(object sender, RoutedEventArgs e) {
        XamlRoot.Changed -= XamlRoot_Changed;
        XamlRoot.Changed += XamlRoot_Changed;
    }

    private void Grid_Unloaded(object sender, RoutedEventArgs e) {
        XamlRoot.Changed -= XamlRoot_Changed;
    }

    private void XamlRoot_Changed(XamlRoot sender, XamlRootChangedEventArgs args) {
        Grid.Height = sender.Size.Height;
        Grid.Width = sender.Size.Width;
    }

    private readonly Storyboard DialogTranslationStoryboard = new();
    private readonly DoubleAnimation DialogTranslationStoryboardAnimation = new() {
        Duration = Utilities.Transitions.DefaultDuration,
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
            DialogTranslationStoryboardAnimation.EasingFunction.EasingMode = EasingMode.EaseIn;
        } else {
            DialogTranslationStoryboardAnimation.From = Dialog.ActualHeight;
            DialogTranslationStoryboardAnimation.To = 0;
            DialogTranslationStoryboardAnimation.EasingFunction.EasingMode = EasingMode.EaseOut;
        }

        DialogTranslationStoryboard.Begin();
    }
}