using Model.Interfaces;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Windows.Storage;
using ZoneTree;
using ZoneTree.Comparers;
using ZoneTree.Serializers;

namespace Model.Database;

internal sealed class Store<T>: IDisposable
    where T : IDatabaseEntity {
    private static readonly string root = ApplicationData.Current.LocalFolder.Path;

    private IZoneTree<string, T>? tree;
    private IZoneTree<string, T> Tree => tree ?? throw new ObjectDisposedException(nameof(Store<>));
    private readonly IMaintainer maintainer;
    private readonly string accountId;
    private readonly string name;

    public Store(string accountId, string name, int maxItemCount, JsonTypeInfo<T> typeInfo) {
        this.accountId = accountId;
        this.name = name;

        var factory = new ZoneTreeFactory<string, T>()
            .SetDataDirectory(DataDirectory)
            .SetComparer(new IdComparer())
            .SetKeySerializer(new Utf8StringSerializer())
            .SetValueSerializer(new JsonValueSerializer(typeInfo))
            .SetIsDeletedDelegate((in key, in value) =>
                (value is null) ||
                (value.ExpiresAt < DateTimeOffset.UtcNow));
        if (maxItemCount > 0) {
            factory = factory.SetMutableSegmentMaxItemCount(maxItemCount);
        }

        tree = factory.OpenOrCreate();
        maintainer = tree.CreateMaintainer();
    }

    public bool TryGet(string id, [NotNullWhen(true)] out T? value) {
        return Tree.TryGet(id, out value) && (value is not null);
    }

    public bool Contains(string id) {
        return Tree.ContainsKey(id);
    }

    public void Upsert(string id, T value) {
        Tree.Upsert(id, value);
    }

    public bool Remove(string id) {
        return Tree.TryDelete(id, out _);
    }

    public string DataDirectory => Path.Combine(root, accountId, name);

    public IEnumerable<T> GetDescending(string? afterId = null) {
        using var iterator = Tree.CreateReverseIterator();
        if (afterId is null) {
            iterator.SeekFirst();
        } else {
            iterator.Seek(afterId);
            if (!iterator.Next()) {
                yield break;
            }
            // Seek is inclusive when the key exists.
            if (iterator.CurrentKey != afterId) {
                yield return iterator.CurrentValue;
            }
        }
        while (iterator.Next()) {
            yield return iterator.CurrentValue;
        }
    }

    public void Dispose() {
        maintainer.WaitForBackgroundThreads();
        maintainer.Dispose();
        Tree.Dispose();
        tree = null;
    }

    // Instance-assigned status ids sort chronologically by length, then ordinal.
    private sealed class IdComparer: IRefComparer<string> {
        public int Compare(in string x, in string y) {
            var result = x.Length.CompareTo(y.Length);
            return (result != 0) ? result : string.CompareOrdinal(x, y);
        }
    }

    private sealed class JsonValueSerializer(JsonTypeInfo<T> typeInfo): ISerializer<T> {
        public Memory<byte> Serialize(in T entry) => JsonSerializer.SerializeToUtf8Bytes(entry, typeInfo);
        public T Deserialize(Memory<byte> bytes) => JsonSerializer.Deserialize(bytes.Span, typeInfo)!;
    }
}

[JsonSerializable(typeof(Entities.Account), TypeInfoPropertyName = "DatabaseAccount")]
[JsonSerializable(typeof(Entities.Notification), TypeInfoPropertyName = "DatabaseNotification")]
[JsonSerializable(typeof(Entities.NotificationGroup), TypeInfoPropertyName = "DatabaseNotificationGroup")]
[JsonSerializable(typeof(Entities.Status), TypeInfoPropertyName = "DatabaseStatus")]
[JsonSerializable(typeof(Entities.Timeline))]
internal sealed partial class StoreJsonContext: JsonSerializerContext;
