using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.Access;
using Model.Enumerations;
using Model.Utilities;
using ViewModel.Interfaces;

namespace ViewModel.Controls;

public sealed partial class Timeline: ObservableObject, IPaginatedViewModel<Timeline, Model.Entities.Timeline> {
    private static readonly Client client = Model.Services.Get<Client>();
    private static readonly Dictionary<string, WeakReference<Timeline>> cache = [];

    private readonly Sentinel<string, Timeline> sentinel;
    private int version;

    public string Id { get; }

    public Status.Account? Reblogger { get; } = null;
    public Status Content { get; }

    [ObservableProperty]
    public partial bool FollowedByGap { get; set; } = false;

    private Timeline(Model.Entities.Timeline timeline, int version) {
        Id = timeline.Id;
        var status = client.GetStatus(Id)!;

        if (string.IsNullOrWhiteSpace(status.Reblog)) {
            Content = Status.Create(Id);
        } else {
            Reblogger = Status.Account.Create(status.Account);
            Content = Status.Create(status.Reblog);
        }
        FollowedByGap = timeline.FollowedByGap;

        sentinel = new(Id, cache);
        this.version = version;
    }

    public void Update(Model.Entities.Timeline entity, int version) {
        // Content.Update(version);
        // Reblogger?.Update(version);

        if (this.version == version) {
            return;
        }

        FollowedByGap = entity.FollowedByGap;
        this.version = version;
    }

    public static IEnumerable<Timeline> FromEntities(IEnumerable<Model.Entities.Timeline> timelines, int version) {
        timelines = timelines.ToCollection();
        for (int i = 0; i < timelines.Count(); i++) {
            yield return Create(timelines.ElementAt(i), version);
        }
    }

    [RelayCommand]
    public async Task LoadMore() {
        var timelines = await client.GetTimelineFromServer(TimelineType.Home, Id);
        FollowedByGap = false;
    }

    public async void DownloadMedia() {
        _ = Content.DownloadMedia();
        _ = Reblogger?.DownloadAvatar();
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