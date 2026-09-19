using Microsoft.UI;
using Microsoft.UI.Xaml.Media;

namespace View;

internal static class Brushes {
    public static readonly SolidColorBrush Transparent = new(Colors.Transparent);
    public static readonly SolidColorBrush TransparentBlackQuarter = new(Colors.Black) { Opacity = 0.25 };
    public static readonly SolidColorBrush TransparentBlackHalf = new(Colors.Black) { Opacity = 0.5 };
    public static readonly SolidColorBrush White = new(Colors.White);
    public static readonly LinearGradientBrush CollapsedStatusBrush = new() {
        StartPoint = new(0, 0.8),
        EndPoint = new(0, 1),
        GradientStops = [
            new GradientStop() { Color = Colors.White, Offset = 0 },
                new GradientStop() { Color = Colors.Transparent, Offset = 1 },
            ],
    };
}
