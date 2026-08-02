using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// <see href="https://docs.joinmastodon.org/entities/FamiliarFollowers/">Mastodon API Documentation</see>
public sealed class FamiliarFollowers {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/FamiliarFollowers/#id">Mastodon API Documentation</see>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/FamiliarFollowers/#accounts">Mastodon API Documentation</see>
    [JsonPropertyName("accounts")]
    public IEnumerable<Account> Accounts { get; set; } = [];
}