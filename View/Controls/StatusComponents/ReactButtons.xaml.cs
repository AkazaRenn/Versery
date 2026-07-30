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
    private readonly IconSize iconSize = IconSize.Size16;

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

    private Icon GetReplyIcon(long repliesCount) {
        return repliesCount > 0 ? Icon.ArrowReplyAll : Icon.ArrowReply;
    }

    private Icon GetReblogIcon(Model.Enumerations.Visibility visibility) {
        return visibility < Model.Enumerations.Visibility.Private ? Icon.ArrowRepeatAll : Icon.LockClosed;
    }

    private IconVariant GetFavouriteIconVariant(bool favourited) {
        return favourited ? IconVariant.Color : IconVariant.Regular;
    }
}
