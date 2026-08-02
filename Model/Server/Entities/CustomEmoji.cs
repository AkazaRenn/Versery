using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/CustomEmoji/">Mastodon API Documentation</see>
public sealed class CustomEmoji {
    /// <summary>
    /// Version: 2.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/CustomEmoji/#shortcode"/>
    [JsonPropertyName("shortcode")]
    public string Shortcode { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/CustomEmoji/#url"/>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// Version: 2.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/CustomEmoji/#static_url"/>
    [JsonPropertyName("static_url")]
    public Uri? StaticUrl { get; set; } = null;

    /// <summary>
    /// Version: 2.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/CustomEmoji/#visible_in_picker"/>
    [JsonPropertyName("visible_in_picker")]
    public bool VisibleInPicker { get; set; } = true;

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/CustomEmoji/#category"/>
    [JsonPropertyName("category")]
    public string? Category { get; set; } = null;
}
