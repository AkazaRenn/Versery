using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/CustomEmoji/">Mastodon API Documentation</see>
public sealed class CustomEmoji {
    [JsonPropertyName("shortcode")]
    public string Shortcode { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    [JsonPropertyName("static_url")]
    public Uri? StaticUrl { get; set; } = null;

    [JsonPropertyName("visible_in_picker")]
    public bool VisibleInPicker { get; set; } = true;

    [JsonPropertyName("category")]
    public string? Category { get; set; } = null;
}
