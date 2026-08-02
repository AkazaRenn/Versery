using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents an OAuth token used for authenticating with the API and performing actions.
/// Version: 0.1.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Token/">Mastodon API Documentation</see>
public sealed class Token {
    /// <summary>
    /// An OAuth token to be used for authorization.
    /// </summary>
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// The OAuth token type. Mastodon uses Bearer tokens.
    /// </summary>
    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = string.Empty;

    /// <summary>
    /// The OAuth scopes granted by this token, space-separated.
    /// </summary>
    [JsonPropertyName("scope")]
    public string Scope { get; set; } = string.Empty;

    /// <summary>
    /// When the token was generated.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
}