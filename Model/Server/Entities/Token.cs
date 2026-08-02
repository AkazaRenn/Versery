using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Token/">Mastodon API Documentation</see>
public sealed class Token {
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = string.Empty;

    [JsonPropertyName("scope")]
    public string Scope { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
}