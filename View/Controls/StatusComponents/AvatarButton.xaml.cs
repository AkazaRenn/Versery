using Microsoft.UI.Xaml.Controls;

namespace View.Controls.StatusComponents;

internal sealed partial class AvatarButton: Button {
    public ViewModel.Controls.StatusComponents.AvatarButton ViewModel {
        get;
        set {
            if (field != value) {
                field = value;
                _ = DispatcherQueue.TryEnqueue(Bindings.Update);
            }
        }
    } = new();

    public AvatarButton() {
        InitializeComponent();
    }
}
