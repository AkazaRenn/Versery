using Microsoft.UI.Xaml.Controls;

namespace View.Controls.StatusComponents;

internal sealed partial class PosterInfo: StackPanel {
    public ViewModel.Controls.StatusComponents.PosterInfo ViewModel {
        get;
        set {
            if (field != value) {
                field = value;
                Bindings.Update();
            }
        }
    } = new();

    public PosterInfo() {
        InitializeComponent();
    }

    private double GetIdFontSize(double referenceFontSize) {
        return referenceFontSize * 0.8;
    }
}
