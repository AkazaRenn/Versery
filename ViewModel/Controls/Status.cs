using CommunityToolkit.Mvvm.ComponentModel;
using Model.Access;
using Model.Entities;

namespace ViewModel.Controls;

public sealed partial class Status: ObservableObject {
    private readonly Client client = Utilities.Services.Get<Client>();

    public event Action<int, IEnumerable<Model.Entities.Timeline>>? MoreLoaded;

    public string Id { get; }
    public string ContentId { get; }
    public int Index { get; set; }

    [ObservableProperty]
    public partial Uri? RebloggerAvatar { get; set; }
    [ObservableProperty]
    public partial string? RebloggerId { get; set; }
    [ObservableProperty]
    public partial string? RebloggerDisplayName { get; set; }
    [ObservableProperty]
    public partial Uri? PosterAvatar { get; set; }
    [ObservableProperty]
    public partial string PosterId { get; set; }
    [ObservableProperty]
    public partial string PosterDisplayName { get; set; }
    [ObservableProperty]
    public partial DateTime CreatedAt { get; set; }
    [ObservableProperty]
    public partial Uri? Uri { get; set; }
    [ObservableProperty]
    public partial string TextContent { get; set; }
    [ObservableProperty]
    public partial bool Reblogged { get; set; }
    [ObservableProperty]
    public partial bool Favourited { get; set; }
    [ObservableProperty]
    public partial bool Bookmarked { get; set; }
    [ObservableProperty]
    public partial bool FollowedByGap { get; set; }

    public Status(Timeline timeline) {
        Id = timeline.Id;
        var status = client.GetStatus(Id) ?? throw new ArgumentNullException(null, $"Unable to get status {Id}");
        var account = client.GetAccount(status.AccountId) ?? throw new ArgumentNullException(null, $"Unable to get account {status.AccountId}");

        if (status.ReblogId != null) {
            RebloggerAvatar = account.Avatar;
            RebloggerId = account.Id;
            RebloggerDisplayName = account.DisplayName;
            status = client.GetStatus(status.ReblogId) ?? throw new ArgumentNullException(null, $"Unable to get status {status.ReblogId}");
            account = client.GetAccount(status.AccountId) ?? throw new ArgumentNullException(null, $"Unable to get account {status.AccountId}");
        }

        ContentId = status.Id;
        PosterId = account.AccountName;
        PosterDisplayName = account.DisplayName;
        CreatedAt = status.CreatedAt;
        Uri = status.Uri;
        TextContent = status.Content;
        Reblogged = status.Reblogged ?? false;
        Favourited = status.Favourited ?? false;
        Bookmarked = status.Bookmarked ?? false;
        FollowedByGap = timeline.FollowedByGap;

        _ = DownloadAvatar(account.Avatar);
    }

    public async void LoadMore() {
        var timelines = await client.GetTimelineFromServer(Model.Enums.TimelineType.Home, Id);
        FollowedByGap = false;
        MoreLoaded?.Invoke(Index, timelines);
    }

    private async Task DownloadAvatar(Uri? uri) {
        if (uri != null) {
            PosterAvatar = await Cache.Get(uri);
        }
    }
}
