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

    public AdvancedCollectionView EmojisView { get; }
    public ObservableCollection<PostPanel_Emoji> Emojis { get; } = [];
    private string currentEmojiFilter = string.Empty;
    public async void UpdateEmojiFilter(string filter) {
        if (filter == currentEmojiFilter) {
            return;
        }

        currentEmojiFilter = filter;
        await Task.Delay(200);
        if (currentEmojiFilter != filter) {
            return;
        }

        using (EmojisView.DeferRefresh()) {
            if (string.IsNullOrWhiteSpace(filter)) {
                EmojisView.Filter = x => true;
            } else {
                EmojisView.Filter = x => ((PostPanel_Emoji)x).ShortCode.Contains(filter);
            }
        }
    }
    public async void LoadMoreEmojis() {
        if (allEmojisLoaded) {
            return;
        }
        const int emojiBatchSize = 100;
        var emojis = await client.GetCustomEmojis();
        var emojisToAdd = emojis.Skip(Emojis.Count).Take(emojiBatchSize).Where(x => x.VisibleInPicker);
        foreach (var emoji in emojisToAdd) {
            Emojis.Add(new PostPanel_Emoji(emoji.Shortcode, emoji.Url));
        }
        allEmojisLoaded = emojis.Count <= Emojis.Count;
    }
    public void OnEmojiRealized(int index) {
        if (index >= 0 && index < Emojis.Count) {
            Emojis[index].Load();
        }
        if (index >= Emojis.Count - 1) {
            LoadMoreEmojis();
        }
    }

    public PostPanel() {
        EmojisView = new(Emojis);
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
