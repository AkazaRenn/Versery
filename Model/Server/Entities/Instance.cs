using Model.Server.Entities.Enumerations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents the software instance of Mastodon running on this domain.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Instance/">Mastodon API Documentation</see>
public class Instance {
    /// <summary>
    /// The WebFinger domain name of the server.
    /// </summary>
    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// The title of the website.
    /// </summary>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The version of Mastodon installed on the server.
    /// </summary>
    [JsonPropertyName("version")]
    public string Version { get; set; } = string.Empty;

    /// <summary>
    /// The URL for the source code of the software running on this server, per the AGPL license requirements.
    /// </summary>
    [JsonPropertyName("source_url")]
    public Uri? SourceUrl { get; set; } = null;

    /// <summary>
    /// A short, plain-text description defined by the admin.
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The current Wrapstodon (Annual report campaign identifier (year), if any.
    /// </summary>
    [JsonPropertyName("wrapstodon")]
    public string? Wrapstodon { get; set; } = null;

    /// <summary>
    /// Usage data for this server.
    /// </summary>
    [JsonPropertyName("usage")]
    public InstanceUsage Usage { get; set; } = new();

    /// <summary>
    /// An image used to represent this server.
    /// </summary>
    [JsonPropertyName("thumbnail")]
    public InstanceThumbnail Thumbnail { get; set; } = new();

    /// <summary>
    /// The list of available size variants for this server’s configured icon.
    /// </summary>
    [JsonPropertyName("icon")]
    public IEnumerable<InstanceIcon> Icon { get; set; } = [];

    /// <summary>
    /// Primary languages of the website and its staff.
    /// </summary>
    [JsonPropertyName("languages")]
    public IEnumerable<CultureInfo> Languages { get; set; } = [];

    /// <summary>
    /// Configured values and limits for this website.
    /// </summary>
    [JsonPropertyName("configuration")]
    public InstanceConfiguration Configuration { get; set; } = new();

    /// <summary>
    /// Information about registering for this website.
    /// </summary>
    [JsonPropertyName("registrations")]
    public InstanceRegistrations Registrations { get; set; } = new();

    /// <summary>
    /// Machine-readable API version information that allows clients to determine which API endpoints and features are available on this server. This provides a more reliable method for capability detection than parsing human-readable version strings, especially for forks and development builds. It contains at least a mastodon attribute, and other implementations may have their own additional attributes.
    /// </summary>
    [JsonPropertyName("api_versions")]
    public InstanceApiVersions ApiVersions { get; set; } = new();

    /// <summary>
    /// Hints related to contacting a representative of the website.
    /// </summary>
    [JsonPropertyName("contact")]
    public InstanceContact Contact { get; set; } = new();

    /// <summary>
    /// An itemized list of rules for this website.
    /// </summary>
    [JsonPropertyName("rules")]
    public IEnumerable<Rule> Rules { get; set; } = [];
}

public class InstanceUsage {
    /// <summary>
    /// Usage data related to users on this server.
    /// </summary>
    [JsonPropertyName("users")]
    public InstanceUsageUsers Users { get; set; } = new();
}

public class InstanceUsageUsers {
    /// <summary>
    /// The number of active users in the past 4 weeks. This is set to zero for server with configuration[limited_federation].
    /// </summary>
    [JsonPropertyName("active_month")]
    public int ActiveMonth { get; set; } = 0;
}

public class InstanceThumbnail {
    /// <summary>
    /// The URL for the thumbnail image.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// The thumbnail’s alt text (a description of the image to help people with visual impairments understand its content).
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// A hash computed by the BlurHash algorithm, for generating colorful preview thumbnails when media has not been downloaded yet.
    /// </summary>
    [JsonPropertyName("blurhash")]
    public string? Blurhash { get; set; } = null;

