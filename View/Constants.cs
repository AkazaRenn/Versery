using Microsoft.UI;
using Microsoft.UI.Xaml.Media;

namespace View;

internal static class Constants {
    internal static class Brush {
        public static readonly SolidColorBrush Transparent = new(Colors.Transparent);
        public static readonly LinearGradientBrush CollapsedStatusBrush = new() {
            StartPoint = new(0, 0.8),
            EndPoint = new(0, 1),
            GradientStops = [
                new GradientStop() { Color = Colors.White, Offset = 0 },
                new GradientStop() { Color = Colors.Transparent, Offset = 1 },
            ],
        };
    }
}
