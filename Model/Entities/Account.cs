using LiteDB;

namespace Model.Entities;
public record class Account() {
    [BsonId]
    public string Id { get; set; } = string.Empty;
    public string AccountName { get; set; } = string.Empty;
    public string DisplayName { get; set; } = string.Empty;
    public string AvatarUrl { get; set; } = string.Empty;

    internal Account(Mastonet.Entities.Account serverAccount): this() {
        Id = serverAccount.Id;
        AccountName = serverAccount.AccountName;
        DisplayName = serverAccount.DisplayName;
        AvatarUrl = serverAccount.AvatarUrl;
    }
}
