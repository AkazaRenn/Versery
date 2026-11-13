using System.Globalization;
using Windows.ApplicationModel;
using Windows.System.UserProfile;

namespace Model;

public static class Constants {
    public static string PackageName { get; } = Package.Current.Id.FamilyName;
    public static string AppName { get; } = Package.Current.DisplayName;
    public static PackageVersion AppVersion { get; } = Package.Current.Id.Version;
    public static string AppVersionString { get; } = $"{AppVersion.Major}.{AppVersion.Minor}.{AppVersion.Build}.{AppVersion.Revision}";
    public const string ProjectLink = "https://github.com/AkazaRenn/Versery/";

    public const int StatusesCountPerLoad = 40;

    private static readonly Lazy<CultureInfo[]> languageList = new(BuildLanguageList);
    public static CultureInfo[] LanguageList => languageList.Value;
    private static CultureInfo[] BuildLanguageList() {
        var preferredLanguageOrder = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
        foreach (var languageTag in GlobalizationPreferences.Languages) {
            var code = CultureInfo.GetCultureInfo(languageTag).TwoLetterISOLanguageName;
            if ((code.Length == 2) && (code != "iv")) {
                preferredLanguageOrder.TryAdd(code, preferredLanguageOrder.Count);
            }
        }
        var languages = CultureInfo.GetCultures(CultureTypes.NeutralCultures)
            .Select(culture => culture.TwoLetterISOLanguageName)
            .Where(code => code.Length == 2 && code != "iv")
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .Select(CultureInfo.GetCultureInfo)
            .OrderBy(culture => preferredLanguageOrder.GetValueOrDefault(culture.Name, int.MaxValue))
            .ThenBy(culture => culture.NativeName, StringComparer.CurrentCultureIgnoreCase);
        return [.. languages];
    }
}
