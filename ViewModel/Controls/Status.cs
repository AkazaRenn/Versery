using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

    public Html PosterDisplayName { get; } = new() {
        IsPlainText = true,
    };
    public Html? SpoilerText { get; } = null;
    public Html PostBody { get; } = new() {
        IsPlainText = false,
    };

    public Account Poster { get; }
    public DateTime CreatedAt { get; } = DateTime.MinValue;
    public Uri? Uri { get; } = null;
    public Status? Quote { get; } = null;

    [ObservableProperty]
    public partial bool Collapsed { get; private set; } = false;
    [ObservableProperty]
    public partial bool HasReplies { get; private set; } = false;
    [ObservableProperty]
    public partial bool CanBeReblogged { get; private set; } = true;
    [ObservableProperty]
    public partial bool IsReblogged { get; private set; } = false;
    [ObservableProperty]
    public partial bool IsFavourited { get; private set; } = false;
    [ObservableProperty]
    public partial bool IsBookmarked { get; private set; } = false;

    public double FirstImageAspect { get; } = 1.0;
    public Uri[] ImagesRemote { get; } = [];
    public MediaPreview?[] ImagePreviews { get; } = new MediaPreview?[4];
    [ObservableProperty]
    public partial bool ImagePreviewsHidden { get; private set; } = false;
    [RelayCommand]
    private void ToggleImagePreviewsHidden() {
        ImagePreviewsHidden = !ImagePreviewsHidden;
    }

    public Card_? Card { get; } = null;

    [RelayCommand]
    private void ToggleCollapsed() {
        Collapsed = !Collapsed;
    }

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
        if (!String.IsNullOrEmpty(status.SpoilerText)) {
            Collapsed = true;
            SpoilerText = new Html {
                IsPlainText = true,
                RawText = status.SpoilerText,
                Emojis = PostBody.Emojis,
            };
        }

        CreatedAt = status.CreatedAt;
        Uri = status.Uri;

        if (status.Images.Count > 0) {
            // Avoid the preview from taking too much space
            FirstImageAspect = Math.Clamp(status.Images[0].Aspect, 0.5, 1.5);
        }

        var validImages = status.Images.Where(x => x.Source is not null).ToArray();
        ImagesRemote = [.. validImages.Select(x => x.Source!)];
        for (int i = 0; i < Math.Min(ImagePreviews.Length, validImages.Length); i++) {
            ImagePreviews[i] = new(validImages[i]);
        }
        ImagePreviewsHidden = status.Sensitive;

        if (status.Card is not null) {
            Card = new(status.Card);
        }

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

    internal async Task DownloadMedias() {
        _ = Poster.DownloadAvatar();
        _ = Card?.DownloadImage();
        foreach (var imagePreview in ImagePreviews) {
            _ = imagePreview?.Download();
        }
    }

    public sealed partial class Account: ObservableObject {
        private static readonly Client client = Model.Services.Get<Client>();
        private static readonly Dictionary<string, WeakReference<Account>> cache = [];

        private readonly Sentinel<string, Account> sentinel;

        public string Id { get; }
        public string AccountName { get; }
        public Dictionary<string, Uri> Emojis { get; }
        public Html DisplayName { get; } = new();

        public Uri? AvatarRemote { get; }
        [ObservableProperty]
        public partial Uri? Avatar { get; private set; } = null;
        internal async Task DownloadAvatar() {
            if ((Avatar == null) && (AvatarRemote is not null)) {
                Avatar = await Cache.Get(AvatarRemote);
            }
        }

        private Account(string id) {
            Id = id;
            var account = client.GetAccount(id)!;
            AccountName = account.AccountName;
            Emojis = account.Emojis;
            DisplayName.RawText = account.DisplayName;
            foreach (var emoji in account.Emojis) {
                DisplayName.Emojis.Add(emoji.Key, Cache.Get(emoji.Value));
            }
            AvatarRemote = account.Avatar;

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

    public sealed partial class MediaPreview(Model.Entities.Media media): ObservableObject {
        public string BlurHash { get; internal set; } = media.BlurHash ?? String.Empty;

        public Uri? Remote { get; internal set; } = media.Preview;
        [ObservableProperty]
        public partial Uri? Uri { get; private set; } = null;
        internal async Task Download() {
            if (Uri is null && Remote is not null) {
                Uri = await Cache.Get(Remote);
            }
        }
    }

    public sealed partial class Card_(Model.Entities.Card card): ObservableObject {
        public Uri? Url { get; } = card.Url;
        public string Title { get; } = card.Title;
        public string Description { get; } = card.Description;
        public string ProviderName { get; } = card.ProviderName;

        public Uri? ImageRemote { get; } = card.Image;
        [ObservableProperty]
        public partial Uri? Image { get; private set; } = null;
        internal async Task DownloadImage() {
            if (Image is null && ImageRemote is not null) {
                Image = await Cache.Get(ImageRemote);
            }
        }
    }
}
