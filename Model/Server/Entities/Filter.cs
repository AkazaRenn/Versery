using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a user-defined filter for determining which statuses should not be shown to the user.
/// Version: 4.4.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Filter/">Mastodon API Documentation</see>
public class Filter {
    /// <summary>
    /// The ID of the Filter in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// A title given by the user to name the filter.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The contexts in which the filter should be applied.
    /// </summary>
    [JsonPropertyName("context")]
    public IEnumerable<FilterContext> Context { get; set; } = [];

    /// <summary>
    /// When the filter should no longer be applied.
    /// </summary>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; } = null;

    /// <summary>
    /// The action to be taken when a status matches this filter.
    /// </summary>
    [JsonPropertyName("filter_action")]
    public FilterAction FilterAction { get; set; } = default;

    /// <summary>
    /// The keywords grouped under this filter. Omitted when part of a FilterResult.
    /// </summary>
    [JsonPropertyName("keywords")]
    public IEnumerable<FilterKeyword>? Keywords { get; set; } = null;

    /// <summary>
    /// The statuses grouped under this filter. Omitted when part of a FilterResult.
    /// </summary>
    [JsonPropertyName("statuses")]
    public IEnumerable<FilterStatus>? Statuses { get; set; } = null;
}
