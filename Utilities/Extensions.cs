using LiteDB;
using Mastonet;
using System.Diagnostics.CodeAnalysis;

namespace Utilities; 
public static class Extensions {
    extension(MastodonClient mastodonClient) {
        public async Task<string> GetFullUserId() {
            var account = await mastodonClient.GetCurrentUser();
            var instance = await mastodonClient.GetInstanceV2();
            return $"{account.UserName}@{instance.Domain}";
        }
    }

    extension<T>(ILiteCollection<T> collection) {
        public bool TryFindById(BsonValue id, [NotNullWhen(true)] out T? result) {
            result = collection.FindById(id);
            return result is not null;
        }

        public bool TryUpdate(BsonValue id, Action<T> update) {
            if (collection.TryFindById(id, out var item)) {
                update(item);
                return collection.Update(item);
            }
            return false;
        }
    }
}
