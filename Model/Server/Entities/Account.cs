using Model.Server.Entities.Enumerations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents a user of Mastodon and their associated profile.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Account/">Mastodon API Documentation</see>
public class Account: PartialAccountWithAvatar {
    #region Attributes
    /// <summary>
    /// The username of the account, not including domain.
    /// </summary>
    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// The user’s ActivityPub actor identifier (used for federation).
    /// </summary>
    [JsonPropertyName("uri")]
    public string Uri { get; set; } = string.Empty;

    /// <summary>
    /// The profile's display name.
    /// </summary>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// The profile's bio or description.
    /// </summary>
    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;

    /// <summary>
    /// An image banner that is shown above the profile and in profile cards. Will end /headers/original/missing.png if the user has not set a header image.
    /// </summary>
    [JsonPropertyName("header")]
    public string Header { get; set; } = string.Empty;

    /// <summary>
    /// A static version of the header. Equal to header if its value is a static image; different if header is an animated GIF.
    /// </summary>
    [JsonPropertyName("header_static")]
    public string HeaderStatic { get; set; } = string.Empty;

    /// <summary>
    /// A textual description of the header image.
    /// </summary>
    [JsonPropertyName("header_description")]
    public string HeaderDescription { get; set; } = string.Empty;

    /// <summary>
    /// Additional metadata attached to a profile as name-value pairs.
    /// </summary>
    [JsonPropertyName("fields")]
    public IEnumerable<Field> Fields { get; set; } = [];

    /// <summary>
    /// Custom emoji entities to be used when rendering the profile. If none, an empty array will be returned.
    /// </summary>
    [JsonPropertyName("emojis")]
    public IEnumerable<CustomEmoji> Emojis { get; set; } = [];

    /// <summary>
    /// Indicates that the account represents a Group actor.
    /// </summary>
    [JsonPropertyName("group")]
    public bool Group { get; set; } = false;

    /// <summary>
    /// Whether the account has opted into discovery features such as the profile directory.
    /// </summary>
    [JsonPropertyName("discoverable")]
    public bool? Discoverable { get; set; } = null;

    /// <summary>
    /// Whether the account allows indexing by search engines.
    /// </summary>
    [JsonPropertyName("indexable")]
    public bool Indexable { get; set; } = false;

    /// <summary>
    /// Whether the local user has opted out of being indexed by search engines.
    /// </summary>
    [JsonPropertyName("noindex")]
    public bool? NoIndex { get; set; } = null;

    /// <summary>
    /// Indicates that the profile is currently inactive and that its user has moved to a new account.
    /// </summary>
    [JsonPropertyName("moved")]
    public Account? Moved { get; set; }

    /// <summary>
    /// An extra attribute returned only when an account is memorialized (when memorial is true).
    /// </summary>
    [JsonPropertyName("memorial")]
    public bool? Memorial { get; set; } = null;

    /// <summary>
    /// An extra attribute returned only when an account is suspended.
    /// </summary>
    [JsonPropertyName("suspended")]
    public bool? Suspended { get; set; } = null;

    /// <summary>
    /// An extra attribute returned only when an account is silenced. If true, indicates that the account should be hidden behind a warning screen.
    /// </summary>
    [JsonPropertyName("limited")]
    public bool? Limited { get; set; } = null;

    /// <summary>
    /// The time the account was created
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    /// <summary>
    /// When the most recent status was posted.
    /// </summary>
    [JsonPropertyName("last_status_at")]
    public DateTime? LastStatusAt { get; set; } = null;

    /// <summary>
    /// How many statuses are attached to this account.
    /// </summary>
    [JsonPropertyName("statuses_count")]
    public long StatusesCount { get; set; } = 0;

    /// <summary>
    /// The reported followers of this profile.
    /// </summary>
    [JsonPropertyName("followers_count")]
    public long FollowersCount { get; set; } = 0;

    /// <summary>
    /// The reported follows of this profile.
    /// </summary>
    [JsonPropertyName("following_count")]
    public long FollowingCount { get; set; } = 0;

    /// <summary>
    /// Whether the user hides the contents of their follows and followers collections.
    /// </summary>
    [JsonPropertyName("hide_collections")]
    public bool? HideCollections { get; set; } = null;

    /// <summary>
    /// Whether the account wishes to have a “Media” tab with media attachments on their profile.
    /// </summary>
    [JsonPropertyName("show_media")]
    public bool ShowMedia { get; set; } = false;

    /// <summary>
    /// Whether the account wishes to have replies in the “Media” tab on their profile.
    /// </summary>
    [JsonPropertyName("show_media_replies")]
    public bool ShowMediaReplies { get; set; } = false;

    /// <summary>
    /// Whether the account wishes to have a “Featured” tab on their profile.
    /// </summary>
    [JsonPropertyName("show_featured")]
    public bool ShowFeatured { get; set; } = false;

    /// <summary>
    /// An array of roles assigned to the user that are publicly visible (highlighted roles only), if the account is local. Will be an empty array if no roles are highlighted or null if the account is remote.
    /// </summary>
    [JsonPropertyName("roles")]
    public IEnumerable<Role>? Roles { get; set; } = null;

    /// <summary>
    /// Summary of the account’s policy with regards to being featured in a Collection and how it applies to the user making the request.
    /// </summary>
    [JsonPropertyName("feature_approval")]
    public FeatureApproval FeatureApproval { get; set; } = new();
    #endregion

