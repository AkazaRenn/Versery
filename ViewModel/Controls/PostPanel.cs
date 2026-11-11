using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.Collections;
using Model.Access;
using Model.Server.Entities;
using System.Collections.ObjectModel;

namespace ViewModel.Controls;

public sealed partial class PostPanel: ObservableObject {
    private readonly Client client = Model.Services.Get<Client>();
    private bool allEmojisLoaded = false;

    [ObservableProperty]
    public partial StatusVisibilityEnum Visibility { get; set; } = StatusVisibilityEnum.Public;

    [ObservableProperty]
    public partial string Language { get; set; } = string.Empty;
    private async void SetDefaultProperties() {
        var account = await client.AccountTask;
        if (account is not null) {
            Visibility = account.Source.Privacy;
            Language = account.Source.Language ?? Language;
        }
    }

    [RelayCommand]
    public void VisibilityPublic() => Visibility = StatusVisibilityEnum.Public;
    public bool IsVisibilityPublic => Visibility == StatusVisibilityEnum.Public;
    [RelayCommand]
    public void VisibilityUnlisted() => Visibility = StatusVisibilityEnum.Unlisted;
    public bool IsVisibilityUnlisted => Visibility == StatusVisibilityEnum.Unlisted;
    [RelayCommand]
    public void VisibilityPrivate() => Visibility = StatusVisibilityEnum.Private;
    public bool IsVisibilityPrivate => Visibility == StatusVisibilityEnum.Private;
    [RelayCommand]
    public void VisibilityDirect() => Visibility = StatusVisibilityEnum.Direct;
    public bool IsVisibilityDirect => Visibility == StatusVisibilityEnum.Direct;

    public string SpoilerText { get; set; } = string.Empty;

    public AdvancedCollectionView EmojisView { get; }
    private readonly ObservableCollection<PostPanel_Emoji> emojis = [];
    public string EmojiFilter {
        get;
        set {
            if (field != value) {
                field = value;
                UpdateEmojiFilter(value);
            }
        }
    } = string.Empty;
    public async void UpdateEmojiFilter(string filter) {
        await Task.Delay(200);
        if (EmojiFilter == filter) {
            EmojisView.Refresh();
        }
    }
    public void LoadInitialEmojis() {
        if (emojis.Count == 0) {
            LoadMoreEmojis();
        }
    }
    public async void LoadMoreEmojis() {
        if (allEmojisLoaded) {
            return;
        }
        const int emojiBatchSize = 30;
        var serverEmojis = await client.EmojisTask;
        var emojisToAdd = serverEmojis.Skip(emojis.Count).Take(emojiBatchSize);
        foreach (var emoji in emojisToAdd) {
            emojis.Add(new PostPanel_Emoji(emoji.Shortcode, emoji.Url));
        }
        allEmojisLoaded = serverEmojis.Count <= emojis.Count;
    }
    public void OnEmojiRealized(int index) {
        if ((index >= 0) &&
            (index < EmojisView.Count)) {
            ((PostPanel_Emoji)EmojisView[index]).Load();
        }
        if (index >= (EmojisView.Count - 1)) {
            LoadMoreEmojis();
        }
    }

    public async Task Post(string content) {
        if (string.IsNullOrWhiteSpace(content)) {
            return;
        }
        var textStatus = new TextStatus() {
            Status = content,
            Visibility = Visibility,
            SpoilerText = SpoilerText,
        };
        await client.PostStatus(textStatus);
    }

    public PostPanel() {
        SetDefaultProperties();
        EmojisView = new(emojis) {
            Filter = x => ((PostPanel_Emoji)x).ShortCode.Contains(EmojiFilter, StringComparison.OrdinalIgnoreCase),
        };
    }
}

public sealed partial class PostPanel_Emoji(string shortCode, Uri? sourceRemote = null): ObservableObject {
    public string ShortCode => shortCode;
    [ObservableProperty]
    public partial Uri? Source { get; private set; } = null;
    internal async void Load() {
        if (sourceRemote is not null) {
            Source = await Cache.Get(sourceRemote);
        }
    }
}
