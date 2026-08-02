using Model.Server.Entities.Enumerations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Instance/">Mastodon API Documentation</see>
public sealed class Instance {
    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    [JsonPropertyName("source_url")]
    public Uri? SourceUrl { get; set; } = null;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("wrapstodon")]
    public string? Wrapstodon { get; set; } = null;

    [JsonPropertyName("usage")]
    public InstanceUsage Usage { get; set; } = new();

    [JsonPropertyName("thumbnail")]
    public InstanceThumbnail Thumbnail { get; set; } = new();

    [JsonPropertyName("icon")]
    public IEnumerable<InstanceIcon> Icon { get; set; } = [];

    [JsonPropertyName("languages")]
    public IEnumerable<CultureInfo> Languages { get; set; } = [];

    [JsonPropertyName("configuration")]
    public InstanceConfiguration Configuration { get; set; } = new();

    [JsonPropertyName("registrations")]
    public InstanceRegistrations Registrations { get; set; } = new();

    [JsonPropertyName("api_versions")]
    public InstanceApiVersions ApiVersions { get; set; } = new();

    [JsonPropertyName("contact")]
    public InstanceContact Contact { get; set; } = new();

    [JsonPropertyName("rules")]
    public IEnumerable<Rule> Rules { get; set; } = [];
}

public sealed class InstanceUsage {
    [JsonPropertyName("users")]
    public InstanceUsageUsers Users { get; set; } = new();
}

public sealed class InstanceUsageUsers {
    [JsonPropertyName("active_month")]
    public int ActiveMonth { get; set; } = 0;
}

public sealed class InstanceThumbnail {
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    [JsonPropertyName("blurhash")]
    public string? Blurhash { get; set; } = null;

    [JsonPropertyName("versions")]
    public InstanceThumbnailVersions Versions { get; set; } = new();
}

public sealed class InstanceThumbnailVersions {
    [JsonPropertyName("@1x")]
    public Uri? OneX { get; set; } = null;

    [JsonPropertyName("@2x")]
    public Uri? TwoX { get; set; } = null;
}

public sealed class InstanceConfiguration {
    [JsonPropertyName("urls")]
    public InstanceConfigurationUrls Urls { get; set; } = new();

    [JsonPropertyName("vapid")]
    public InstanceConfigurationVapid Vapid { get; set; } = new();

    [JsonPropertyName("accounts")]
    public InstanceConfigurationAccounts Accounts { get; set; } = new();

    [JsonPropertyName("statuses")]
    public InstanceConfigurationStatuses Statuses { get; set; } = new();

    [JsonPropertyName("media_attachments")]
    public InstanceConfigurationMediaAttachments MediaAttachments { get; set; } = new();

    [JsonPropertyName("polls")]
    public InstanceConfigurationPolls Polls { get; set; } = new();

    [JsonPropertyName("translation")]
    public InstanceConfigurationTranslation Translation { get; set; } = new();

    [JsonPropertyName("timelines_access")]
    public InstanceConfigurationTimelinesAccess TimelinesAccess { get; set; } = new();

    [JsonPropertyName("limited_federation")]
    public bool LimitedFederation { get; set; } = false;
}

public sealed class InstanceConfigurationVapid {
    [JsonPropertyName("public_key")]
    public string PublicKey { get; set; } = string.Empty;
}

public sealed class InstanceConfigurationAccounts {
    [JsonPropertyName("max_display_name_length")]
    public int MaxDisplayNameLength { get; set; } = 0;

    [JsonPropertyName("max_note_length")]
    public int MaxNoteLength { get; set; } = 0;

    [JsonPropertyName("max_avatar_description_length")]
    public int MaxAvatarDescriptionLength { get; set; } = 0;

    [JsonPropertyName("max_header_description_length")]
    public int MaxHeaderDescriptionLength { get; set; } = 0;

    [JsonPropertyName("max_featured_tags")]
    public int MaxFeaturedTags { get; set; } = 0;

