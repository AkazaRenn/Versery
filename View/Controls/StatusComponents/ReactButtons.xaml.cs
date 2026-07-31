using FluentIcons.Common;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;

namespace View.Controls.StatusComponents;

internal sealed partial class ReactButtons: Grid {
    private readonly Brush background = new SolidColorBrush(Colors.Transparent);
    private readonly Brush borderBrush = new SolidColorBrush(Colors.Transparent);
    private readonly Thickness borderThickness = new(0, 0, 0, 0);
    private readonly Thickness padding = new(4, 4, 4, 4);

    public ViewModel.Controls.StatusComponents.ReactButtons ViewModel {
        get;
        set {
            if (field != value) {
                field = value;
                _ = DispatcherQueue.TryEnqueue(Bindings.Update);
            }
        }
    } = new();

    public ReactButtons() {
        InitializeComponent();
    }

    private Icon GetReplyIcon(bool hasReplies) {
        return hasReplies ? Icon.ArrowReplyAll : Icon.ArrowReply;
    }

    private Icon GetReblogIcon(bool canBeReblogged) {
        return canBeReblogged ? Icon.ArrowRepeatAll : Icon.ArrowRepeatAllOff;
    }

    private IconVariant GetFavouriteIconVariant(bool favourited) {
        return favourited ? IconVariant.Color : IconVariant.Regular;
    }
}
