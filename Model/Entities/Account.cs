namespace Model.Entities;

public record Account() {
    public string Id { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public Uri? Avatar { get; set; } = null;
    public Dictionary<string, Uri> Emojis { get; set; } = [];

    internal Account(Model.Server.Entities.Account serverAccount)
        : this(serverAccount.Id, serverAccount.Acct, serverAccount.DisplayName, serverAccount.Avatar, serverAccount.Emojis) {
    }

    internal Account(Model.Server.Entities.CredentialAccount serverAccount)
        : this(serverAccount.Id, serverAccount.Acct, serverAccount.DisplayName, serverAccount.Avatar, serverAccount.Emojis) {
    }

    private Account(string id, string acct, string displayName, Uri? avatar, IEnumerable<Model.Server.Entities.CustomEmoji> emojis) : this() {
        Id = id;
        AccountName = acct;
        DisplayName = displayName;
        Avatar = avatar;

        foreach (var emoji in emojis) {
            if (emoji.Url is not null) {
                Emojis[emoji.Shortcode] = emoji.Url;
            }
        }
    }

    internal static IEnumerable<Account> FromServer(IEnumerable<Model.Server.Entities.Account> serverAccounts) {
        foreach (var serverAccount in serverAccounts) {
            yield return new Account(serverAccount);
        }
    }
}
