using Windows.ApplicationModel;

namespace Model;

public static class Constants {
    public static string PackageName { get; } = Package.Current.Id.FamilyName;
    public static string AppName { get; } = Package.Current.DisplayName;
    public static PackageVersion AppVersion { get; } = Package.Current.Id.Version;
    public static string AppVersionString { get; } = $"{AppVersion.Major}.{AppVersion.Minor}.{AppVersion.Build}.{AppVersion.Revision}";
    public const string ProjectLink = "https://github.com/AkazaRenn/Versery/";

    public const int StatusesCountPerLoad = 40;
}
