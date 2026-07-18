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
    public partial string Uri { get; set; }
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
        FollowedByGap = status.FollowedByGap ?? false;

        DownloadAvatar(account.AvatarUrl);
    }

    public async void LoadMore() {
        var timelines = await client.GetTimelineFromServer(Model.Enums.TimelineType.Home, Id);
        FollowedByGap = false;
        MoreLoaded?.Invoke(Index, timelines);
    }

    private async void DownloadAvatar(string url) {
        PosterAvatar = await Cache.Get(url);
    }
}
