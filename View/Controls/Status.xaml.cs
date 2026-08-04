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

    private double GetAboveLineHeight(Thickness parentPadding, double parentHeight) {
        return parentPadding.Top + parentHeight / 2;
    }

    private Thickness GetAboveLineMargin(Thickness parentPadding) {
        return new(0, -parentPadding.Top, 0, 0);
    }

    private int GetBelowLineRowSpan(int RowDefinitionsCount) {
        return RowDefinitionsCount - 1;
    }

    private Thickness GetBelowLineMargin(Thickness parentPadding, double parentHeight) {
        return new(0, parentHeight / 2, 0, -parentPadding.Bottom);
    }

    private double GetAvatarButtonSize(object? obj, double parentWidth) {
        return obj is null ? parentWidth : parentWidth * 0.9;
    }

    private CornerRadius GetAvatarButtonCornerRadius(object? obj, double parentWidth) {
        return new(GetAvatarButtonSize(obj, parentWidth) / 2);
    }

    private double GetRebloggerAvatarSize(object? obj, double parentWidth) {
        return obj is null ? parentWidth : parentWidth * 0.5;
    }

    private CornerRadius GetRebloggerAvatarCornerRadius(object? obj, double parentWidth) {
        return new(GetRebloggerAvatarSize(obj, parentWidth) / 2);
    }
}
