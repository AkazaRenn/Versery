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
    public Uri? RebloggerAvatarRemote { get; } = null;
    [ObservableProperty]
    public partial Uri? RebloggerAvatar { get; set; } = null;

    public string PosterId { get; } = String.Empty;
    public string PosterAccount { get; } = String.Empty;
    public Uri? PosterAvatarRemote { get; } = null;
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
            RebloggerId = account.Id;
            RebloggerDisplayName = account.DisplayName;
            RebloggerAvatarRemote = account.Avatar;

            status = client.GetStatus(status.ReblogId)!;
            account = client.GetAccount(status.AccountId)!;
        }

        PosterId = account.Id;
        PosterAccount = account.AccountName;
        PosterAvatarRemote = account.Avatar;

        PosterDisplayName.RawText = account.DisplayName;
        foreach (var emoji in account.Emojis) {
            PosterDisplayName.Emojis.Add(emoji.Key, Cache.Get(emoji.Value));
        }

        CanBeReblogged = status.Visibility < StatusVisibility.Private;
        HasReplies = status.RepliesCount > 0;
        IsReblogged = status.Reblogged;
        IsFavourited = status.Favourited;
        IsBookmarked = status.Bookmarked;

        Html.RawText = status.Content;
        foreach (var emoji in status.Emojis) {
            Html.Emojis.Add(emoji.Key, Cache.Get(emoji.Value));
        }

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

    public async Task DownloadMedias() {
        if (PosterAvatarRemote is not null) {
            PosterAvatar = await Cache.Get(PosterAvatarRemote);
        }
        if (RebloggerAvatarRemote is not null) {
            RebloggerAvatar = await Cache.Get(RebloggerAvatarRemote);
        }
    }
}
