using Microsoft.UI;
using Microsoft.UI.Composition;
using Microsoft.UI.Xaml.Media;
using System.Numerics;

namespace View.Utilities;

internal static partial class Brushes {
    public static readonly SolidColorBrush Transparent = new(Colors.Transparent);
    public static readonly SolidColorBrush TransparentBlack025 = new(Colors.Black) { Opacity = 0.25 };
    public static readonly SolidColorBrush Black = new(Colors.Black);
    public static readonly SolidColorBrush LimeGreen = new(Colors.LimeGreen);
    public static readonly SolidColorBrush SkyBlue = new(Colors.SkyBlue);
    public static readonly SolidColorBrush Orange = new(Colors.Orange);
    public static readonly SolidColorBrush White = new(Colors.White);

    public static SolidColorBrush Reply => SkyBlue;
    public static SolidColorBrush Reblog => LimeGreen;
    public static SolidColorBrush Favourite => Orange;

    public static FadeOutBrush CollapsedStatusOpacityMaskBrush => new(0.6f, 0.9f);
    public sealed partial class FadeOutBrush(float startPoint, float endPoint): XamlCompositionBrushBase {
        private CompositionColorGradientStop? opaqueStop;
        private CompositionColorGradientStop? transparentStop;

        protected override void OnConnected() {
            if (CompositionBrush is not null) {
                return;
            }

            var compositor = CompositionTarget.GetCompositorForCurrentThread();
            var brush = compositor.CreateLinearGradientBrush();
            brush.StartPoint = Vector2.Zero;
            brush.EndPoint = Vector2.UnitY;
            opaqueStop = compositor.CreateColorGradientStop(startPoint, Colors.White);
            transparentStop = compositor.CreateColorGradientStop(endPoint, Colors.Transparent);
            brush.ColorStops.Add(opaqueStop);
            brush.ColorStops.Add(transparentStop);
            CompositionBrush = brush;
        }

        protected override void OnDisconnected() {
            CompositionBrush?.Dispose();
            CompositionBrush = null;
            opaqueStop?.Dispose();
            opaqueStop = null;
            transparentStop?.Dispose();
            transparentStop = null;
        }

        public void MoveOut() {
            if ((opaqueStop is null) || (transparentStop is null) || (CompositionBrush is null)) {
                return;
            }

            var brush = CompositionBrush;
            var compositor = brush.Compositor;
            using var easing = CompositionEasingFunction.CreatePowerEasingFunction(compositor, CompositionEasingFunctionMode.Out, 3f);
            using var animation = compositor.CreateScalarKeyFrameAnimation();
            animation.Duration = Transitions.DefaultDuration;

            animation.InsertKeyFrame(0, opaqueStop.Offset);
            animation.InsertKeyFrame(1, 1, easing);
            opaqueStop.StartAnimation(nameof(CompositionColorGradientStop.Offset), animation);
            animation.InsertKeyFrame(0, transparentStop.Offset);
            transparentStop.StartAnimation(nameof(CompositionColorGradientStop.Offset), animation);
        }
    }
}
