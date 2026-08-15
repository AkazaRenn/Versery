using CommunityToolkit.Mvvm.ComponentModel;
using Model.Access;
using Model.Server.Entities;
using Model.Utilities;
using System.Collections.ObjectModel;
using ViewModel.Extensions;

namespace ViewModel.Controls;

public sealed partial class Status: ObservableObject {
    private static readonly Client client = Model.Services.Get<Client>();
    private static readonly Dictionary<string, WeakReference<Status>> cache = [];

    private readonly Sentinel<string, Status> sentinel;

    public string Id { get; }
    public int Index { get; set; } = -1;

    public Html PosterDisplayName { get; set; } = new() {
        IsPlainText = true,
    };
    public Html PostBody { get; set; } = new() {
        IsPlainText = false,
    };

    public Account Poster { get; }
    public DateTime CreatedAt { get; } = DateTime.MinValue;
    public Uri? Uri { get; } = null;
    public ObservableCollection<Uri?>? MediaAttachments { get; } = null;
    public Status? Quote { get; } = null;

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

    private Status(string id) {
        Id = id;
        var status = client.GetStatus(Id)!;

        Poster = Account.Create(status.AccountId);

        if ((status.QuotedStatusId is not null) &&
            (status.QuotedStatusId != Id)) {
            Quote = Create(status.QuotedStatusId);
        }

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

        sentinel = new(Id, cache);
    }

    internal static Status Create(string id) {
        lock (cache) {
            if ((!cache.TryGetValue(id, out var reference)) ||
                (!reference.TryGetTarget(out var obj))) {
                obj = new(id);
                cache[id] = new(obj);
            }
            return obj;
        }
    }

    public sealed partial class Account: ObservableObject {
        private static readonly Client client = Model.Services.Get<Client>();
        private static readonly Dictionary<string, WeakReference<Account>> cache = [];

        private readonly Sentinel<string, Account> sentinel;

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

            sentinel = new(Id, cache);
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
    }
}