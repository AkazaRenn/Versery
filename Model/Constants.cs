using System.Globalization;
using Windows.ApplicationModel;
using Windows.System.UserProfile;

namespace Model;

public static class Constants {
    public static readonly string PackageName = Package.Current.Id.FamilyName;
    public static readonly string AppName = Package.Current.DisplayName;
    public static readonly PackageVersion AppVersion = Package.Current.Id.Version;
    public static readonly string AppVersionString = $"{AppVersion.Major}.{AppVersion.Minor}.{AppVersion.Build}.{AppVersion.Revision}";
    public const string ProjectLink = "https://github.com/AkazaRenn/Versery/";
    public static readonly Guid MsaAppId = new("bce54557-aa3a-4341-add6-b5e0cbb1cf77");

    public const int ItemsCountPerLoad = 40;

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
