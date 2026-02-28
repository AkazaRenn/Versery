using Windows.Security.Credentials;

namespace Model; 
public static class Credentials {
    private static readonly string resourceName = Windows.ApplicationModel.Package.Current.Id.FamilyName;

    public static void AddAccessToken(string userId, string token) {
        var vault = new PasswordVault();
        vault.Add(new PasswordCredential(resourceName, userId, token));
    }

    public static string GetAccessToken(string userId) {
        return Retrieve(userId);
    }

    public static void AddAppRegistration(string instance, string appJson) {
        var vault = new PasswordVault();
        vault.Add(new PasswordCredential(resourceName, instance, appJson));
    }

    public static string GetAppRegistration(string instance) {
        return Retrieve(instance);
    }

    private static string Retrieve(string userName) {
        var vault = new PasswordVault();
        try {
            var credential = vault.Retrieve(resourceName, userName);
            if (credential is not null) {
                credential.RetrievePassword();
                return credential.Password;
            } else {
                return string.Empty;
            }
        } catch (Exception) {
            return string.Empty;
        }
    }
}
