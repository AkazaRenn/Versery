using Microsoft.Windows.ApplicationModel.Resources;

namespace View;

internal static class Localization {
    static readonly ResourceLoader resourceLoader = new("resources.pri", $"{typeof(Localization).Namespace}/Resources");

    internal static string L(string key) {
        return resourceLoader.GetString(key);
    }
}
