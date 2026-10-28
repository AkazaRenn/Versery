using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace View.Utilities;

internal static class Brushes {
    public static readonly SolidColorBrush Transparent = new(Colors.Transparent);
    public static readonly SolidColorBrush TransparentBlack025 = new(Colors.Black) { Opacity = 0.25 };
    public static readonly SolidColorBrush White = new(Colors.White);
    public static readonly LinearGradientBrush CollapsedStatusBrush = new() {
        StartPoint = new(0, 0.6),
        EndPoint = new(0, 0.9),
        GradientStops = [
            new GradientStop() { Color = Colors.White, Offset = 0 },
            new GradientStop() { Color = Colors.Transparent, Offset = 1 },
        ],
    };
}
