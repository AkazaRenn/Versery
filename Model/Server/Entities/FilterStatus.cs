using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a status ID that, if matched, should cause the filter action to be taken.
/// Version: 4.0.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/FilterStatus/">Mastodon API Documentation</see>
public sealed class FilterStatus {
    /// <summary>
    /// The ID of the FilterStatus in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the Status that will be filtered.
    /// </summary>
    [JsonPropertyName("status_id")]
    public string StatusId { get; set; } = string.Empty;
}