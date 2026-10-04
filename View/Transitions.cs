using Microsoft.UI.Xaml;

namespace View;

internal static class Transitions {
    public static readonly ScalarTransition ScalarTransition = new();
    public static readonly TimeSpan DefaultDuration = ScalarTransition.Duration;

    public static readonly Vector3Transition Vector3Transition = new() {
        Duration = DefaultDuration,
    };
}
