using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a keyword that, if matched, should cause the filter action to be taken.
/// Version: 4.0.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/FilterKeyword/">Mastodon API Documentation</see>
public sealed class FilterKeyword {
    /// <summary>
    /// The ID of the FilterKeyword in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The phrase to be matched against.
    /// </summary>
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; } = string.Empty;

    /// <summary>
    /// Should the filter consider word boundaries? See implementation guidelines for filters.
    /// </summary>
    [JsonPropertyName("whole_word")]
    public bool WholeWord { get; set; } = false;
}