using CommunityToolkit.Mvvm.ComponentModel;
using Model;
using Model.Access;
using Model.Utilities;
using ViewModel.Interfaces;

namespace ViewModel.Controls;

public sealed partial class Timeline: ObservableObject, IPaginatedViewModel<Timeline, Model.Entities.Timeline> {
    private static readonly Client client = Model.Services.Get<Client>();
    private static readonly Dictionary<string, WeakReference<Timeline>> cache = [];

    private readonly Sentinel<string, Timeline> sentinel;
    private int version;

    public string Id { get; }
    public string PaginateId => Id;

    public Account? Reblogger { get; } = null;
    public Status Content { get; }

    private Timeline(Model.Entities.Timeline timeline, int version) {
        Id = timeline.Id;
        var status = client.GetStatus(Id)!;

        if (string.IsNullOrWhiteSpace(status.Reblog)) {
            Content = Status.Create(Id);
        } else {
            Reblogger = Account.Create(status.Account);
            Content = Status.Create(status.Reblog);
        }

        sentinel = new(Id, cache);
        this.version = version;
    }

    public void Update(int version) {
        // Content.Update(version);
        // Reblogger?.Update(version);

        if (this.version == version) {
            return;
        }

        this.version = version;
    }

    public static IEnumerable<Timeline> FromEntities(IEnumerable<Model.Entities.Timeline> timelines, int version) {
        timelines = timelines.ToCollection();
        for (int i = 0; i < timelines.Count(); i++) {
            yield return Create(timelines.ElementAt(i), version);
        }
    }

    public async void DownloadMedia() {
        Content.DownloadMedia();
        Reblogger?.DownloadAvatar();
    }

    public static Timeline Create(Model.Entities.Timeline timeline, int version) {
        lock (cache) {
            if ((!cache.TryGetValue(timeline.Id, out var reference)) ||
                (!reference.TryGetTarget(out var obj))) {
                obj = new(timeline, version);
                cache[timeline.Id] = new(obj);
            }
            return obj;
        }
    }
}