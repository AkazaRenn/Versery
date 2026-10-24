using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Windows.Storage;
using ZoneTree;
using ZoneTree.Comparers;
using ZoneTree.Serializers;

namespace Model.Database;

internal sealed class Store<T>: IDisposable {
    private static readonly string root = ApplicationData.Current.LocalFolder.Path;

    private readonly IZoneTree<string, T> tree;
    private readonly IMaintainer maintainer;

    public Store(string accountId, string name, JsonTypeInfo<T> typeInfo) {
        tree = new ZoneTreeFactory<string, T>()
            .SetDataDirectory(Path.Combine(root, accountId, name))
            .SetComparer(new IdComparer())
            .SetKeySerializer(new Utf8StringSerializer())
            .SetValueSerializer(new JsonValueSerializer(typeInfo))
            .OpenOrCreate();
        maintainer = tree.CreateMaintainer();
        maintainer.EnableJobForCleaningInactiveCaches = true;
    }

    public bool TryGet(string id, [NotNullWhen(true)] out T? value) {
        return tree.TryGet(id, out value) && (value is not null);
    }

    public bool Contains(string id) {
        return tree.ContainsKey(id);
    }

    public void Upsert(string id, T value) {
        tree.Upsert(id, value);
    }

    public IEnumerable<T> GetDescending(string? afterId = null) {
        using var iterator = tree.CreateReverseIterator();
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
        tree.Dispose();
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

[JsonSerializable(typeof(Entities.Account))]
[JsonSerializable(typeof(Entities.Status))]
[JsonSerializable(typeof(Entities.Timeline))]
internal sealed partial class StoreJsonContext: JsonSerializerContext;
