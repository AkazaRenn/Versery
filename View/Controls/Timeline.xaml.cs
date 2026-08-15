using Microsoft.UI.Xaml.Controls;

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
    private string ReactorDisplayNameBeforeText => ViewModel?.Reblogger is null ? String.Empty : Localization.L("Controls_Timeline_ReactorDisplayName_Before/Text_Reblog");
    private string ReactorDisplayNameAfterText => ViewModel?.Reblogger is null ? String.Empty : Localization.L("Controls_Timeline_ReactorDisplayName_After/Text_Reblog");
}
