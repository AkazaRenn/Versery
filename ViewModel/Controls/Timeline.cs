using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.Access;
using Model.Enumerations;
using Model.Utilities;

namespace ViewModel.Controls;

public sealed partial class Timeline: ObservableObject {
    private static readonly Client client = Model.Services.Get<Client>();
    private static readonly Dictionary<string, WeakReference<Timeline>> cache = [];

    private readonly Sentinel<string, Timeline> sentinel;

    public event Action<int, IEnumerable<Model.Entities.Timeline>>? MoreLoaded;

    public string Id { get; }
    public int Index { get; set; } = -1;

    public Status.Account? Reblogger { get; } = null;
    public Status Content { get; }

    [ObservableProperty]
    public partial bool FollowedByGap { get; set; } = false;

    private Timeline(Model.Entities.Timeline timeline) {
        Id = timeline.Id;
        var status = client.GetStatus(Id)!;

        if (status.ReblogId == null) {
            Content = Status.Create(Id);
        } else {
            Reblogger = Status.Account.Create(status.AccountId);
            Content = Status.Create(status.ReblogId);
        }
        FollowedByGap = timeline.FollowedByGap;

        sentinel = new(Id, cache);
    }

    internal static IEnumerable<Timeline> FromTimelines(IEnumerable<Model.Entities.Timeline> timelines) {
        timelines = timelines.ToCollection();
        for (int i = 0; i < timelines.Count(); i++) {
            yield return Create(timelines.ElementAt(i));
        }
    }

    [RelayCommand]
    public async Task LoadMore() {
        var timelines = await client.GetTimelineFromServer(TimelineType.Home, Id);
        FollowedByGap = false;
        MoreLoaded?.Invoke(Index, timelines);
    }

    internal async Task DownloadMedias() {
        _ = Content.DownloadMedias();
        if (Reblogger?.Avatar is null && Reblogger?.AvatarRemote is not null) {
            Reblogger.Avatar = await Cache.Get(Reblogger.AvatarRemote);
        }
    }

    internal static Timeline Create(Model.Entities.Timeline timeline) {
        lock (cache) {
            if ((!cache.TryGetValue(timeline.Id, out var reference)) ||
                (!reference.TryGetTarget(out var obj))) {
                obj = new(timeline);
                reference = new(obj);
                cache[timeline.Id] = reference;
            }
            return obj;
        }
    }
}