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
    public partial StatusComponents.AvatarButton AvatarButton { get; set; } = new();
    [ObservableProperty]
    public partial StatusComponents.PosterInfo PosterInfo { get; set; } = new();
    [ObservableProperty]
    public partial StatusComponents.ReactButtons ReactButtons { get; set; } = new();


    [ObservableProperty]
    public partial string? RebloggerId { get; set; } = null;
    [ObservableProperty]
    public partial string? RebloggerDisplayName { get; set; } = null;
    [ObservableProperty]
    public partial DateTime CreatedAt { get; set; } = DateTime.MinValue;
    [ObservableProperty]
    public partial Uri? Uri { get; set; } = null;
    [ObservableProperty]
    public partial string TextContent { get; set; } = string.Empty;
    [ObservableProperty]
    public partial bool FollowedByGap { get; set; } = false;

    public static async Task<Status> FromTimeline(Timeline timeline) {
        var obj = new Status() {
            Id = timeline.Id,
        };

        var status = await client.GetStatus(obj.Id) ?? throw new ArgumentNullException(null, $"Unable to get status {obj.Id}");
        var account = await client.GetAccount(status.AccountId) ?? throw new ArgumentNullException(null, $"Unable to get account {status.AccountId}");

        if (status.ReblogId != null) {
            _ = obj.DownloadRebloggerAvatar(account.Avatar);

            obj.AvatarButton.IsReblog = true;

            obj.RebloggerId = account.Id;
            obj.RebloggerDisplayName = account.DisplayName;

            status = await client.GetStatus(status.ReblogId) ?? throw new ArgumentNullException(null, $"Unable to get status {status.ReblogId}");
            account = await client.GetAccount(status.AccountId) ?? throw new ArgumentNullException(null, $"Unable to get account {status.AccountId}");
        }

        _ = obj.DownloadAvatar(account.Avatar);

        obj.AvatarButton.ContentPosterId = account.Id;

        obj.PosterInfo.AccountId = account.Id;
        obj.PosterInfo.DisplayName = account.DisplayName;
        obj.PosterInfo.AccountName = account.AccountName;

        obj.ReactButtons.Reblogged = status.Reblogged ?? false;
        obj.ReactButtons.Favourited = status.Favourited ?? false;
        obj.ReactButtons.Bookmarked = status.Bookmarked ?? false;

        obj.ContentId = status.Id;
        obj.CreatedAt = status.CreatedAt;
        obj.Uri = status.Uri;
        obj.TextContent = status.Content;
        obj.FollowedByGap = timeline.FollowedByGap;

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
            AvatarButton.ContentPosterAvatar = await Cache.Get(uri);
        }
    }

    private async Task DownloadRebloggerAvatar(Uri? uri) {
        if (uri is not null) {
            AvatarButton.RebloggerAvatar = await Cache.Get(uri);
        }
    }
}
