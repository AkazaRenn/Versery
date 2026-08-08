using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model;
using Model.Access;
using Model.Entities;
using Model.Enumerations;
using Model.Server.Entities;
using ViewModel.Controls.RichTextRenderer;

namespace ViewModel.Controls;

public sealed partial class Status: ObservableObject {
    private static readonly Client client = Model.Services.Get<Client>();
    private static readonly Dictionary<string, WeakReference<Status>> idToStatusMap = [];

    private readonly Sentinel sentinel;

    public event Action<int, IEnumerable<Model.Entities.Timeline>>? MoreLoaded;

    public string Id { get; }
    public int Index { get; set; } = -1;

    public Account? Reblogger { get; } = null;
    public Content Content { get; }

    [ObservableProperty]
    public partial bool FollowedByGap { get; set; } = false;

    private Status(Timeline timeline) {
        Id = timeline.Id;
        var status = client.GetStatus(Id)!;

        if (status.ReblogId == null) {
            Content = Content.Create(Id);
        } else {
            Reblogger = Account.Create(status.AccountId);
            Content = Content.Create(status.ReblogId);
        }
        FollowedByGap = timeline.FollowedByGap;

        sentinel = new(Id);
    }

    public static Status Create(Timeline timeline) {
        lock (idToStatusMap) {
            if ((!idToStatusMap.TryGetValue(timeline.Id, out var statusRef)) ||
                (!statusRef.TryGetTarget(out var status))) {
                status = new Status(timeline);
                statusRef = new WeakReference<Status>(status);
                idToStatusMap[timeline.Id] = statusRef;
            }
            return status;
        }
    }

    public static IEnumerable<Status> FromTimelines(IEnumerable<Timeline> timelines) {
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

    public async Task DownloadMedias() {
        if (Content.Poster.AvatarRemote is not null) {
            Content.Poster.Avatar = await Cache.Get(Content.Poster.AvatarRemote);
        }
        if (Reblogger?.AvatarRemote is not null) {
            Reblogger.Avatar = await Cache.Get(Reblogger.AvatarRemote);
        }
    }

    private sealed class Sentinel(string id) {
        ~Sentinel() {
            lock (idToStatusMap) {
                if (idToStatusMap.TryGetValue(id, out var statusRef) && !statusRef.TryGetTarget(out var _)) {
                    idToStatusMap.Remove(id);
                }
            }
        }
    }
}

public sealed partial class Content: ObservableObject {
    private static readonly Client client = Model.Services.Get<Client>();
    private static readonly Dictionary<string, WeakReference<Content>> idToStatusMap = [];

    private readonly Sentinel sentinel;

    public string Id { get; }
    public string ContentId { get; } = string.Empty;
    public int Index { get; set; } = -1;

    public RichTextRenderer.Html PosterDisplayName { get; set; } = new() {
        IsPlainText = true,
    };
    public RichTextRenderer.Html PostBody { get; set; } = new() {
        IsPlainText = false,
    };

    public Account Poster { get; }

    public DateTime CreatedAt { get; } = DateTime.MinValue;
    public Uri? Uri { get; } = null;

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

    private Content(string id) {
        Id = id;
        var status = client.GetStatus(Id)!;

        Poster = Account.Create(status.AccountId);

        CanBeReblogged = status.Visibility < StatusVisibility.Private;
        HasReplies = status.RepliesCount > 0;
        IsReblogged = status.Reblogged;
        IsFavourited = status.Favourited;
        IsBookmarked = status.Bookmarked;

        PostBody.RawText = status.Content;
        foreach (var emoji in status.Emojis) {
            PostBody.Emojis.Add(emoji.Key, Cache.Get(emoji.Value));
        }

        ContentId = status.Id;
        CreatedAt = status.CreatedAt;
        Uri = status.Uri;

        sentinel = new(Id);
    }

    public static Content Create(string id) {
        lock (idToStatusMap) {
            if ((!idToStatusMap.TryGetValue(id, out var contentRef)) ||
                (!contentRef.TryGetTarget(out var content))) {
                content = new Content(id);
                idToStatusMap[id] = new WeakReference<Content>(content);
            }
            return content;
        }
    }

    private sealed class Sentinel(string id) {
        ~Sentinel() {
            lock (idToStatusMap) {
                if (idToStatusMap.TryGetValue(id, out var statusRef) && !statusRef.TryGetTarget(out var _)) {
                    idToStatusMap.Remove(id);
                }
            }
        }
    }
}

public sealed partial class Account: ObservableObject {
    private static readonly Client client = Model.Services.Get<Client>();
    private static readonly Dictionary<string, WeakReference<Account>> idToAccountMap = [];

    private readonly Sentinel sentinel;

    public string Id { get; }
    public string AccountName { get; }
    public Uri? AvatarRemote { get; }
    public Dictionary<string, Uri> Emojis { get; }
    public Html DisplayName { get; } = new();

    [ObservableProperty]
    public partial Uri? Avatar { get; set; } = null;

    private Account(string id) {
        Id = id;
        var account = client.GetAccount(id)!;
        AccountName = account.AccountName;
        AvatarRemote = account.Avatar;
        Emojis = account.Emojis;
        DisplayName.RawText = account.DisplayName;
        foreach (var emoji in account.Emojis) {
            DisplayName.Emojis.Add(emoji.Key, Cache.Get(emoji.Value));
        }

        sentinel = new(id);
    }

    public static Account Create(string id) {
        lock (idToAccountMap) {
            if ((!idToAccountMap.TryGetValue(id, out var accountRef)) ||
                (!accountRef.TryGetTarget(out var account))) {
                account = new Account(id);
                idToAccountMap[id] = new WeakReference<Account>(account);
            }
            return account;
        }
    }

    private sealed class Sentinel(string id) {
        ~Sentinel() {
            lock (idToAccountMap) {
                if (idToAccountMap.TryGetValue(id, out var accountRef) && !accountRef.TryGetTarget(out var _)) {
                    idToAccountMap.Remove(id);
                }
            }
        }
    }
}