    /// <summary>
    /// Links to scaled resolution images, for high DPI screens.
    /// </summary>
    [JsonPropertyName("versions")]
    public InstanceThumbnailVersions Versions { get; set; } = new();
}

public class InstanceThumbnailVersions {
    /// <summary>
    /// The URL for the thumbnail image at 1x resolution.
    /// </summary>
    [JsonPropertyName("@1x")]
    public Uri? OneX { get; set; } = null;

    /// <summary>
    /// The URL for the thumbnail image at 2x resolution.
    /// </summary>
    [JsonPropertyName("@2x")]
    public Uri? TwoX { get; set; } = null;
}

public class InstanceConfiguration {
    /// <summary>
    /// Configured values and limits for this website.
    /// </summary>
    [JsonPropertyName("urls")]
    public InstanceConfigurationUrls Urls { get; set; } = new();

    /// <summary>
    /// The server’s VAPID public key, used for push notifications, the same as WebPushSubscription#server_key.
    /// </summary>
    [JsonPropertyName("vapid")]
    public InstanceConfigurationVapid Vapid { get; set; } = new();

    /// <summary>
    /// Limits related to accounts.
    /// </summary>
    [JsonPropertyName("accounts")]
    public InstanceConfigurationAccounts Accounts { get; set; } = new();

    /// <summary>
    /// Limits related to authoring statuses.
    /// </summary>
    [JsonPropertyName("statuses")]
    public InstanceConfigurationStatuses Statuses { get; set; } = new();

    /// <summary>
    /// Hints for which attachments will be accepted.
    /// </summary>
    [JsonPropertyName("media_attachments")]
    public InstanceConfigurationMediaAttachments MediaAttachments { get; set; } = new();

    /// <summary>
    /// Limits related to polls.
    /// </summary>
    [JsonPropertyName("polls")]
    public InstanceConfigurationPolls Polls { get; set; } = new();

    /// <summary>
    /// Hints related to translation.
    /// </summary>
    [JsonPropertyName("translation")]
    public InstanceConfigurationTranslation Translation { get; set; } = new();

    /// <summary>
    /// Access restrictions on different timelines.
    /// </summary>
    [JsonPropertyName("timelines_access")]
    public InstanceConfigurationTimelinesAccess TimelinesAccess { get; set; } = new();

    /// <summary>
    /// Whether federation is limited to explicitly allowed domains.
    /// </summary>
    [JsonPropertyName("limited_federation")]
    public bool LimitedFederation { get; set; } = false;
}

public class InstanceConfigurationVapid {
    /// <summary>
    /// The server’s VAPID public key, used for push notifications, the same as WebPushSubscription#server_key.
    /// </summary>
    [JsonPropertyName("public_key")]
    public string PublicKey { get; set; } = string.Empty;
}

public class InstanceConfigurationAccounts {
    /// <summary>
    /// The maximum length allowed for an account’s display name.
    /// </summary>
    [JsonPropertyName("max_display_name_length")]
    public int MaxDisplayNameLength { get; set; } = 0;

    /// <summary>
    /// The maximum length allowed for an account’s bio.
    /// </summary>
    [JsonPropertyName("max_note_length")]
    public int MaxNoteLength { get; set; } = 0;

    /// <summary>
    /// The maximum length allowed for an account’s avatar description.
    /// </summary>
    [JsonPropertyName("max_avatar_description_length")]
    public int MaxAvatarDescriptionLength { get; set; } = 0;

    /// <summary>
    /// The maximum length allowed for an account’s profile header description.
    /// </summary>
    [JsonPropertyName("max_header_description_length")]
    public int MaxHeaderDescriptionLength { get; set; } = 0;

    /// <summary>
    /// The maximum number of featured tags allowed for each account.
    /// </summary>
    [JsonPropertyName("max_featured_tags")]
    public int MaxFeaturedTags { get; set; } = 0;

    /// <summary>
    /// The maximum number of pinned statuses for each account.
    /// </summary>
    [JsonPropertyName("max_pinned_statuses")]
    public int MaxPinnedStatuses { get; set; } = 0;

