using LiteDB;

namespace Model.Entities;
public record class Account() {
    [BsonId]
    public string Id { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public Uri? Avatar { get; set; } = null;
    public Dictionary<string, Uri> Emojis { get; set; } = [];

    internal Account(Mastonet.Entities.Account serverAccount): this() {
        Id = serverAccount.Id;
        AccountName = serverAccount.AccountName;
        DisplayName = serverAccount.DisplayName;

        if (Uri.TryCreate(serverAccount.AvatarUrl, UriKind.Absolute, out var avatarUri)) {
            Avatar = avatarUri;
        }
        foreach (var emoji in serverAccount.Emojis) {
            if (Uri.TryCreate(emoji.Url, UriKind.Absolute, out var emojiUri)) {
                Emojis[emoji.Shortcode] = emojiUri;
            }
        }
    }

    internal static IEnumerable<Account> FromServer(IEnumerable<Mastonet.Entities.Account> serverAccounts) {
        foreach (var serverAccount in serverAccounts) {
            yield return new Account(serverAccount);
        }
    }
}
