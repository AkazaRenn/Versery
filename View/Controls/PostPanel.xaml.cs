using FluentIcons.Common;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Text;
using View.Controls.Components;
using CommunityToolkit.WinUI.Controls;

namespace View.Controls;

internal sealed partial class PostPanel: SlideDialog {
    private readonly ViewModel.Controls.PostPanel viewModel = new();

    private void DismissButton_Tapped(object sender, TappedRoutedEventArgs e) {
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

    private UIElement? focusedElement = null;
    private void TextBox_GotFocus(object sender, RoutedEventArgs e) {
        if (sender is UIElement element) {
            focusedElement = element;
        }
    }
    private void ContentBox_Loaded(object sender, RoutedEventArgs args) {
        ContentBox.Focus(FocusState.Programmatic);
    }

    private void EmojiButton_Tapped(object sender, TappedRoutedEventArgs e) {
        EmojiPicker.IsOpen = !EmojiPicker.IsOpen;
    }
    private void SlideDialog_Loaded(object sender, RoutedEventArgs e) {
        viewModel.LoadInitialEmojis();
    }
    private void EmojiGridView_ContainerContentChanging(ListViewBase sender, ContainerContentChangingEventArgs args) {
        if (args.InRecycleQueue) {
            return;
        }
        if (args.Phase == 0) {
            viewModel.OnEmojiRealized(args.ItemIndex);
        }
    }
    private void EmojiPicker_Closed(TeachingTip sender, TeachingTipClosedEventArgs args) {
        focusedElement?.Focus(FocusState.Programmatic);
    }
    private void EmojiGridView_ItemClick(object sender, ItemClickEventArgs e) {
        if ((e.ClickedItem is ViewModel.Controls.PostPanel_Emoji emoji) &&
            (ContentBox.TextDocument is { } document)) {
            var selection = document.Selection;
            var hasSpaceBefore = (selection.StartPosition <= 0) ||
                document.GetRange(selection.StartPosition - 1, selection.StartPosition).Text == " ";
            var hasSpaceAfter = (selection.EndPosition < selection.StoryLength) &&
                document.GetRange(selection.EndPosition, selection.EndPosition + 1).Text == " ";

            selection.SetText(
                TextSetOptions.Unhide,
                $"{(hasSpaceBefore ? "" : " ")}:{emoji.ShortCode}:{(hasSpaceAfter ? "" : " ")}");
            selection.Collapse(false);
        }
    }

    private async void PostButton_Tapped(object sender, TappedRoutedEventArgs e) {
        if (ContentBox.TextDocument is { } document) {
            PostButton.IsEnabled = false;
            PostButtonProgressRing.Visibility = Visibility.Visible;
            PostButtonIcon.Visibility = Visibility.Collapsed;
            document.GetText(TextGetOptions.NoHidden, out var content);
            await viewModel.Post(content.Trim());
            PostButtonIcon.Visibility = Visibility.Visible;
            PostButtonProgressRing.Visibility = Visibility.Collapsed;
            PostButton.IsEnabled = true;
            HideGracefully();
        }
    }

    public PostPanel() {
        InitializeComponent();
        Bindings.Initialize();
    }
}