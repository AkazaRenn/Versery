using Microsoft.UI.Xaml.Controls;
using View.Strings;

namespace View.Controls;

internal sealed partial class Timeline: Grid {
    public ViewModel.Controls.Timeline? ViewModel {
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

    public Timeline() {
        InitializeComponent();
    }

    private double PosterAvatarScale => ViewModel?.Reblogger is null ? 1 : 0.9;
}
