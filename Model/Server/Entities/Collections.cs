using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Collection/#Collections">Mastodon API Documentation</see>
public sealed class Collections {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Collection/#collections">Mastodon API Documentation</see>
    [JsonPropertyName("collections")]
    public IEnumerable<Collection> Items { get; set; } = [];
}