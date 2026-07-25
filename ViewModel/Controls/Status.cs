using CommunityToolkit.Mvvm.ComponentModel;
using Model.Access;
using Model.Entities;
using Utilities;

namespace ViewModel.Controls;

public sealed partial class Status: ObservableObject {
    private static readonly Client client = Utilities.Services.Get<Client>();

    public event Action<int, IEnumerable<Model.Entities.Timeline>>? MoreLoaded;

    public string Id { get; private set; } = string.Empty;
    public string ContentId { get; private set; } = string.Empty;
    public int Index { get; set; } = -1;


    [ObservableProperty]
    public partial Uri? RebloggerAvatar { get; set; } = null;
    [ObservableProperty]
    public partial string? RebloggerId { get; set; } = null;
    [ObservableProperty]
    public partial string? RebloggerDisplayName { get; set; } = null;
    [ObservableProperty]
    public partial Uri? PosterAvatar { get; set; } = null;
    [ObservableProperty]
    public partial string PosterId { get; set; } = string.Empty;
    [ObservableProperty]
    public partial string PosterDisplayName { get; set; } = string.Empty;
    [ObservableProperty]
    public partial DateTime CreatedAt { get; set; } = DateTime.MinValue;
    [ObservableProperty]
    public partial Uri? Uri { get; set; } = null;
    [ObservableProperty]
    public partial string TextContent { get; set; } = string.Empty;
    [ObservableProperty]
    public partial bool Reblogged { get; set; } = false;
    [ObservableProperty]
    public partial bool Favourited { get; set; } = false;
    [ObservableProperty]
    public partial bool Bookmarked { get; set; } = false;
    [ObservableProperty]
    public partial bool FollowedByGap { get; set; } = false;

    public static async Task<Status> FromTimeline(Timeline timeline) {
        var obj = new Status() {
            Id = timeline.Id,
        };

        var status = await client.GetStatus(obj.Id) ?? throw new ArgumentNullException(null, $"Unable to get status {obj.Id}");
        var account = await client.GetAccount(status.AccountId) ?? throw new ArgumentNullException(null, $"Unable to get account {status.AccountId}");

        if (status.ReblogId != null) {
            obj.RebloggerAvatar = account.Avatar;
            obj.RebloggerId = account.Id;
            obj.RebloggerDisplayName = account.DisplayName;
            status = await client.GetStatus(status.ReblogId) ?? throw new ArgumentNullException(null, $"Unable to get status {status.ReblogId}");
            account = await client.GetAccount(status.AccountId) ?? throw new ArgumentNullException(null, $"Unable to get account {status.AccountId}");
        }

        obj.ContentId = status.Id;
        obj.PosterId = account.AccountName;
        obj.PosterDisplayName = account.DisplayName;
        obj.CreatedAt = status.CreatedAt;
        obj.Uri = status.Uri;
        obj.TextContent = status.Content;
        obj.Reblogged = status.Reblogged ?? false;
        obj.Favourited = status.Favourited ?? false;
        obj.Bookmarked = status.Bookmarked ?? false;
        obj.FollowedByGap = timeline.FollowedByGap;

        _ = obj.DownloadAvatar(account.Avatar);
        return obj;
    }

    public static async Task<Status[]> FromTimelines(IEnumerable<Timeline> timelines) {
        timelines = timelines.ToCollection();
        var statuses = new Status[timelines.Count()];
        for (int i = 0; i < timelines.Count(); i++) {
            statuses[i] = await FromTimeline(timelines.ElementAt(i));
        }
        return statuses;
    }

    public async Task LoadMore() {
        var timelines = await client.GetTimelineFromServer(Model.Enums.TimelineType.Home, Id);
        FollowedByGap = false;
        MoreLoaded?.Invoke(Index, timelines);
    }

    private async Task DownloadAvatar(Uri? uri) {
        if (uri is not null) {
            PosterAvatar = await Cache.Get(uri);
        }
    }
}
