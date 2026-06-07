using Realms;
using Windows.Storage;
using Windows.System;

namespace Model.DataPersistence; 

public class Status : RealmObject {
    [PrimaryKey]
    public string Id { get; set; } = string.Empty;
    public string AccountId { get; set; } = string.Empty;
}

public class User {
    private static readonly StorageFolder dataFolder = ApplicationData.Current.LocalFolder;

    public static async Task<User?> CreateAsync(string userId) {
        if (string.IsNullOrWhiteSpace(userId)) {
            return null;
        }

        var folder = await dataFolder.CreateFolderAsync("users", CreationCollisionOption.OpenIfExists);
        var config = new RealmConfiguration(System.IO.Path.Combine(folder.Path, $"{userId}.realm"));
        var userRealm = await Realm.GetInstanceAsync(config);

        return new User(userId, userRealm);
    } 

    public string Id { get;  init; }
    private Realm Realm { get; init; }

    private User(string id, Realm realm) {
        Id = id;
        Realm = realm;
    }

    public async Task<Status?> GetStatus(string statusId) {
        return Realm.Find<Status>(statusId);
    }
}
