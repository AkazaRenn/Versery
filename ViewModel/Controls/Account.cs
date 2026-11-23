

using CommunityToolkit.Mvvm.ComponentModel;
using Model.Access;
using Model.Utilities;
using ViewModel.Extensions;

namespace ViewModel.Controls;

public sealed partial class Account: ObservableObject {
    private static readonly Client client = Model.Services.Get<Client>();
    private static readonly Dictionary<string, WeakReference<Account>> cache = [];

    private readonly Sentinel<string, Account> sentinel;

    public string Id { get; }
    public string AccountName { get; }
    public Html DisplayName { get; } = new();

    public Uri? AvatarRemote { get; }
    [ObservableProperty]
    public partial Uri? Avatar { get; private set; } = null;
    internal async void DownloadAvatar() {
        if ((Avatar == null) && (AvatarRemote is not null)) {
            Avatar = await Cache.Get(AvatarRemote);
        }
    }

    private Account(string id) {
        Id = id;
        var account = client.GetAccount(id)!;
        AccountName = $"@{account.Acct}";
        DisplayName.RawText = account.DisplayName;
        foreach (var emoji in account.Emojis) {
            DisplayName.Emojis.Add(emoji.Shortcode, Cache.Get(emoji.Url));
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