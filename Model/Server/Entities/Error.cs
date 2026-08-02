using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Error/">Mastodon API Documentation</see>
public sealed class Error {
    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Error/#error"/>
    [JsonPropertyName("error")]
    public string Error_ { get; set; } = string.Empty;

    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Error/#error_description"/>
    [JsonPropertyName("error_description")]
    public string? ErrorDescription { get; set; } = null;
}
