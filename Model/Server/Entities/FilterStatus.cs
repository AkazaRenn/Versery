using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/FilterStatus/">Mastodon API Documentation</see>
public sealed class FilterStatus {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("status_id")]
    public string StatusId { get; set; } = string.Empty;
}