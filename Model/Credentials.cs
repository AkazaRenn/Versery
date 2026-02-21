using Windows.Security.Credentials;

namespace Model; 
public static class Credentials {
    private static readonly string resourceName = Windows.ApplicationModel.Package.Current.Id.FamilyName;

    public static bool AddToken(string token) {
        var vault = new PasswordVault();
        vault.Add(new PasswordCredential(resourceName, "@renn@not.iee.engineer", token));
        return true;
    }

    public static string GetToken(string id) {
        var vault = new PasswordVault();
        var credential = vault.Retrieve(resourceName, id);
        if (credential is not null) {
            credential.RetrievePassword();
            return credential.Password;
        } else {
            return string.Empty;
        }
    }
}
