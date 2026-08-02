using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents a conversation with "direct message" visibility.
/// Version: 2.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Conversation/">Mastodon API Documentation</see>
public sealed class Conversation {
    /// <summary>
    /// Local database ID of the conversation.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Participants in the conversation.
    /// </summary>
    [JsonPropertyName("accounts")]
    public IEnumerable<Account> Accounts { get; set; } = [];

    /// <summary>
    /// Is the conversation currently marked as unread?
    /// </summary>
    [JsonPropertyName("unread")]
    public bool Unread { get; set; } = true;

    /// <summary>
    /// The last status in the conversation, to be used for optional display.
    /// </summary>
    [JsonPropertyName("last_status")]
    public Status? LastStatus { get; set; } = null;
}
