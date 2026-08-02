using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Conversation/">Mastodon API Documentation</see>
public sealed class Conversation {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("accounts")]
    public IEnumerable<Account> Accounts { get; set; } = [];

    [JsonPropertyName("unread")]
    public bool Unread { get; set; } = true;

    [JsonPropertyName("last_status")]
    public Status? LastStatus { get; set; } = null;
}
