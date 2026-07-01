using CommunityToolkit.Mvvm.ComponentModel;
using Model.Access;
using Model.Entities;

namespace ViewModel.Controls;

public sealed partial class Status: ObservableObject {
    private readonly Client client = Utilities.Services.Get<Client>();

    public event Action<int, IEnumerable<Model.Entities.Timeline>>? MoreLoaded;

    public string Id { get; }
    public int Index { get; set; }

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
        var status = client.GetStatus(timeline.Id);
        if (status is null) {
            throw new ArgumentNullException(null, nameof(status));
        }
        var account = client.GetAccount(status.AccountId);
        if (account is null) {
            throw new ArgumentNullException(null, nameof(account));
        }

        Id = timeline.Id;

        PosterId = account.AccountName;
        PosterDisplayName = account.DisplayName;
        CreatedAt = status.CreatedAt;
        Uri = status.Uri;
        TextContent = status.Content;
        Reblogged = status.Reblogged;
        Favourited = status.Favourited;
        Bookmarked = status.Bookmarked;
        FollowedByGap = status.FollowedByGap;
    }

    public async void LoadMore() {
        var timelines = await client.GetTimelineFromServer(Model.Enums.TimelineType.Home, Id);
        FollowedByGap = false;
        MoreLoaded?.Invoke(Index, timelines);
    }
}
