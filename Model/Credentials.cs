using Windows.Security.Credentials;

namespace Model; 
public static class Credentials {
    private static readonly string resourceName = Windows.ApplicationModel.Package.Current.Id.FamilyName;

    public static void AddAccessToken(string userId, string token) {
        var vault = new PasswordVault();
        vault.Add(new PasswordCredential(resourceName, userId, token));
    }

    public static string GetAccessToken(string userId) {
        var vault = new PasswordVault();
        var credential = vault.Retrieve(resourceName, userId);
        if (credential is not null) {
            credential.RetrievePassword();
            return credential.Password;
        } else {
            return string.Empty;
        }
    }

    public static void AddAppRegistration(string instance, string appJson) {
        var vault = new PasswordVault();
        vault.Add(new PasswordCredential(resourceName, instance, appJson));
    }

    public static string GetAppRegistration(string instance) {
        var vault = new PasswordVault();
        var credential = vault.Retrieve(resourceName, instance);
        if (credential is not null) {
            credential.RetrievePassword();
            return credential.Password;
        } else {
            return string.Empty;
        }
    }
}
