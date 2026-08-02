using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/ExtendedDescription/">Mastodon API Documentation</see>
public sealed class ExtendedDescription {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ExtendedDescription/#updated_at"/>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = default;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/ExtendedDescription/#content"/>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}
