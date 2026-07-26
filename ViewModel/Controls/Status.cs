using CommunityToolkit.Mvvm.ComponentModel;
using Model;
using Model.Access;
using Model.Entities;

namespace ViewModel.Controls;

public sealed partial class Status: ObservableObject {
    private static readonly Client client = Model.Services.Get<Client>();

    public event Action<int, IEnumerable<Model.Entities.Timeline>>? MoreLoaded;

    public string Id { get; }
    public string ContentId { get; } = string.Empty;
    public int Index { get; set; } = -1;

    [ObservableProperty]
    public partial StatusComponents.AvatarButton AvatarButton { get; set; } = new();
    [ObservableProperty]
    public partial StatusComponents.PosterInfo PosterInfo { get; set; } = new();
    [ObservableProperty]
    public partial StatusComponents.ReactButtons ReactButtons { get; set; } = new();
    [ObservableProperty]
    public partial RichTextRenderer.Html Html { get; set; } = new();


    [ObservableProperty]
    public partial string? RebloggerId { get; set; } = null;
    [ObservableProperty]
    public partial string? RebloggerDisplayName { get; set; } = null;
    [ObservableProperty]
    public partial DateTime CreatedAt { get; set; } = DateTime.MinValue;
    [ObservableProperty]
    public partial Uri? Uri { get; set; } = null;
    [ObservableProperty]
    public partial bool FollowedByGap { get; set; } = false;

    public Status(Timeline timeline) {
        Id = timeline.Id;

        var status = client.GetStatus(Id)!;
        var account = client.GetAccount(status.AccountId)!;

        if (status.ReblogId != null) {
            _ = DownloadRebloggerAvatar(account.Avatar);

            AvatarButton.IsReblog = true;

            RebloggerId = account.Id;
            RebloggerDisplayName = account.DisplayName;

            status = client.GetStatus(status.ReblogId)!;
            account = client.GetAccount(status.AccountId)!;
        }

        _ = DownloadAvatar(account.Avatar);

        AvatarButton.ContentPosterId = account.Id;

        PosterInfo.AccountId = account.Id;
        PosterInfo.DisplayNameInfo.RawText = account.DisplayName;
        PosterInfo.DisplayNameInfo.Emojis = account.Emojis;
        PosterInfo.AccountName = account.AccountName;

        ReactButtons.Reblogged = status.Reblogged ?? false;
        ReactButtons.Favourited = status.Favourited ?? false;
        ReactButtons.Bookmarked = status.Bookmarked ?? false;

        Html.RawText = status.Content;
        Html.Emojis = status.Emojis;

        ContentId = status.Id;
        CreatedAt = status.CreatedAt;
        Uri = status.Uri;
        FollowedByGap = timeline.FollowedByGap;
    }

    public static IEnumerable<Status> FromTimelines(IEnumerable<Timeline> timelines) {
        timelines = timelines.ToCollection();
        for (int i = 0; i < timelines.Count(); i++) {
            yield return new Status(timelines.ElementAt(i));
        }
    }

    public async Task LoadMore() {
        var timelines = await client.GetTimelineFromServer(Model.Enums.TimelineType.Home, Id);
        FollowedByGap = false;
        MoreLoaded?.Invoke(Index, timelines);
    }

    private async Task DownloadAvatar(Uri? uri) {
        if (uri is not null) {
            var downloaded = await Cache.Get(uri);
            AvatarButton.ContentPosterAvatar = downloaded;
        }
    }

    private async Task DownloadRebloggerAvatar(Uri? uri) {
        if (uri is not null) {
            var downloaded = await Cache.Get(uri);
            AvatarButton.RebloggerAvatar = downloaded;
        }
    }
}
