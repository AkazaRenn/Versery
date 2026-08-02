using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Context/">Mastodon API Documentation</see>
public sealed class Context {
    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Context/#ancestors"/>
    [JsonPropertyName("ancestors")]
    public List<Status> Ancestors { get; set; } = [];

    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Context/#descendants"/>
    [JsonPropertyName("descendants")]
    public List<Status> Descendants { get; set; } = [];
}
