using Model.Server.Entities;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Model;

public static class Extensions {
    extension(Server.Client client) {
        internal async Task<string> GetFullUserId() {
            var account = await client.Accounts.VerifyCredentials();
            var instance = await client.Instance.V2();
            return $"{account.Username}@{instance.Domain}";
        }
    }

    extension(Status serverStatus) {
        private IEnumerable<Status> Flattened {
            get {
                yield return serverStatus;
                if (serverStatus.Reblog is not null) {
                    foreach (var flattened in serverStatus.Reblog.Flattened) {
                        yield return flattened;
                    }
                }
                if (serverStatus.Quote?.QuotedStatus is not null) {
                    foreach (var flattened in serverStatus.Quote.QuotedStatus.Flattened) {
                        yield return flattened;
                    }
                }
            }
        }
    }

    extension(IEnumerable<Status> serverStatuses) {
        public IEnumerable<Status> Flattened {
            get {
                foreach (var status in serverStatuses) {
                    foreach (var flattened in status.Flattened) {
                        yield return flattened;
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
