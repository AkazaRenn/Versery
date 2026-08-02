using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Error/">Mastodon API Documentation</see>
public sealed class Error {
    [JsonPropertyName("error")]
    public string Message { get; set; } = string.Empty;

    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; } = null;
}