    /// <summary>
    /// The maximum number of custom profile fields allowed to be set.
    /// </summary>
    [JsonPropertyName("max_profile_fields")]
    public int MaxProfileFields { get; set; } = 0;

    /// <summary>
    /// The maximum size of a profile field name, in characters.
    /// </summary>
    [JsonPropertyName("profile_field_name_limit")]
    public int ProfileFieldNameLimit { get; set; } = 0;

    /// <summary>
    /// The maximum size of a profile field value, in characters.
    /// </summary>
    [JsonPropertyName("profile_field_value_limit")]
    public int ProfileFieldValueLimit { get; set; } = 0;
}

public class InstanceConfigurationStatuses {
    /// <summary>
    /// The maximum number of allowed characters per status.
    /// </summary>
    [JsonPropertyName("max_characters")]
    public int MaxCharacters { get; set; } = 0;

    /// <summary>
    /// The maximum number of media attachments that can be added to a status.
    /// </summary>
    [JsonPropertyName("max_media_attachments")]
    public int MaxMediaAttachments { get; set; } = 0;

    /// <summary>
    /// Each URL in a status will be assumed to be exactly this many characters.
    /// </summary>
    [JsonPropertyName("characters_reserved_per_url")]
    public int CharactersReservedPerUrl { get; set; } = 0;
}

public class InstanceConfigurationMediaAttachments {
    /// <summary>
    /// Contains MIME types that can be uploaded.
    /// </summary>
    [JsonPropertyName("supported_mime_types")]
    public IEnumerable<string> SupportedMimeTypes { get; set; } = [];

    /// <summary>
    /// The maximum size of a description, in characters.
    /// </summary>
    [JsonPropertyName("description_limit")]
    public int DescriptionLimit { get; set; } = 0;

    /// <summary>
    /// The maximum size of any uploaded image, in bytes.
    /// </summary>
    [JsonPropertyName("image_size_limit")]
    public int ImageSizeLimit { get; set; } = 0;

    /// <summary>
    /// The maximum number of pixels (width times height) for image uploads.
    /// </summary>
    [JsonPropertyName("image_matrix_limit")]
    public int ImageMatrixLimit { get; set; } = 0;

    /// <summary>
    /// The maximum size of any uploaded video, in bytes.
    /// </summary>
    [JsonPropertyName("video_size_limit")]
    public int VideoSizeLimit { get; set; } = 0;

    /// <summary>
    /// The maximum frame rate for any uploaded video.
    /// </summary>
    [JsonPropertyName("video_frame_rate_limit")]
    public int VideoFrameRateLimit { get; set; } = 0;

    /// <summary>
    /// The maximum number of pixels (width times height) for video uploads.
    /// </summary>
    [JsonPropertyName("video_matrix_limit")]
    public int VideoMatrixLimit { get; set; } = 0;
}

public class InstanceConfigurationPolls {
    /// <summary>
    /// Each poll is allowed to have up to this many options.
    /// </summary>
    [JsonPropertyName("max_options")]
    public int MaxOptions { get; set; } = 0;

    /// <summary>
    /// Each poll option is allowed to have this many characters.
    /// </summary>
    [JsonPropertyName("max_characters_per_option")]
    public int MaxCharactersPerOption { get; set; } = 0;

    /// <summary>
    /// The shortest allowed poll duration, in seconds.
    /// </summary>
    [JsonPropertyName("min_expiration")]
    public int MinExpiration { get; set; } = 0;

    /// <summary>
    /// The longest allowed poll duration, in seconds.
    /// </summary>
    [JsonPropertyName("max_expiration")]
    public int MaxExpiration { get; set; } = 0;
}

public class InstanceConfigurationTranslation {
    /// <summary>
    /// Whether the Translations API is available on this server.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = false;
}

public class InstanceConfigurationTimelinesAccess {
    /// <summary>
    /// Access restrictions on public feeds.
    /// </summary>
    [JsonPropertyName("live_feeds")]
    public InstanceConfigurationTimelinesAccessFeeds LiveFeeds { get; set; } = new();

