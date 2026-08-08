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
    private static readonly Dictionary<string, WeakReference<Status>> cache = [];

    private readonly Sentinel sentinel;

    public event Action<int, IEnumerable<Model.Entities.Timeline>>? MoreLoaded;

    public string Id { get; }
    public int Index { get; set; } = -1;

    public Account? Reblogger { get; } = null;
    public StatusContent Content { get; }

    [ObservableProperty]
    public partial bool FollowedByGap { get; set; } = false;

    private Status(string id) {
        Id = id;
        var status = client.GetStatus(Id)!;

        if (status.ReblogId == null) {
            Content = StatusContent.Create(Id);
        } else {
            Reblogger = Account.Create(status.AccountId);
            Content = StatusContent.Create(status.ReblogId);
        }
        FollowedByGap = timeline.FollowedByGap;

        sentinel = new(Id);
    }

    internal static IEnumerable<Status> FromTimelines(IEnumerable<Timeline> timelines) {
        timelines = timelines.ToCollection();
        for (int i = 0; i < timelines.Count(); i++) {
            yield return Create(timelines.ElementAt(i).Id);
        }
    }

    [RelayCommand]
    public async Task LoadMore() {
        var timelines = await client.GetTimelineFromServer(TimelineType.Home, Id);
        FollowedByGap = false;
        MoreLoaded?.Invoke(Index, timelines);
    }

    internal async Task DownloadMedias() {
        if (Content.Poster.Avatar is null && Content.Poster.AvatarRemote is not null) {
            Content.Poster.Avatar = await Cache.Get(Content.Poster.AvatarRemote);
        }
        if (Reblogger?.Avatar is null && Reblogger?.AvatarRemote is not null) {
            Reblogger.Avatar = await Cache.Get(Reblogger.AvatarRemote);
        }
    }

    internal static Status Create(string id) {
        lock (cache) {
            if ((!cache.TryGetValue(id, out var reference)) ||
                (!reference.TryGetTarget(out var obj))) {
                obj = new(id);
                reference = new(obj);
                cache[id] = reference;
            }
            return obj;
        }
    }

    private sealed class Sentinel(string id) {
        ~Sentinel() {
            lock (cache) {
                if (cache.TryGetValue(id, out var reference) &&
                    !reference.TryGetTarget(out var _)) {
                    cache.Remove(id);
                }
            }
        }
    }

    public sealed partial class StatusContent: ObservableObject {
        private static readonly Client client = Model.Services.Get<Client>();
        private static readonly Dictionary<string, WeakReference<StatusContent>> cache = [];

        private readonly Sentinel sentinel;

        public string Id { get; }
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

        private StatusContent(string id) {
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

            CreatedAt = status.CreatedAt;
            Uri = status.Uri;

            sentinel = new(Id);
        }

        internal static StatusContent Create(string id) {
            lock (cache) {
                if ((!cache.TryGetValue(id, out var reference)) ||
                    (!reference.TryGetTarget(out var obj))) {
                    obj = new(id);
                    cache[id] = new(obj);
                }
                return obj;
            }
        }

        private sealed class Sentinel(string id) {
            ~Sentinel() {
                lock (cache) {
                    if (cache.TryGetValue(id, out var reference) &&
                        !reference.TryGetTarget(out var _)) {
                        cache.Remove(id);
                    }
                }
            }
        }
    }

    public sealed partial class Account: ObservableObject {
        private static readonly Client client = Model.Services.Get<Client>();
        private static readonly Dictionary<string, WeakReference<Account>> cache = [];

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

        internal static Account Create(string id) {
            lock (cache) {
                if ((!cache.TryGetValue(id, out var reference)) ||
                    (!reference.TryGetTarget(out var obj))) {
                    obj = new(id);
                    cache[id] = new(obj);
                }
                return obj;
            }
        }

        private sealed class Sentinel(string id) {
            ~Sentinel() {
                lock (cache) {
                    if (cache.TryGetValue(id, out var reference) &&
                        !reference.TryGetTarget(out var _)) {
                        cache.Remove(id);
                    }
                }
            }
        }
    }
}