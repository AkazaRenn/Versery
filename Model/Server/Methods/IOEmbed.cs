using Refit;
using System.Text.Json.Serialization;

namespace Model.Server.Methods;

/// <see href="https://docs.joinmastodon.org/methods/oembed/">Mastodon API Documentation</see>
public interface IOEmbed {
    /// <summary>
    /// Version: 1.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/oembed/#get"/>
    [Get("/api/oembed")]
    Task<OEmbed> Get(
        [AliasAs("url")] string url,
        [AliasAs("maxwidth")] int? maxWidth = null,
        [AliasAs("maxheight")] int? maxHeight = null);
}

/// <see href="https://docs.joinmastodon.org/methods/oembed/#response">Mastodon API Documentation</see>
public sealed class OEmbed {
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("author_name")]
    public string AuthorName { get; set; } = string.Empty;

    [JsonPropertyName("author_url")]
    public Uri? AuthorUrl { get; set; } = null;

    [JsonPropertyName("provider_name")]
    public string ProviderName { get; set; } = string.Empty;

    [JsonPropertyName("provider_url")]
    public Uri? ProviderUrl { get; set; } = null;

    [JsonPropertyName("cache_age")]
    public int CacheAge { get; set; } = 0;

    [JsonPropertyName("html")]
    public string Html { get; set; } = string.Empty;

    [JsonPropertyName("width")]
    public int Width { get; set; } = 0;

    [JsonPropertyName("height")]
    public int? Height { get; set; } = null;
}