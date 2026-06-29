using LiteDB;

namespace Model.Entities;
public record class Account {
    [BsonId]
    public string Id { get; set; }
    public string AccountName { get; set; }
    public string DisplayName { get; set; }
    public string AvatarUrl { get; set; }

    internal Account(Mastonet.Entities.Account serverAccount) {
        Id = serverAccount.Id;
        AccountName = serverAccount.AccountName;
        DisplayName = serverAccount.DisplayName;
        AvatarUrl = serverAccount.AvatarUrl;
    }
}
