using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Conversation/">Mastodon API Documentation</see>
public sealed class Conversation {
    /// <summary>
    /// Version: 2.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Conversation/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Conversation/#unread"/>
    [JsonPropertyName("unread")]
    public bool Unread { get; set; } = true;

    /// <summary>
    /// Version: 2.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Conversation/#accounts"/>
    [JsonPropertyName("accounts")]
    public List<Account> Accounts { get; set; } = [];

    /// <summary>
    /// Version: 2.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Conversation/#last_status"/>
    [JsonPropertyName("last_status")]
    public Status? LastStatus { get; set; } = null;
}
