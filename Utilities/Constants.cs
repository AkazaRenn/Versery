namespace Utilities; 
public static class Constants {
    public static string PackageName => Windows.ApplicationModel.Package.Current.Id.FamilyName;
    public static string AppName => Windows.ApplicationModel.Package.Current.DisplayName;
    public static string ProjectLink => "https://github.com/AkazaRenn/Versery/";

    public static int StatusesCountPerLoad => 20;
}