    /// <summary>
    /// An extra entity to be used with API methods to verify credentials and update credentials.
    /// </summary>
    [JsonPropertyName("source")]
    public CredentialAccount Source { get; set; } = new();

    /// <summary>
    /// The complete role assigned to the currently authorized user, including permissions and highlighted status.
    /// </summary>
    [JsonPropertyName("role")]
    public Role Role { get; set; } = new();
}

/// <summary>
/// Version: 3.3.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Account/#MutedAccount">Mastodon API Documentation</see>
public class MutedAccount: Account {
    /// <summary>
    /// When the mute will expire, if applicable.
    /// </summary>
    [JsonPropertyName("mute_expires_at")]
    public DateTime? MuteExpiresAt { get; set; }
}

/// <summary>
/// Version: 4.5.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Account/#CredentialAccount">Mastodon API Documentation</see>
public class CredentialAccount {
    /// <summary>
    /// Domains of websites allowed to credit the account.
    /// </summary>
    [JsonPropertyName("attribution_domains")]
    public IEnumerable<string> AttributionDomains { get; set; } = [];

    /// <summary>
    /// Profile bio, in plain text instead of HTML.
    /// </summary>
    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;

    /// <summary>
    /// Metadata about the account.
    /// </summary>
    [JsonPropertyName("fields")]
    public IEnumerable<Field> Fields { get; set; } = [];

    /// <summary>
    /// The default post privacy to be used for new statuses.
    /// </summary>
    [JsonPropertyName("privacy")]
    public StatusVisibility Privacy { get; set; } = StatusVisibility.Public;

    /// <summary>
    /// Whether new statuses should be marked sensitive by default.
    /// </summary>
    [JsonPropertyName("sensitive")]
    public bool Sensitive { get; set; } = false;

    /// <summary>
    /// The default posting language for new statuses.
    /// </summary>
    [JsonPropertyName("language")]
    public CultureInfo Language { get; set; } = CultureInfo.InvariantCulture;

    /// <summary>
    /// The number of pending follow requests.
    /// </summary>
    [JsonPropertyName("follow_requests_count")]
    public int FollowRequestsCount { get; set; } = 0;

    /// <summary>
    /// Whether the user hides the contents of their follows and followers collections.
    /// </summary>
    [JsonPropertyName("hide_collections")]
    public bool? HideCollections { get; set; } = null;

    /// <summary>
    /// Whether the account has opted into discovery features such as the profile directory.
    /// </summary>
    [JsonPropertyName("discoverable")]
    public bool? Discoverable { get; set; } = null;

    /// <summary>
    /// Whether public posts should be searchable to anyone.
    /// </summary>
    [JsonPropertyName("indexable")]
    public bool Indexable { get; set; } = false;

    /// <summary>
    /// The default quote policy to be used for new statuses.
    /// </summary>
    [JsonPropertyName("quote_policy")]
    public QuotePolicy QuotePolicy { get; set; } = QuotePolicy.Nobody;

    /// <summary>
    /// The complete role assigned to the currently authorized user, including permissions and highlighted status.
    /// </summary>
    [JsonPropertyName("role")]
    public Role Role { get; set; } = new();
}

/// <summary>
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Account/#PartialAccountWithAvatar">Mastodon API Documentation</see>
public class PartialAccountWithAvatar {
    /// <summary>
    /// The account id.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The WebFinger account URI. Equal to username for local users, or username@domain for remote users.
    /// </summary>
    [JsonPropertyName("acct")]
    public string Acct { get; set; } = string.Empty;

    /// <summary>
    /// The location of the user's profile page.
    /// </summary>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// An image icon that is shown next to statuses and in the profile.
    /// </summary>
    [JsonPropertyName("avatar")]
    public Uri? Avatar { get; set; } = null;

    /// <summary>
    /// A static version of the avatar. Equal to avatar if its value is a static image; different if avatar is an animated GIF.
    /// </summary>
    [JsonPropertyName("avatar_static")]
    public Uri? AvatarStatic { get; set; } = null;

    /// <summary>
    /// A textual description of the avatar image.
    /// </summary>
    [JsonPropertyName("avatar_description")]
    public string AvatarDescription { get; set; } = string.Empty;

    /// <summary>
    /// Whether the account manually approves follow requests.
    /// </summary>
    [JsonPropertyName("locked")]
    public bool Locked { get; set; } = false;

    /// <summary>
    /// Indicates that the account may perform automated actions, may not be monitored, or identifies as a robot.
    /// </summary>
    [JsonPropertyName("bot")]
    public bool Bot { get; set; } = false;
}

/// <summary>
/// Version: 4.1.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Account/#AccountRole">Mastodon API Documentation</see>
public class AccountRole {
    /// <summary>
    /// The ID of the Role in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The name of the role.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The hex code assigned to this role. If no hex code is assigned, the string will be empty.
    /// </summary>
    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;
}

/// <summary>
/// Version: 2.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Account/#Field">Mastodon API Documentation</see>
public class Field {
    /// <summary>
    /// The key of a given field's key-value pair.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The value associated with the name key.
    /// </summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Timestamp of when the server verified a URL value for a rel="me” link.
    /// </summary>
    [JsonPropertyName("verified_at")]
    public DateTime? VerifiedAt { get; set; }
}