    [JsonPropertyName("max_pinned_statuses")]
    public int MaxPinnedStatuses { get; set; } = 0;

    [JsonPropertyName("max_profile_fields")]
    public int MaxProfileFields { get; set; } = 0;

    [JsonPropertyName("profile_field_name_limit")]
    public int ProfileFieldNameLimit { get; set; } = 0;

    [JsonPropertyName("profile_field_value_limit")]
    public int ProfileFieldValueLimit { get; set; } = 0;
}

public sealed class InstanceConfigurationStatuses {
    [JsonPropertyName("max_characters")]
    public int MaxCharacters { get; set; } = 0;

    [JsonPropertyName("max_media_attachments")]
    public int MaxMediaAttachments { get; set; } = 0;

    [JsonPropertyName("characters_reserved_per_url")]
    public int CharactersReservedPerUrl { get; set; } = 0;
}

public sealed class InstanceConfigurationMediaAttachments {
    [JsonPropertyName("supported_mime_types")]
    public IEnumerable<string> SupportedMimeTypes { get; set; } = [];

    [JsonPropertyName("description_limit")]
    public int DescriptionLimit { get; set; } = 0;

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

public sealed class InstanceConfigurationPolls {
    [JsonPropertyName("max_options")]
    public int MaxOptions { get; set; } = 0;

    [JsonPropertyName("max_characters_per_option")]
    public int MaxCharactersPerOption { get; set; } = 0;

    [JsonPropertyName("min_expiration")]
    public int MinExpiration { get; set; } = 0;

    [JsonPropertyName("max_expiration")]
    public int MaxExpiration { get; set; } = 0;
}

public sealed class InstanceConfigurationTranslation {
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = false;
}

public sealed class InstanceConfigurationTimelinesAccess {
    [JsonPropertyName("live_feeds")]
    public InstanceConfigurationTimelinesAccessFeeds LiveFeeds { get; set; } = new();

    [JsonPropertyName("hashtag_feeds")]
    public InstanceConfigurationTimelinesAccessFeeds HashtagFeeds { get; set; } = new();

    [JsonPropertyName("trending_link_feeds")]
    public InstanceConfigurationTimelinesAccessFeeds TrendingLinkFeeds { get; set; } = new();
}

public sealed class InstanceConfigurationTimelinesAccessFeeds {
    [JsonPropertyName("local")]
    public InstanceConfigurationTimelineAccess Local { get; set; } = default;

    [JsonPropertyName("remote")]
    public InstanceConfigurationTimelineAccess Remote { get; set; } = default;
}

public sealed class InstanceConfigurationUrls {
    [JsonPropertyName("streaming")]
    public Uri? Streaming { get; set; } = null;

    [JsonPropertyName("status")]
    public Uri? Status { get; set; } = null;

    [JsonPropertyName("about")]
    public Uri? About { get; set; } = null;

    [JsonPropertyName("privacy_policy")]
    public Uri? PrivacyPolicy { get; set; } = null;

    [JsonPropertyName("terms_of_service")]
    public Uri? TermsOfService { get; set; } = null;
}

public sealed class InstanceRegistrations {
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = false;

    [JsonPropertyName("approval_required")]
    public bool ApprovalRequired { get; set; } = true;

    [JsonPropertyName("message")]
    public string? Message { get; set; } = null;

    [JsonPropertyName("min_age")]
    public int? MinAge { get; set; } = null;

    [JsonPropertyName("reason_required")]
    public bool? ReasonRequired { get; set; } = null;

    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;
}

public sealed class InstanceApiVersions {
    [JsonPropertyName("mastodon")]
    public int Mastodon { get; set; } = 0;
}

public sealed class InstanceContact {
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("account")]
    public Account? Account { get; set; } = null;
}

public sealed class InstanceIcon {
    [JsonPropertyName("src")]
    public Uri? Src { get; set; } = null;

    [JsonPropertyName("size")]
    public string Size { get; set; } = string.Empty;
}