using Microsoft.UI.Xaml;

namespace View; 
internal static class Converters {
    public static bool NullableObjectToBoolReversed(object? value) {
        return value != null;
    }

    public static Visibility NullableObjectToVisibility(object? value) {
        return value != null ? Visibility.Visible : Visibility.Collapsed;
    }
}
