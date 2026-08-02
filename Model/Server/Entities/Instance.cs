using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Instance/">Mastodon API Documentation</see>
public sealed class Instance {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#domain"/>
    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#title"/>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#version"/>
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#source_url"/>
    [JsonPropertyName("source_url")]
    public Uri? SourceUrl { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#description"/>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#wrapstodon"/>
    [JsonPropertyName("wrapstodon")]
    public string? Wrapstodon { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#usage"/>
    [JsonPropertyName("usage")]
    public InstanceUsage Usage { get; set; } = new();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#thumbnail"/>
    [JsonPropertyName("thumbnail")]
    public InstanceThumbnail Thumbnail { get; set; } = new();

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#icon"/>
    [JsonPropertyName("icon")]
    public List<InstanceIcon> Icon { get; set; } = [];

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#languages"/>
    [JsonPropertyName("languages")]
    public List<CultureInfo> Languages { get; set; } = [];

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#configuration"/>
    [JsonPropertyName("configuration")]
    public InstanceConfiguration Configuration { get; set; } = new();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#registrations"/>
    [JsonPropertyName("registrations")]
    public InstanceRegistrations Registrations { get; set; } = new();

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#api-versions"/>
    [JsonPropertyName("api_versions")]
    public InstanceApiVersions ApiVersions { get; set; } = new();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#contact"/>
    [JsonPropertyName("contact")]
    public InstanceContact Contact { get; set; } = new();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#rules"/>
    [JsonPropertyName("rules")]
    public List<Rule> Rules { get; set; } = [];
}

public sealed class InstanceUsage {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#users"/>
    [JsonPropertyName("users")]
    public InstanceUsageUsers Users { get; set; } = new();
}

public sealed class InstanceUsageUsers {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#active_month"/>
    [JsonPropertyName("active_month")]
    public int ActiveMonth { get; set; } = 0;
}

public sealed class InstanceThumbnail {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#thumbnail-url"/>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#thumbnail-description"/>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#blurhash"/>
    [JsonPropertyName("blurhash")]
    public string? Blurhash { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#thumbnail-versions"/>
    [JsonPropertyName("versions")]
    public InstanceThumbnailVersions Versions { get; set; } = new();
}

public sealed class InstanceThumbnailVersions {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#1x"/>
    [JsonPropertyName("@1x")]
    public Uri? OneX { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#2x"/>
    [JsonPropertyName("@2x")]
    public Uri? TwoX { get; set; } = null;
}

public sealed class InstanceConfiguration {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#urls"/>
    [JsonPropertyName("urls")]
    public InstanceConfigurationUrls Urls { get; set; } = new();

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#vapid_public_key"/>
    [JsonPropertyName("vapid")]
    public InstanceConfigurationVapid Vapid { get; set; } = new();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#accounts"/>
    [JsonPropertyName("accounts")]
    public InstanceConfigurationAccounts Accounts { get; set; } = new();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#statuses"/>
    [JsonPropertyName("statuses")]
    public InstanceConfigurationStatuses Statuses { get; set; } = new();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#media_attachments"/>
    [JsonPropertyName("media_attachments")]
    public InstanceConfigurationMediaAttachments MediaAttachments { get; set; } = new();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#polls"/>
    [JsonPropertyName("polls")]
    public InstanceConfigurationPolls Polls { get; set; } = new();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#translation"/>
    [JsonPropertyName("translation")]
    public InstanceConfigurationTranslation Translation { get; set; } = new();

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#timelines_access"/>
    [JsonPropertyName("timelines_access")]
    public InstanceConfigurationTimelinesAccess TimelinesAccess { get; set; } = new();

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#limited-federation"/>
    [JsonPropertyName("limited_federation")]
    public bool LimitedFederation { get; set; } = false;
}

public sealed class InstanceConfigurationVapid {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#vapid_public_key"/>
    [JsonPropertyName("public_key")]
    public string PublicKey { get; set; } = string.Empty;
}

public sealed class InstanceConfigurationAccounts {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#max_display_name_length"/>
    [JsonPropertyName("max_display_name_length")]
    public int MaxDisplayNameLength { get; set; } = 0;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#max_note_length"/>
    [JsonPropertyName("max_note_length")]
    public int MaxNoteLength { get; set; } = 0;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#max_avatar_description_length"/>
    [JsonPropertyName("max_avatar_description_length")]
    public int MaxAvatarDescriptionLength { get; set; } = 0;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#max_header_description_length"/>
    [JsonPropertyName("max_header_description_length")]
    public int MaxHeaderDescriptionLength { get; set; } = 0;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#max_featured_tags"/>
    [JsonPropertyName("max_featured_tags")]
    public int MaxFeaturedTags { get; set; } = 0;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#max_pinned_statuses"/>
    [JsonPropertyName("max_pinned_statuses")]
    public int MaxPinnedStatuses { get; set; } = 0;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#max_profile_fields"/>
    [JsonPropertyName("max_profile_fields")]
    public int MaxProfileFields { get; set; } = 0;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#profile_field_name_limit"/>
    [JsonPropertyName("profile_field_name_limit")]
    public int ProfileFieldNameLimit { get; set; } = 0;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#profile_field_value_limit"/>
    [JsonPropertyName("profile_field_value_limit")]
    public int ProfileFieldValueLimit { get; set; } = 0;
}

public sealed class InstanceConfigurationStatuses {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#max_characters"/>
    [JsonPropertyName("max_characters")]
    public int MaxCharacters { get; set; } = 0;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#max_media_attachments"/>
    [JsonPropertyName("max_media_attachments")]
    public int MaxMediaAttachments { get; set; } = 0;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#characters_reserved_per_url"/>
    [JsonPropertyName("characters_reserved_per_url")]
    public int CharactersReservedPerUrl { get; set; } = 0;
}

public sealed class InstanceConfigurationMediaAttachments {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#supported_mime_types"/>
    [JsonPropertyName("supported_mime_types")]
    public List<string> SupportedMimeTypes { get; set; } = [];

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#description_limit"/>
    [JsonPropertyName("description_limit")]
    public int DescriptionLimit { get; set; } = 0;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#image_size_limit"/>
    [JsonPropertyName("image_size_limit")]
    public int ImageSizeLimit { get; set; } = 0;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#image_matrix_limit"/>
    [JsonPropertyName("image_matrix_limit")]
    public int ImageMatrixLimit { get; set; } = 0;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#video_size_limit"/>
    [JsonPropertyName("video_size_limit")]
    public int VideoSizeLimit { get; set; } = 0;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#video_frame_rate_limit"/>
    [JsonPropertyName("video_frame_rate_limit")]
    public int VideoFrameRateLimit { get; set; } = 0;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#video_matrix_limit"/>
    [JsonPropertyName("video_matrix_limit")]
    public int VideoMatrixLimit { get; set; } = 0;
}

public sealed class InstanceConfigurationPolls {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#max_options"/>
    [JsonPropertyName("max_options")]
    public int MaxOptions { get; set; } = 0;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#max_characters_per_option"/>
    [JsonPropertyName("max_characters_per_option")]
    public int MaxCharactersPerOption { get; set; } = 0;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#min_expiration"/>
    [JsonPropertyName("min_expiration")]
    public int MinExpiration { get; set; } = 0;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#max_expiration"/>
    [JsonPropertyName("max_expiration")]
    public int MaxExpiration { get; set; } = 0;
}

public sealed class InstanceConfigurationTranslation {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#translation-enabled"/>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = false;
}

public sealed class InstanceConfigurationTimelinesAccess {
    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#timelines_access-live_feeds"/>
    [JsonPropertyName("live_feeds")]
    public InstanceConfigurationTimelinesAccessFeeds LiveFeeds { get; set; } = new();

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#timelines_access-hashtag_feeds"/>
    [JsonPropertyName("hashtag_feeds")]
    public InstanceConfigurationTimelinesAccessFeeds HashtagFeeds { get; set; } = new();

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#timelines_access-trending_link_feeds"/>
    [JsonPropertyName("trending_link_feeds")]
    public InstanceConfigurationTimelinesAccessFeeds TrendingLinkFeeds { get; set; } = new();
}

public sealed class InstanceConfigurationTimelinesAccessFeeds {
    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#timelines_access-live_feeds-local"/>
    [JsonPropertyName("local")]
    public InstanceConfigurationTimelineAccess Local { get; set; } = InstanceConfigurationTimelineAccess.Public;

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#timelines_access-live_feeds-remote"/>
    [JsonPropertyName("remote")]
    public InstanceConfigurationTimelineAccess Remote { get; set; } = InstanceConfigurationTimelineAccess.Public;
}

public sealed class InstanceConfigurationUrls {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#streaming"/>
    [JsonPropertyName("streaming")]
    public Uri? Streaming { get; set; } = null;

    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#status_url"/>
    [JsonPropertyName("status")]
    public Uri? Status { get; set; } = null;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#about_url"/>
    [JsonPropertyName("about")]
    public Uri? About { get; set; } = null;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#privacy_policy"/>
    [JsonPropertyName("privacy_policy")]
    public Uri? PrivacyPolicy { get; set; } = null;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#terms_of_service"/>
    [JsonPropertyName("terms_of_service")]
    public Uri? TermsOfService { get; set; } = null;
}

public sealed class InstanceRegistrations {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#registrations-enabled"/>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#approval_required"/>
    [JsonPropertyName("approval_required")]
    public bool ApprovalRequired { get; set; } = true;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#registrations-message"/>
    [JsonPropertyName("message")]
    public string? Message { get; set; } = null;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#registrations-min_age"/>
    [JsonPropertyName("min_age")]
    public int? MinAge { get; set; } = null;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#registrations-reason_required"/>
    [JsonPropertyName("reason_required")]
    public bool? ReasonRequired { get; set; } = null;

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#registrations-url"/>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;
}

public sealed class InstanceApiVersions {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#api-versions"/>
    [JsonPropertyName("mastodon")]
    public int Mastodon { get; set; } = 0;
}

public sealed class InstanceContact {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#contact-email"/>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#contact-account"/>
    [JsonPropertyName("account")]
    public Account? Account { get; set; } = null;
}

public sealed class InstanceIcon {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#src"/>
    [JsonPropertyName("src")]
    public Uri? Src { get; set; } = null;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Instance/#size"/>
    [JsonPropertyName("size")]
    public string Size { get; set; } = string.Empty;
}

public enum InstanceConfigurationTimelineAccess {
    [JsonStringEnumMemberName("public")]
    Public,

    [JsonStringEnumMemberName("authenticated")]
    Authenticated,

    [JsonStringEnumMemberName("disabled")]
    Disabled,
}