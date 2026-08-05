using LiteDB;
using Model.Server.Entities;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Model;

public static class Extensions {
    extension(Server.Client client) {
        public async Task<string> GetFullUserId() {
            var account = await client.Accounts.VerifyCredentials();
            var instance = await client.Instance.V2();
            return $"{account.Username}@{instance.Domain}";
        }
    }

    extension(IEnumerable<Status> serverStatuses) {
        public IEnumerable<Status> Flattened {
            get {
                foreach (var status in serverStatuses) {
                    yield return status;
                    if (status.Reblog is not null) {
                        yield return status.Reblog;
                    }
                    if (status.Quote?.QuotedStatus is not null) {
                        yield return status.Quote.QuotedStatus;
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

    extension(HttpStatusCode httpStatusCode) {
        public bool Retriable {
            get => httpStatusCode switch {
                HttpStatusCode.RequestTimeout => true,      // 408
                (HttpStatusCode)429 => true,                // Too Many Requests
                HttpStatusCode.InternalServerError => true, // 500
                HttpStatusCode.BadGateway => true,          // 502
                HttpStatusCode.ServiceUnavailable => true,  // 503
                HttpStatusCode.GatewayTimeout => true,      // 504
                _ => false
            };
        }
    }
}
