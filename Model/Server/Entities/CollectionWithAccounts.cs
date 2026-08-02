using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/CollectionWithAccounts/">Mastodon API Documentation</see>
public sealed class CollectionWithAccounts {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/CollectionWithAccounts/#accounts"/>
    [JsonPropertyName("accounts")]
    public List<Account> Accounts { get; set; } = [];

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/CollectionWithAccounts/#collection"/>
    [JsonPropertyName("collection")]
    public Collection Collection { get; set; } = new();
}