    /// <summary>
    /// Access restrictions on hashtag feeds.
    /// </summary>
    [JsonPropertyName("hashtag_feeds")]
    public InstanceConfigurationTimelinesAccessFeeds HashtagFeeds { get; set; } = new();

    /// <summary>
    /// Access restrictions on trending link feeds.
    /// </summary>
    [JsonPropertyName("trending_link_feeds")]
    public InstanceConfigurationTimelinesAccessFeeds TrendingLinkFeeds { get; set; } = new();
}

public class InstanceConfigurationTimelinesAccessFeeds {
    /// <summary>
    /// Access restrictions for local posts in the public feed.
    /// </summary>
    [JsonPropertyName("local")]
    public InstanceConfigurationTimelineAccess Local { get; set; } = default;

    /// <summary>
    /// Access restrictions for remote posts in the public feed.
    /// </summary>
    [JsonPropertyName("remote")]
    public InstanceConfigurationTimelineAccess Remote { get; set; } = default;
}

public class InstanceConfigurationUrls {
    /// <summary>
    /// The Websockets URL for connecting to the streaming API.
    /// </summary>
    [JsonPropertyName("streaming")]
    public Uri? Streaming { get; set; } = null;

    /// <summary>
    /// The URL of the server’s status page, if configured.
    /// </summary>
    [JsonPropertyName("status")]
    public Uri? Status { get; set; } = null;

    /// <summary>
    /// The URL of the server’s about page.
    /// </summary>
    [JsonPropertyName("about")]
    public Uri? About { get; set; } = null;

    /// <summary>
    /// The URL of the server’s privacy policy.
    /// </summary>
    [JsonPropertyName("privacy_policy")]
    public Uri? PrivacyPolicy { get; set; } = null;

    /// <summary>
    /// The URL of the server’s current terms of service, if any.
    /// </summary>
    [JsonPropertyName("terms_of_service")]
    public Uri? TermsOfService { get; set; } = null;
}

public class InstanceRegistrations {
    /// <summary>
    /// Whether registrations are enabled. This will be false if registrations_mode is none or if the server is in single_user_mode.
    /// </summary>
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; } = false;

    /// <summary>
    /// Whether registrations require moderator approval.
    /// </summary>
    [JsonPropertyName("approval_required")]
    public bool ApprovalRequired { get; set; } = true;

    /// <summary>
    /// A custom message to be shown when registrations are closed. Will be null if registrations are open.
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; } = null;

    /// <summary>
    /// A minimum age required to register, if configured.
    /// </summary>
    [JsonPropertyName("min_age")]
    public int? MinAge { get; set; } = null;

    /// <summary>
    /// Whether registrations require the user to provide a reason for joining. Only applicable when registrations[approval_required] is true.
    /// </summary>
    [JsonPropertyName("reason_required")]
    public bool? ReasonRequired { get; set; } = null;

    /// <summary>
    /// A custom URL for account registration, when using external authentication.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;
}

public class InstanceApiVersions {
    /// <summary>
    /// API version number that increments with substantial API changes. Clients can use this value to determine API compatibility rather than parsing complex version strings like “4.4+hometown-123” from forks or nightly builds. This number increases independently of the human-readable version number.
    /// </summary>
    [JsonPropertyName("mastodon")]
    public int Mastodon { get; set; } = 0;
}

public class InstanceContact {
    /// <summary>
    /// An email address that can be messaged regarding inquiries or issues.
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// An account that can be contacted natively over the network regarding inquiries or issues.
    /// </summary>
    [JsonPropertyName("account")]
    public Account? Account { get; set; } = null;
}

public class InstanceIcon {
    /// <summary>
    /// The URL of this icon.
    /// </summary>
    [JsonPropertyName("src")]
    public Uri? Src { get; set; } = null;

    /// <summary>
    /// The size of this icon.
    /// </summary>
    [JsonPropertyName("size")]
    public string Size { get; set; } = string.Empty;
}