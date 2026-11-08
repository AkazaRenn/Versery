using FluentIcons.Common;
using View.Controls.Components;
using ViewModel.Controls;

namespace View.Controls;

internal sealed partial class PostPanel: SlideDialog {
    private readonly ViewModel.Controls.PostPanel viewModel = new();

    private void DismissButton_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e) {
        HideGracefully();
    }

    internal Icon GetVisibilityIcon(object visibility) {
        if (viewModel.IsVisibilityUnlisted) {
            return Icon.LockOpen;
        } else if (viewModel.IsVisibilityPrivate) {
            return Icon.LockClosed;
        } else if (viewModel.IsVisibilityDirect) {
            return Icon.Mail;
        } else {
            return Icon.Globe;
        }
    }

    private void EmojiButton_Tapped(object sender, Microsoft.UI.Xaml.Input.TappedRoutedEventArgs e) {
        EmojiPicker.IsOpen = !EmojiPicker.IsOpen;
    }
    private void EmojiGridView_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e) {
        if (viewModel.Emojis.Count == 0) {
            viewModel.LoadMoreEmojis();
        }
    }
    private void EmojiGridView_ContainerContentChanging(Microsoft.UI.Xaml.Controls.ListViewBase sender, Microsoft.UI.Xaml.Controls.ContainerContentChangingEventArgs args) {
        if (args.InRecycleQueue) {
            return;
        }

        if (args.Phase == 0) {
            viewModel.OnEmojiRealized(args.ItemIndex);
        }
    }

    public PostPanel() {
        InitializeComponent();
        Bindings.Initialize();
    }
}