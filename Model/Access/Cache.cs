using System.Collections.Concurrent;
using System.Security.Cryptography;
using System.Text;
using Windows.Storage;

namespace Model.Access;
public sealed class Cache {
    private static readonly StorageFolder cacheFolder = ApplicationData.Current.TemporaryFolder;
    private static readonly HttpClient httpClient = Services.Get<HttpClient>();
    private static readonly ConcurrentDictionary<string, Task<Uri?>> cacheTasks = new();

    public static Task<Uri?> Get(string url) {
        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri? uri)) {
            return Task.FromResult<Uri?>(null);
        }
        return Get(uri);
    }

    public static Task<Uri?> Get(Uri uri) {
        if (uri.IsFile) {
            return Task.FromResult<Uri?>(uri);
        } else {
            return cacheTasks.GetOrAdd(uri.AbsoluteUri.Sha256, hash => GetInternal(uri, hash));
        }
    }

    private static async Task<Uri?> GetInternal(Uri uri, string hash) {
        try {
            var existing = await cacheFolder.TryGetItemAsync(hash);
            if (existing is StorageFile existingFile) {
                // Update the modification time to avoid from being deleted by Windows
                File.SetLastWriteTime(existingFile.Path, DateTime.Now);
                return new Uri(existingFile.Path);
            }

            var response = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead);
            if (!response.IsSuccessStatusCode) {
                return null;
            }

            await using var input = await response.Content.ReadAsStreamAsync();
            StorageFile file = await cacheFolder.CreateFileAsync($"{hash}.tmp", CreationCollisionOption.ReplaceExisting);
            await using var output = await file.OpenStreamForWriteAsync();

            await input.CopyToAsync(output);
            await file.RenameAsync(hash, NameCollisionOption.ReplaceExisting);

            return new Uri(file.Path);
        } catch (Exception) {
            return null;
        } finally {
            cacheTasks.TryRemove(hash, out _);
        }
    }
}
