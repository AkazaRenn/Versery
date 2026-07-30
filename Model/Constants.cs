namespace Model; 
public static class Constants {
    public static string PackageName => Windows.ApplicationModel.Package.Current.Id.FamilyName;
    public static string AppName => Windows.ApplicationModel.Package.Current.DisplayName;
    public const string ProjectLink = "https://github.com/AkazaRenn/Versery/";

    public const int StatusesCountPerLoad = 30;
}
