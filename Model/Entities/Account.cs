using LiteDB;

namespace Model.Entities;

public record Account() {
    [BsonId]
    public string Id { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public Uri? Avatar { get; set; } = null;
    public Dictionary<string, Uri> Emojis { get; set; } = [];

    internal Account(Model.Server.Entities.Account serverAccount) : this() {
        Id = serverAccount.Id;
        AccountName = serverAccount.Acct;
        DisplayName = serverAccount.DisplayName;
        Avatar = serverAccount.Avatar;

        foreach (var emoji in serverAccount.Emojis) {
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
