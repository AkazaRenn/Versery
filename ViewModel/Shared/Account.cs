

using CommunityToolkit.Mvvm.ComponentModel;
using Model.Access;
using ViewModel.Abstractions;
using ViewModel.Extensions;
using ViewModel.Interfaces;

namespace ViewModel.Shared;

public sealed partial class Account: CachedViewModel<Account>, ICachedViewModel<Account> {
    private static readonly Client client = Model.Services.Get<Client>();
    private int version = 0;

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

    private Account(string id) : base(id) {
        var account = client.GetAccount(id)!;
        AccountName = $"@{account.Acct}";
        DisplayName.RawText = account.DisplayName;
        foreach (var emoji in account.Emojis) {
            DisplayName.Emojis.Add(emoji.Shortcode, Cache.Get(emoji.Url));
        }
        AvatarRemote = account.Avatar;
    }

    static Account ICachedViewModel<Account>.New(string id) => new(id);

    internal void Update(int version) {
        if (this.version == version) {
            return;
        }

        this.version = version;
    }
}