using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Context/">Mastodon API Documentation</see>
public sealed class Context {
    [JsonPropertyName("ancestors")]
    public IEnumerable<Status> Ancestors { get; set; } = [];

    [JsonPropertyName("descendants")]
    public IEnumerable<Status> Descendants { get; set; } = [];
}
