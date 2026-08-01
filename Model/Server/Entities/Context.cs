using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents the tree around a given status. Used for reconstructing threads of statuses.
/// Version: 0.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Context/">Mastodon API Documentation</see>
public class Context {
    /// <summary>
    /// Parents in the thread.
    /// </summary>
    [JsonPropertyName("ancestors")]
    public IEnumerable<Status> Ancestors { get; set; } = [];

    /// <summary>
    /// Children in the thread.
    /// </summary>
    [JsonPropertyName("descendants")]
    public IEnumerable<Status> Descendants { get; set; } = [];
}
