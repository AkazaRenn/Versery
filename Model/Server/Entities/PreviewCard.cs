using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/PreviewCard/">Mastodon API Documentation</see>
public class PreviewCard {
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("type")]
    public PreviewCardType Type { get; set; } = default;

    [JsonPropertyName("authors")]
    public IEnumerable<PreviewCardAuthor> Authors { get; set; } = [];

    [Obsolete("Deprecated since 4.3.0, clients should use Authors instead.")]
    [JsonPropertyName("author_name")]
    public string AuthorName { get; set; } = string.Empty;

    [Obsolete("Deprecated since 4.3.0, clients should use Authors instead.")]
    [JsonPropertyName("author_url")]
    public string AuthorUrl { get; set; } = string.Empty;

    [JsonPropertyName("provider_name")]
    public string ProviderName { get; set; } = string.Empty;

    [JsonPropertyName("provider_url")]
    public Uri? ProviderUrl { get; set; } = null;

    [JsonPropertyName("html")]
    public string Html { get; set; } = string.Empty;

    [JsonPropertyName("width")]
    public int Width { get; set; } = 0;

    [JsonPropertyName("height")]
    public int Height { get; set; } = 0;

    [JsonPropertyName("image")]
    public Uri? Image { get; set; } = null;

    [JsonPropertyName("embed_url")]
    public Uri? EmbedUrl { get; set; } = null;

    [JsonPropertyName("blurhash")]
    public string? BlurHash { get; set; } = null;

    [JsonPropertyName("missing_attribution")]
    public bool? MissingAttribution { get; set; } = null;

    [JsonPropertyName("published_at")]
    public DateTime PublishedAt { get; set; } = DateTime.MinValue;
}

/// <see href="https://docs.joinmastodon.org/entities/PreviewCard/#trends-link">Mastodon API Documentation</see>
public sealed class TrendsLink: PreviewCard {
    /// <see href="https://docs.joinmastodon.org/entities/PreviewCard/#trends-link">Mastodon API Documentation</see>
    [JsonPropertyName("history")]
    public IEnumerable<TrendsLinkHistory> History { get; set; } = [];
}

/// <see href="https://docs.joinmastodon.org/entities/PreviewCard/#history">Mastodon API Documentation</see>
public sealed class TrendsLinkHistory {
    /// <see href="https://docs.joinmastodon.org/entities/PreviewCard/#history-day">Mastodon API Documentation</see>
    [JsonPropertyName("day")]
    public string Day { get; set; } = string.Empty;

    /// <see href="https://docs.joinmastodon.org/entities/PreviewCard/#history-uses">Mastodon API Documentation</see>
    [JsonPropertyName("uses")]
    public string Uses { get; set; } = string.Empty;

    /// <see href="https://docs.joinmastodon.org/entities/PreviewCard/#history-accounts">Mastodon API Documentation</see>
    [JsonPropertyName("accounts")]
    public string Accounts { get; set; } = string.Empty;
}
