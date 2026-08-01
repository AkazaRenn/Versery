using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents an error message.
/// Version: 0.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Error/">Mastodon API Documentation</see>
public class Error {
    /// <summary>
    /// The error message.
    /// </summary>
    [JsonPropertyName("error")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// A longer description of the error, mainly provided with the OAuth API.
    /// </summary>
    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; } = null;
}
