using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/PrivacyPolicy/">Mastodon API Documentation</see>
public sealed class PrivacyPolicy {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PrivacyPolicy/#updated_at"/>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = default;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PrivacyPolicy/#content"/>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}
