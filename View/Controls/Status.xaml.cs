using FluentIcons.Common;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using View.Interfaces;

namespace View.Controls;

internal sealed partial class Status: Grid {
    private Window? window;

    public ViewModel.Controls.Status? ViewModel {
        get;
        set {
            if (field != value) {
                field = value;
                if (DispatcherQueue.HasThreadAccess) {
                    Bindings.Update();
                } else {
                    _ = DispatcherQueue.TryEnqueue(Bindings.Update);
                }
            }
        }
    }

    public Status() {
        InitializeComponent();
    }

    private void Grid_Loaded(object sender, RoutedEventArgs e) {
        if ((window is null) && (Application.Current is IWindowHelper windowHelper)) {
            if (windowHelper.TryGetWindow(this, out window)) {
                window?.Activated -= Window_Activated;
                window?.Activated += Window_Activated;
            }
        }
    }

    private void Grid_Unloaded(object sender, RoutedEventArgs e) {
        window?.Activated -= Window_Activated;
    }

    private void Window_Activated(object sender, WindowActivatedEventArgs args) {
        switch (args.WindowActivationState) {
        case WindowActivationState.Deactivated:
            PosterAvatarBitmapImage?.Stop();
            RebloggerAvatarBitmapImage?.Stop();
            break;
        default:
            PosterAvatarBitmapImage?.Play();
            RebloggerAvatarBitmapImage?.Play();
            break;
        }
    }

    private bool LoadAboveLine => false;
    private bool LoadBelowLine => false;
    private double LineWidth => 4;
    private double AboveLineHeight => Padding.Top + RowDefinitions[1].Height.Value / 2;
    private Thickness AboveLineMargin => new(0, -Padding.Top, 0, 0);
    private int BelowLineRowSpan => RowDefinitions.Count - 1;
    private Thickness BelowLineMargin => new(0, RowDefinitions[1].Height.Value / 2, 0, -Padding.Bottom);
    private double AvatarButtonSize => ViewModel?.Reblogger?.Id is null ? ColumnDefinitions[0].Width.Value : ColumnDefinitions[0].Width.Value * 0.9;
    private CornerRadius AvatarButtonCornerRadius => new(AvatarButtonSize / 2);
    private double RebloggerAvatarSize => ColumnDefinitions[0].Width.Value / 2;
    private CornerRadius RebloggerAvatarCornerRadius => new(RebloggerAvatarSize / 2);
    private double PosterIdFontSize => DisplayNameTextBlock.FontSize * 0.85;

    private bool LoadMediaAttachments => ViewModel?.Content.MediaAttachments.Count > 0;
    private int MediaAttachment0ColumnSpan => ViewModel?.Content.MediaAttachments.Count > 1 ? 1 : 2;
    private int MediaAttachment0RowSpan => ViewModel?.Content.MediaAttachments.Count > 3 ? 1 : 2;
    private Uri? MediaAttachment0Source => ViewModel?.Content.MediaAttachments.ElementAtOrDefault(0);
    private int MediaAttachment1RowSpan => ViewModel?.Content.MediaAttachments.Count > 2 ? 1 : 2;
    private Uri? MediaAttachment1Source => ViewModel?.Content.MediaAttachments.ElementAtOrDefault(1);
    private int MediaAttachment2Row => ViewModel?.Content.MediaAttachments.Count > 3 ? 0 : 1;
    private Uri? MediaAttachment2Source => ViewModel?.Content.MediaAttachments.ElementAtOrDefault(2);
    private Uri? MediaAttachment3Source => ViewModel?.Content.MediaAttachments.ElementAtOrDefault(3);

    private bool LoadReactButtons => true;
    private Brush ReactButtonBackground => Utilities.Brush.Transparent;
    private Brush ReactButtonBorderBrush => Utilities.Brush.Transparent;
    private Thickness ReactButtonBorderThickness => new(0, 0, 0, 0);
    private Thickness ReactButtonPadding => new(4, 4, 4, 4);
    private double ReactButtonFontSize => 16;
    private Icon GetReplyIcon(bool hasReplies) => hasReplies ? Icon.ArrowReplyAll : Icon.ArrowReply;
    private Icon GetReblogIcon(bool canBeReblogged) => canBeReblogged ? Icon.ArrowRepeatAll : Icon.ArrowRepeatAllOff;
    private IconVariant GetFavouriteIconVariant(bool favourited) => favourited ? IconVariant.Color : IconVariant.Regular;

    private Thickness LoadMoreButtonBorderThickness => new(0, BorderThickness.Bottom, 0, 0);
    private Thickness LoadMoreButtonMargin => new(-Padding.Left, Padding.Bottom, -Padding.Right, -Padding.Bottom);
}
