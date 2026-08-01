using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents an author in a rich preview card.
/// Version: 4.3.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/PreviewCardAuthor/">Mastodon API Documentation</see>
public class PreviewCardAuthor {
    /// <summary>
    /// The original resource author’s name. Replaces the deprecated author_name attribute of the preview card.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A link to the author of the original resource. Replaces the deprecated author_url attribute of the preview card.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// The fediverse account of the author.
    /// </summary>
    [JsonPropertyName("account")]
    public Account? Account { get; set; } = null;
}