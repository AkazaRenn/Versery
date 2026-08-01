using Model.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a rich preview card that is generated using OpenGraph tags from a URL.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/PreviewCard/">Mastodon API Documentation</see>
public class PreviewCard {
    /// <summary>
    /// Location of linked resource.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// Title of linked resource.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description of preview.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The type of the preview card.
    /// </summary>
    [JsonPropertyName("type")]
    public PreviewCardType Type { get; set; } = default;

    /// <summary>
    /// Fediverse account of the authors of the original resource.
    /// </summary>
    [JsonPropertyName("authors")]
    public IEnumerable<PreviewCardAuthor> Authors { get; set; } = [];

    /// <summary>
    /// The author of the original resource.
    /// </summary>
    [Obsolete("Deprecated since 4.3.0, clients should use Authors instead.")]
    [JsonPropertyName("author_name")]
    public string AuthorName { get; set; } = string.Empty;

    /// <summary>
    /// A link to the author of the original resource.
    /// </summary>
    [Obsolete("Deprecated since 4.3.0, clients should use Authors instead.")]
    [JsonPropertyName("author_url")]
    public string AuthorUrl { get; set; } = string.Empty;

    /// <summary>
    /// The provider of the original resource.
    /// </summary>
    [JsonPropertyName("provider_name")]
    public string ProviderName { get; set; } = string.Empty;

    /// <summary>
    /// A link to the provider of the original resource.
    /// </summary>
    [JsonPropertyName("provider_url")]
    public Uri? ProviderUrl { get; set; } = null;

    /// <summary>
    /// HTML to be used for generating the preview card.
    /// </summary>
    [JsonPropertyName("html")]
    public string Html { get; set; } = string.Empty;

    /// <summary>
    /// Width of preview, in pixels.
    /// </summary>
    [JsonPropertyName("width")]
    public int Width { get; set; } = 0;

    /// <summary>
    /// Height of preview, in pixels.
    /// </summary>
    [JsonPropertyName("height")]
    public int Height { get; set; } = 0;

    /// <summary>
    /// Preview thumbnail.
    /// </summary>
    [JsonPropertyName("image")]
    public Uri? Image { get; set; } = null;

    /// <summary>
    /// Used for photo embeds, instead of custom html.
    /// </summary>
    [JsonPropertyName("embed_url")]
    public Uri? EmbedUrl { get; set; } = null;

    /// <summary>
    /// A hash computed by the BlurHash algorithm, for generating colorful preview thumbnails
    /// when media has not been downloaded yet.
    /// </summary>
    [JsonPropertyName("blurhash")]
    public string? BlurHash { get; set; } = null;

    /// <summary>
    /// True if the linked article claims to be written by the current user without the user having the article’s domain in their attribution_domains). This is used to prompt them to review and add the domain.
    /// </summary>
    [JsonPropertyName("missing_attribution")]
    public bool? MissingAttribution { get; set; } = null;

    /// <summary>
    /// UNIX timestamp of publication date.
    /// </summary>
    [JsonPropertyName("published_at")]
    public DateTime PublishedAt { get; set; } = DateTime.MinValue;
}

// TODO: https://docs.joinmastodon.org/entities/PreviewCard/#trends-link
