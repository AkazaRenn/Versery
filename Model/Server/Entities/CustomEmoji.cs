using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents a custom emoji.
/// Version: 3.0.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/CustomEmoji/">Mastodon API Documentation</see>
public class CustomEmoji {
    /// <summary>
    /// The name of the custom emoji.
    /// </summary>
    [JsonPropertyName("shortcode")]
    public string Shortcode { get; set; } = string.Empty;

    /// <summary>
    /// A link to the custom emoji.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// A link to a static copy of the custom emoji.
    /// </summary>
    [JsonPropertyName("static_url")]
    public Uri? StaticUrl { get; set; } = null;

    /// <summary>
    /// Whether this emoji should be visible in the picker or unlisted.
    /// </summary>
    [JsonPropertyName("visible_in_picker")]
    public bool VisibleInPicker { get; set; } = true;

    /// <summary>
    /// Used for sorting custom emoji in the picker.
    /// </summary>
    [JsonPropertyName("category")]
    public string? Category { get; set; } = null;
}
