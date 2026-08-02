using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Filter/">Mastodon API Documentation</see>
public sealed class Filter {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("context")]
    public IEnumerable<FilterContext> Context { get; set; } = [];

    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; } = null;

    [JsonPropertyName("filter_action")]
    public FilterAction FilterAction { get; set; } = default;

    [JsonPropertyName("keywords")]
    public IEnumerable<FilterKeyword>? Keywords { get; set; } = null;

    [JsonPropertyName("statuses")]
    public IEnumerable<FilterStatus>? Statuses { get; set; } = null;
}
