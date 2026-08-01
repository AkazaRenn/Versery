using System.Text.Json.Serialization;

namespace Model.Server.Methods;
/// <summary>
/// Version: 4.3.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/methods/oauth/#response-1">Mastodon API Documentation</see>
public class OAuth {
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = string.Empty;

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; } = string.Empty;

    [JsonPropertyName("scope")]
    public string Scope { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public long CreatedAt { get; set; }
}
