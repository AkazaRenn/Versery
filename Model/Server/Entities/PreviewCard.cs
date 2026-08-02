using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/PreviewCard/">Mastodon API Documentation</see>
public class PreviewCard {
    /// <summary>
    /// Version: 1.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#url"/>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// Version: 1.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#title"/>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Version: 1.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#description"/>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Version: 1.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#type"/>
    [JsonPropertyName("type")]
    public PreviewCardType Type { get; set; } = PreviewCardType.Link;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#authors"/>
    [JsonPropertyName("authors")]
    public List<PreviewCardAuthor> Authors { get; set; } = [];

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#author_name"/>
    [Obsolete("Deprecated since 4.3.0, clients should use Authors instead.")]
    [JsonPropertyName("author_name")]
    public string AuthorName { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#author_url"/>
    [Obsolete("Deprecated since 4.3.0, clients should use Authors instead.")]
    [JsonPropertyName("author_url")]
    public Uri? AuthorUrl { get; set; } = null;

    /// <summary>
    /// Version: 1.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#provider_name"/>
    [JsonPropertyName("provider_name")]
    public string ProviderName { get; set; } = string.Empty;

    /// <summary>
    /// Version: 1.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#provider_url"/>
    [JsonPropertyName("provider_url")]
    public Uri? ProviderUrl { get; set; } = null;

    /// <summary>
    /// Version: 1.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#html"/>
    [JsonPropertyName("html")]
    public string Html { get; set; } = string.Empty;

    /// <summary>
    /// Version: 1.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#height"/>
    [JsonPropertyName("width")]
    public int Width { get; set; } = 0;

    /// <summary>
    /// Version: 1.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#height"/>
    [JsonPropertyName("height")]
    public int Height { get; set; } = 0;

    /// <summary>
    /// Version: 1.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#image"/>
    [JsonPropertyName("image")]
    public Uri? Image { get; set; } = null;

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#embed_url"/>
    [JsonPropertyName("embed_url")]
    public Uri? EmbedUrl { get; set; } = null;

    /// <summary>
    /// Version: 3.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#blurhash"/>
    [JsonPropertyName("blurhash")]
    public string? Blurhash { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#missing_attribution"/>
    [JsonPropertyName("missing_attribution")]
    public bool? MissingAttribution { get; set; } = null;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#published-at"/>
    [JsonPropertyName("published_at")]
    public string? PublishedAt { get; set; } = null;
}

public sealed class TrendsLink: PreviewCard {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#trends-link"/>
    [JsonPropertyName("history")]
    public List<TrendsLinkHistory> History { get; set; } = [];
}

public sealed class TrendsLinkHistory {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#history-day"/>
    [JsonPropertyName("day")]
    public string Day { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#history-uses"/>
    [JsonPropertyName("uses")]
    public string Uses { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/PreviewCard/#history-accounts"/>
    [JsonPropertyName("accounts")]
    public string Accounts { get; set; } = string.Empty;
}

public enum PreviewCardType {
    [JsonStringEnumMemberName("link")]
    Link,

    [JsonStringEnumMemberName("photo")]
    Photo,

    [JsonStringEnumMemberName("video")]
    Video,

    [JsonStringEnumMemberName("rich")]
    Rich,
}
