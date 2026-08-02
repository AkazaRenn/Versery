using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class V1Instance {
    [JsonPropertyName("uri")]
    public string Uri { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("short_description")]
    public string ShortDescription { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    [JsonPropertyName("urls")]
    public V1InstanceUrls Urls { get; set; } = new();

    [JsonPropertyName("stats")]
    public V1InstanceStats Stats { get; set; } = new();

    [JsonPropertyName("thumbnail")]
    public Uri? Thumbnail { get; set; } = null;

    [JsonPropertyName("languages")]
    public List<string> Languages { get; set; } = [];

    [JsonPropertyName("registrations")]
    public bool Registrations { get; set; } = false;

    [JsonPropertyName("approval_required")]
    public bool ApprovalRequired { get; set; } = false;

    [JsonPropertyName("invites_enabled")]
    public bool InvitesEnabled { get; set; } = false;

    [JsonPropertyName("configuration")]
    public V1InstanceConfiguration Configuration { get; set; } = new();

    [JsonPropertyName("contact_account")]
    public Account? ContactAccount { get; set; } = null;

    [JsonPropertyName("rules")]
    public List<Rule> Rules { get; set; } = [];
}

public sealed class V1InstanceUrls {
    [JsonPropertyName("streaming_api")]
    public Uri? StreamingApi { get; set; } = null;
}

public sealed class V1InstanceStats {
    [JsonPropertyName("user_count")]
    public int UserCount { get; set; } = 0;

    [JsonPropertyName("status_count")]
    public int StatusCount { get; set; } = 0;

    [JsonPropertyName("domain_count")]
    public int DomainCount { get; set; } = 0;
}

public sealed class V1InstanceConfiguration {
    [JsonPropertyName("accounts")]
    public V1InstanceConfigurationAccounts Accounts { get; set; } = new();

    [JsonPropertyName("statuses")]
    public V1InstanceConfigurationStatuses Statuses { get; set; } = new();

    [JsonPropertyName("media_attachments")]
    public V1InstanceConfigurationMediaAttachments MediaAttachments { get; set; } = new();

    [JsonPropertyName("polls")]
    public V1InstanceConfigurationPolls Polls { get; set; } = new();
}

public sealed class V1InstanceConfigurationAccounts {
    [JsonPropertyName("max_featured_tags")]
    public int MaxFeaturedTags { get; set; } = 0;
}

public sealed class V1InstanceConfigurationStatuses {
    [JsonPropertyName("max_characters")]
    public int MaxCharacters { get; set; } = 0;

    [JsonPropertyName("max_media_attachments")]
    public int MaxMediaAttachments { get; set; } = 0;

    [JsonPropertyName("characters_reserved_per_url")]
    public int CharactersReservedPerUrl { get; set; } = 0;
}

public sealed class V1InstanceConfigurationMediaAttachments {
    [JsonPropertyName("supported_mime_types")]
    public List<string> SupportedMimeTypes { get; set; } = [];

    [JsonPropertyName("image_size_limit")]
    public int ImageSizeLimit { get; set; } = 0;

    [JsonPropertyName("image_matrix_limit")]
    public int ImageMatrixLimit { get; set; } = 0;

    [JsonPropertyName("video_size_limit")]
    public int VideoSizeLimit { get; set; } = 0;

    [JsonPropertyName("video_frame_rate_limit")]
    public int VideoFrameRateLimit { get; set; } = 0;

    [JsonPropertyName("video_matrix_limit")]
    public int VideoMatrixLimit { get; set; } = 0;
}

public sealed class V1InstanceConfigurationPolls {
    [JsonPropertyName("max_options")]
    public int MaxOptions { get; set; } = 0;

    [JsonPropertyName("max_characters_per_option")]
    public int MaxCharactersPerOption { get; set; } = 0;

    [JsonPropertyName("min_expiration")]
    public int MinExpiration { get; set; } = 0;

    [JsonPropertyName("max_expiration")]
    public int MaxExpiration { get; set; } = 0;
}
