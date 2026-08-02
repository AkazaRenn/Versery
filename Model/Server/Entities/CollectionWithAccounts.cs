using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/CollectionWithAccounts/">Mastodon API Documentation</see>
public sealed class CollectionWithAccounts {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/CollectionWithAccounts/#accounts">Mastodon API Documentation</see>
    [JsonPropertyName("accounts")]
    public IEnumerable<Account> Accounts { get; set; } = [];

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/CollectionWithAccounts/#collection">Mastodon API Documentation</see>
    [JsonPropertyName("collection")]
    public Collection Collection { get; set; } = new();
}