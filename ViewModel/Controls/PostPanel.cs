using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.WinUI.Collections;
using Model;
using Model.Access;
using Model.Server.Entities;
using System.Collections.ObjectModel;
using System.Globalization;
using ViewModel.Enumerations;
using Windows.System.UserProfile;

namespace ViewModel.Controls;

public sealed partial class PostPanel: ObservableObject {
    private readonly Client client = Model.Services.Get<Client>();
    private bool allEmojisLoaded = false;

    [ObservableProperty]
    public partial bool IsEnabled { get; private set; } = true;

    [ObservableProperty]
    public partial Visibility Visibility { get; set; } = Visibility.Public;
    [RelayCommand]
    public void SetVisibility(Visibility visibility) => Visibility = visibility;

    [ObservableProperty]
    public partial string Language { get; set; } = string.Empty;
    [RelayCommand]
    public void SetLanguage(string language) => Language = language;
    public CultureInfo[] LanguageList => Constants.LanguageList;

    private async void SetDefaultProperties() {
        var account = await client.AccountTask;
        if (account is not null) {
            Visibility = account.Source.Privacy.Client;
            Language = account.Source.Language ?? Language;
        }
    }

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
        IsEnabled = false;
        var textStatus = new TextStatus() {
            Status = content,
            Visibility = Visibility.Server,
            Language = Language,
            SpoilerText = SpoilerText,
        };
        await client.PostStatus(textStatus);
        IsEnabled = true;
    }

    public PostPanel() {
        SetDefaultProperties();
        EmojisView = new(emojis) {
            Filter = x => ((PostPanel_Emoji)x).Shortcode.Contains(EmojiFilter, StringComparison.OrdinalIgnoreCase),
        };
    }
}

public sealed partial class PostPanel_Emoji(string shortCode, Uri? sourceRemote = null): ObservableObject {
    public string Shortcode => shortCode;
    [ObservableProperty]
    public partial Uri? Source { get; private set; } = null;
    internal async void Load() {
        if (sourceRemote is not null) {
            Source = await Cache.Get(sourceRemote);
        }
    }
}
