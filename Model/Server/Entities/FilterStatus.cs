using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/FilterStatus/">Mastodon API Documentation</see>
public sealed class FilterStatus {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FilterStatus/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FilterStatus/#keyword"/>
    [JsonPropertyName("status_id")]
    public string StatusId { get; set; } = string.Empty;
}