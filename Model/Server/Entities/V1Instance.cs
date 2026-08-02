using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/V1_Instance/">Mastodon API Documentation</see>
public sealed class V1Instance {
    /// <summary>
    /// Version: 1.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#uri"/>
    [JsonPropertyName("uri")]
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// Version: 1.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#title"/>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.9.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#short_description"/>
    [JsonPropertyName("short_description")]
    public string ShortDescription { get; set; } = string.Empty;

    /// <summary>
    /// Version: 1.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#description"/>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Version: 1.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#email"/>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Version: 1.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#version"/>
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Version: 1.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#urls"/>
    [JsonPropertyName("urls")]
    public V1InstanceUrls Urls { get; set; } = new();

    /// <summary>
    /// Version: 1.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#stats"/>
    [JsonPropertyName("stats")]
    public V1InstanceStats Stats { get; set; } = new();

    /// <summary>
    /// Version: 1.6.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#thumbnail"/>
    [JsonPropertyName("thumbnail")]
    public Uri? Thumbnail { get; set; } = null;

    /// <summary>
    /// Version: 2.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#languages"/>
    [JsonPropertyName("languages")]
    public List<CultureInfo> Languages { get; set; } = [];

    /// <summary>
    /// Version: 2.7.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#registrations"/>
    [JsonPropertyName("registrations")]
    public bool Registrations { get; set; } = false;

    /// <summary>
    /// Version: 2.9.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#approval_required"/>
    [JsonPropertyName("approval_required")]
    public bool ApprovalRequired { get; set; } = false;

    /// <summary>
    /// Version: 3.1.4
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#invites_enabled"/>
    [JsonPropertyName("invites_enabled")]
    public bool InvitesEnabled { get; set; } = false;

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#configuration"/>
    [JsonPropertyName("configuration")]
    public V1InstanceConfiguration Configuration { get; set; } = new();

    /// <summary>
    /// Version: 2.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#contact_account"/>
    [JsonPropertyName("contact_account")]
    public Account? ContactAccount { get; set; } = null;

    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#rules"/>
    [JsonPropertyName("rules")]
    public List<Rule> Rules { get; set; } = [];
}

public sealed class V1InstanceUrls {
    /// <summary>
    /// Version: 1.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#streaming_api"/>
    [JsonPropertyName("streaming_api")]
    public Uri? StreamingApi { get; set; } = null;
}

public sealed class V1InstanceStats {
    /// <summary>
    /// Version: 1.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#user_count"/>
    [JsonPropertyName("user_count")]
    public int UserCount { get; set; } = 0;

    /// <summary>
    /// Version: 1.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#status_count"/>
    [JsonPropertyName("status_count")]
    public int StatusCount { get; set; } = 0;

    /// <summary>
    /// Version: 1.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#domain_count"/>
    [JsonPropertyName("domain_count")]
    public int DomainCount { get; set; } = 0;
}

public sealed class V1InstanceConfiguration {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#accounts"/>
    [JsonPropertyName("accounts")]
    public V1InstanceConfigurationAccounts Accounts { get; set; } = new();

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#statuses"/>
    [JsonPropertyName("statuses")]
    public V1InstanceConfigurationStatuses Statuses { get; set; } = new();

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#media_attachments"/>
    [JsonPropertyName("media_attachments")]
    public V1InstanceConfigurationMediaAttachments MediaAttachments { get; set; } = new();

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#polls"/>
    [JsonPropertyName("polls")]
    public V1InstanceConfigurationPolls Polls { get; set; } = new();
}

public sealed class V1InstanceConfigurationAccounts {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#max_featured_tags"/>
    [JsonPropertyName("max_featured_tags")]
    public int MaxFeaturedTags { get; set; } = 0;
}

public sealed class V1InstanceConfigurationStatuses {
    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#max_characters"/>
    [JsonPropertyName("max_characters")]
    public int MaxCharacters { get; set; } = 0;

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#max_media_attachments"/>
    [JsonPropertyName("max_media_attachments")]
    public int MaxMediaAttachments { get; set; } = 0;

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#characters_reserved_per_url"/>
    [JsonPropertyName("characters_reserved_per_url")]
    public int CharactersReservedPerUrl { get; set; } = 0;
}

public sealed class V1InstanceConfigurationMediaAttachments {
    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#supported_mime_types"/>
    [JsonPropertyName("supported_mime_types")]
    public List<string> SupportedMimeTypes { get; set; } = [];

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#image_size_limit"/>
    [JsonPropertyName("image_size_limit")]
    public int ImageSizeLimit { get; set; } = 0;

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#image_matrix_limit"/>
    [JsonPropertyName("image_matrix_limit")]
    public int ImageMatrixLimit { get; set; } = 0;

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#video_size_limit"/>
    [JsonPropertyName("video_size_limit")]
    public int VideoSizeLimit { get; set; } = 0;

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#video_frame_rate_limit"/>
    [JsonPropertyName("video_frame_rate_limit")]
    public int VideoFrameRateLimit { get; set; } = 0;

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#video_matrix_limit"/>
    [JsonPropertyName("video_matrix_limit")]
    public int VideoMatrixLimit { get; set; } = 0;
}

public sealed class V1InstanceConfigurationPolls {
    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#max_options"/>
    [JsonPropertyName("max_options")]
    public int MaxOptions { get; set; } = 0;

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#max_characters_per_option"/>
    [JsonPropertyName("max_characters_per_option")]
    public int MaxCharactersPerOption { get; set; } = 0;

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#min_expiration"/>
    [JsonPropertyName("min_expiration")]
    public int MinExpiration { get; set; } = 0;

    /// <summary>
    /// Version: 3.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Instance/#max_expiration"/>
    [JsonPropertyName("max_expiration")]
    public int MaxExpiration { get; set; } = 0;
}
