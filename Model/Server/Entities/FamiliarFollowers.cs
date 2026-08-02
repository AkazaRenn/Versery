using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/FamiliarFollowers/">Mastodon API Documentation</see>
public sealed class FamiliarFollowers {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FamiliarFollowers/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FamiliarFollowers/#accounts"/>
    [JsonPropertyName("accounts")]
    public List<Account> Accounts { get; set; } = [];
}