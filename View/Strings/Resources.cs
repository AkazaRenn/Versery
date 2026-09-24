using Microsoft.Windows.ApplicationModel.Resources;

namespace View.Strings;

internal static class Resources {
    static readonly Type type = typeof(Resources);
    static readonly ResourceLoader resourceLoader = new("resources.pri", $"{type.Namespace!.Split('.')[0]}/{type.Name}");

    internal static string L(string key) {
        return resourceLoader.GetString(key);
    }
}
