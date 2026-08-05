using CommunityToolkit.Mvvm.ComponentModel;
using Model;
using Model.Access;
using Model.Entities;
using Model.Enumerations;
using Model.Server.Entities;

namespace ViewModel.Controls;

public sealed partial class Status: ObservableObject {
    private static readonly Client client = Model.Services.Get<Client>();

    public event Action<int, IEnumerable<Model.Entities.Timeline>>? MoreLoaded;

    public string Id { get; }
    public string ContentId { get; } = string.Empty;
    public int Index { get; set; } = -1;

    public RichTextRenderer.Html PosterDisplayName { get; set; } = new() {
        IsPlainText = true,
    };
    public RichTextRenderer.Html Html { get; set; } = new() {
        IsPlainText = false,
    };

    public string? RebloggerId { get; } = null;
    public string? RebloggerDisplayName { get; } = null;
    [ObservableProperty]
    public partial Uri? RebloggerAvatar { get; set; } = null;

    public string PosterId { get; }= String.Empty;
    public string PosterAccount { get; }= String.Empty;
    [ObservableProperty]
    public partial Uri? PosterAvatar { get; set; } = null;

    public DateTime CreatedAt { get; } = DateTime.MinValue;
    public Uri? Uri { get; } = null;
    [ObservableProperty]
    public partial bool FollowedByGap { get; set; } = false;

    [ObservableProperty]
    public partial bool HasReplies { get; set; } = false;
    [ObservableProperty]
    public partial bool CanBeReblogged { get; set; } = true;
    [ObservableProperty]
    public partial bool IsReblogged { get; set; } = false;
    [ObservableProperty]
    public partial bool IsFavourited { get; set; } = false;
    [ObservableProperty]
    public partial bool IsBookmarked { get; set; } = false;

    public Status(Timeline timeline) {
        Id = timeline.Id;

        var status = client.GetStatus(Id)!;
        var account = client.GetAccount(status.AccountId)!;

        if (status.ReblogId != null) {
            _ = DownloadRebloggerAvatar(account.Avatar);

            RebloggerId = account.Id;
            RebloggerDisplayName = account.DisplayName;

            status = client.GetStatus(status.ReblogId)!;
            account = client.GetAccount(status.AccountId)!;
        }

        _ = DownloadAvatar(account.Avatar);

        PosterId = account.Id;
        PosterAccount = account.AccountName;

        PosterDisplayName.RawText = account.DisplayName;
        PosterDisplayName.Emojis = account.Emojis;

        CanBeReblogged = status.Visibility < StatusVisibility.Private;
        HasReplies = status.RepliesCount > 0;
        IsReblogged = status.Reblogged;
        IsFavourited = status.Favourited;
        IsBookmarked = status.Bookmarked;

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
        var timelines = await client.GetTimelineFromServer(TimelineType.Home, Id);
        FollowedByGap = false;
        MoreLoaded?.Invoke(Index, timelines);
    }

    private async Task DownloadAvatar(Uri? uri) {
        if (uri is not null) {
            var downloaded = await Cache.Get(uri);
            PosterAvatar = downloaded;
        }
    }

    private async Task DownloadRebloggerAvatar(Uri? uri) {
        if (uri is not null) {
            var downloaded = await Cache.Get(uri);
            RebloggerAvatar = downloaded;
        }
    }
}
