using LiteDB;
using Mastonet;
using Mastonet.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace Model; 
public static class Extensions {
    extension(MastodonClient mastodonClient) {
        public async Task<string> GetFullUserId() {
            var account = await mastodonClient.GetCurrentUser();
            var instance = await mastodonClient.GetInstanceV2();
            return $"{account.UserName}@{instance.Domain}";
        }
    }

    extension(IEnumerable<Status> statuses) {
        public IEnumerable<Status> Flattened {
            get {
                foreach (var status in statuses) {
                    yield return status;
                    if (status.Reblog is not null) {
                        yield return status.Reblog;
                    }
                }
            }
        }
    }

    extension<T>(IEnumerable<T> enumerable) {
        public ICollection<T> ToCollection() {
            return enumerable is ICollection<T> collection ? collection : [.. enumerable];
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

    extension(string str) {
        public string Sha256 {
            get {
                byte[] hash = SHA256.HashData(Encoding.UTF8.GetBytes(str));
                return Convert.ToHexString(hash).ToLowerInvariant();
            }
        }
    }
}
