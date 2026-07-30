using FluentIcons.Common;
using Microsoft.UI.Xaml.Controls;

namespace View.Controls.StatusComponents;

internal sealed partial class ReactButtons: Grid {
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
