using Microsoft.UI.Xaml;

namespace View;

internal static class Converters {
    public static bool IsNull(object? value) {
        return value is null;
    }

    public static bool IsNotNull(object? value) {
        return value is not null;
    }

    public static bool StringIsNotNullOrEmpty(string? value) {
        return !string.IsNullOrEmpty(value);
    }

    public static Visibility BoolToVisibility(bool value) {
        return value ? Visibility.Visible : Visibility.Collapsed;
    }

    public static Visibility BoolToVisibilityReversed(bool value) {
        return BoolToVisibility(!value);
    }

    public static Visibility NullableObjectToVisibility(object? value) {
        return BoolToVisibility(IsNotNull(value));
    }
}
