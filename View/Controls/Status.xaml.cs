using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace View.Controls;

internal sealed partial class Status: Grid {
    public ViewModel.Controls.Status? ViewModel {
        get;
        set {
            if (field != value) {
                field = value;
                _ = DispatcherQueue.TryEnqueue(Bindings.Update);
            }
        }
    }

    public Status() {
        InitializeComponent();
    }

    private readonly double lineWidth = 4;
    private double AboveLineHeight => Padding.Top + RowDefinitions[1].Height.Value / 2;
    private Thickness AboveLineMargin => new(0, -Padding.Top, 0, 0);
    private int BelowLineRowSpan => RowDefinitions.Count - 1;
    private Thickness BelowLineMargin => new(0, RowDefinitions[1].Height.Value / 2, 0, -Padding.Bottom);
    private double AvatarButtonSize => ViewModel?.RebloggerId is null ? ColumnDefinitions[0].Width.Value : ColumnDefinitions[0].Width.Value * 0.9;
    private CornerRadius AvatarButtonCornerRadius => new(AvatarButtonSize / 2);
    private double RebloggerAvatarSize => ColumnDefinitions[0].Width.Value / 2;
    private CornerRadius RebloggerAvatarCornerRadius => new(RebloggerAvatarSize / 2);
}
