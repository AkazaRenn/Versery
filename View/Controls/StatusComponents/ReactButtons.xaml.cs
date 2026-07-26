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

    private FontIcon GetFavouriteButtonIcon(bool favourited) {
        if (favourited) {
            return new FontIcon {
                Glyph = "\uE735",
            };
        } else {
            return new FontIcon {
                Glyph = "\uE734",
            };
        }
    }
}
