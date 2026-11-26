using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Model.Access;
using ViewModel.Abstractions;
using ViewModel.Extensions;
using ViewModel.Interfaces;
using ViewModel.Shared;

namespace ViewModel.Controls;

public sealed partial class Status: CachedViewModel<Status>, ICachedViewModel<Status> {
    private static readonly Client client = Model.Services.Get<Client>();
    private int version = 0;

    public Html? SpoilerText { get; } = null;
    public Html PostBody { get; } = new() {
        IsPlainText = false,
    };

    public Account Poster { get; }
    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.MinValue;
    public Uri? Uri { get; } = null;
    public Status? Quote { get; } = null;

    [ObservableProperty]
    public partial bool Collapsed { get; set; } = false;
    [ObservableProperty]
    public partial bool IsReply { get; private set; } = false;
    [ObservableProperty]
    public partial bool CanBeReblogged { get; private set; } = true;
    [ObservableProperty]
    public partial bool IsReblogged { get; private set; } = false;
    [RelayCommand]
    public async Task ToggleReblogged() {
        IsReblogged = !IsReblogged;
        var status = await client.ReblogStatus(Id, IsReblogged);
        IsReblogged = status?.Reblogged ?? !IsReblogged;
    }
    [ObservableProperty]
    public partial bool IsFavourited { get; private set; } = false;
    [RelayCommand]
    public async Task ToggleFavourited() {
        IsFavourited = !IsFavourited;
        var status = await client.FavouriteStatus(Id, IsFavourited);
        IsFavourited = status?.Favourited ?? !IsFavourited;
    }
    [ObservableProperty]
    public partial bool IsBookmarked { get; private set; } = false;

    public MediaPreview[] Media { get; } = [];
    public bool MediaPreviewsHidden { get; set; } = true;

    public PreviewCard? Card { get; } = null;

    private Status(string id) : base(id) {
        var status = client.GetStatus(Id)!;

        Poster = Account.Create(status.Account);

        if ((!string.IsNullOrWhiteSpace(status.Quote)) &&
            (status.Quote != Id)) {
            Quote = Create(status.Quote);
        }

        CanBeReblogged = status.Visibility < Model.Server.Entities.StatusVisibilityEnum.Private;
        IsReply = !string.IsNullOrWhiteSpace(status.InReplyToId);
        IsReblogged = status.Reblogged is true;
        IsFavourited = status.Favourited is true;
        IsBookmarked = status.Bookmarked is true;

        PostBody.RawText = status.Content;
        foreach (var emoji in status.Emojis) {
            PostBody.Emojis.Add(emoji.Shortcode, Cache.Get(emoji.Url));
        }
        if (!string.IsNullOrEmpty(status.SpoilerText)) {
            Collapsed = true;
            SpoilerText = new Html {
                IsPlainText = true,
                RawText = status.SpoilerText,
                Emojis = PostBody.Emojis,
            };
        }

        CreatedAt = status.CreatedAt;
        if (Uri.TryCreate(status.Uri, UriKind.Absolute, out var uri)) {
            Uri = uri;
        }

        Media = [.. status.MediaAttachments.Select(x => new MediaPreview(x))];
        MediaPreviewsHidden = status.Sensitive;

        if (status.Card is not null) {
            Card = new PreviewCard(status.Card);
        }
    }

    static Status ICachedViewModel<Status>.New(string id) => new(id);

    internal void Update(int version) {
        if (this.version == version) {
            return;
        }

        this.version = version;
        Poster.Update(version);
        Quote?.Update(version);
    }

    internal void DownloadMedia() {
        Poster.DownloadAvatar();
        Card?.DownloadImage();
        foreach (var media in Media) {
            media.Download();
        }
        Quote?.DownloadMedia();
    }
}
