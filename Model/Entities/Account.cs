using LiteDB;

namespace Model.Entities;
public record class Account {
    [BsonId]
    public string Id;
    public string AccountName;
    public string DisplayName;
    public string AvatarUrl;

    internal Account(Mastonet.Entities.Account serverAccount) {
        Id = serverAccount.Id;
        AccountName = serverAccount.AccountName;
        DisplayName = serverAccount.DisplayName;
        AvatarUrl = serverAccount.AvatarUrl;
    }
}
