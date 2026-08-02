using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Token/">Mastodon API Documentation</see>
public sealed class Token {
    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Token/#access_token"/>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Token/#token_type"/>
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = string.Empty;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Token/#scope"/>
    [JsonPropertyName("scope")]
    public string Scope { get; set; } = string.Empty;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Token/#created_at"/>
    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; } = 0;
}