using Raiqub.Generators.EnumUtilities;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Account/">Mastodon API Documentation</see>
public class Account: PartialAccountWithAvatar {
    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#username"/>
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#uri"/>
    [JsonPropertyName("uri")]
    public Uri? Uri { get; set; } = null;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#display_name"/>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#note"/>
    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#header"/>
    [JsonPropertyName("header")]
    public Uri? Header { get; set; } = null;

    /// <summary>
    /// Version: 1.1.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#header_static"/>
    [JsonPropertyName("header_static")]
    public Uri? HeaderStatic { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#header_description"/>
    [JsonPropertyName("header_description")]
    public string HeaderDescription { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#fields"/>
    [JsonPropertyName("fields")]
    public List<AccountField> Fields { get; set; } = [];

    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#emojis"/>
    [JsonPropertyName("emojis")]
    public List<CustomEmoji> Emojis { get; set; } = [];

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#group"/>
    [JsonPropertyName("group")]
    public bool Group { get; set; } = false;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#discoverable"/>
    [JsonPropertyName("discoverable")]
    public bool? Discoverable { get; set; } = null;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#indexable"/>
    [JsonPropertyName("indexable")]
    public bool Indexable { get; set; } = false;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#noindex"/>
    [JsonPropertyName("noindex")]
    public bool? NoIndex { get; set; } = null;

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#moved"/>
    [JsonPropertyName("moved")]
    public Account? Moved { get; set; }

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#memorial"/>
    [JsonPropertyName("memorial")]
    public bool? Memorial { get; set; } = null;

    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#suspended"/>
    [JsonPropertyName("suspended")]
    public bool? Suspended { get; set; } = null;

    /// <summary>
    /// Version: 3.5.3
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#limited"/>
    [JsonPropertyName("limited")]
    public bool? Limited { get; set; } = null;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#last_status_at"/>
    [JsonPropertyName("last_status_at")]
    public DateTime? LastStatusAt { get; set; } = null;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#statuses_count"/>
    [JsonPropertyName("statuses_count")]
    public long StatusesCount { get; set; } = 0;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#followers_count"/>
    [JsonPropertyName("followers_count")]
    public long FollowersCount { get; set; } = 0;

    /// <summary>
    /// Version: 0.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#following_count"/>
    [JsonPropertyName("following_count")]
    public long FollowingCount { get; set; } = 0;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#hide_collections"/>
    [JsonPropertyName("hide_collections")]
    public bool? HideCollections { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#show_media"/>
    [JsonPropertyName("show_media")]
    public bool ShowMedia { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#show_media_replies"/>
    [JsonPropertyName("show_media_replies")]
    public bool ShowMediaReplies { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#show_featured"/>
    [JsonPropertyName("show_featured")]
    public bool ShowFeatured { get; set; } = false;

    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#roles"/>
    [JsonPropertyName("roles")]
    public List<AccountRole>? Roles { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#feature_approval"/>
    [JsonPropertyName("feature_approval")]
    public FeatureApproval FeatureApproval { get; set; } = new();

    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#source"/>
    [JsonPropertyName("source")]
    public CredentialAccount Source { get; set; } = new();

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#role"/>
    [JsonPropertyName("role")]
    public Role Role { get; set; } = new();
}

/// <see href="https://docs.joinmastodon.org/entities/Account/#MutedAccount">Mastodon API Documentation</see>
public sealed class MutedAccount: Account {
    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#mute_expires_at"/>
    [JsonPropertyName("mute_expires_at")]
    public DateTime? MuteExpiresAt { get; set; }
}

/// <see href="https://docs.joinmastodon.org/entities/Account/#CredentialAccount">Mastodon API Documentation</see>
public sealed class CredentialAccount {
    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#source-attribution_domains"/>
    [JsonPropertyName("attribution_domains")]
    public List<string> AttributionDomains { get; set; } = [];

    /// <summary>
    /// Version: 1.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#source-note"/>
    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#source-fields"/>
    [JsonPropertyName("fields")]
    public List<AccountField> Fields { get; set; } = [];

    /// <summary>
    /// Version: 1.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#source-privacy"/>
    [JsonPropertyName("privacy")]
    public StatusVisibility Privacy { get; set; } = StatusVisibility.Public;

    /// <summary>
    /// Version: 1.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#source-sensitive"/>
    [JsonPropertyName("sensitive")]
    public bool Sensitive { get; set; } = false;

    /// <summary>
    /// Version: 2.4.2
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#source-language"/>
    [JsonPropertyName("language")]
    public CultureInfo Language { get; set; } = CultureInfo.InvariantCulture;

    /// <summary>
    /// Version: 3.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#follow_requests_count"/>
    [JsonPropertyName("follow_requests_count")]
    public int FollowRequestsCount { get; set; } = 0;

    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#source-hide_collections"/>
    [JsonPropertyName("hide_collections")]
    public bool? HideCollections { get; set; } = null;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#source-discoverable"/>
    [JsonPropertyName("discoverable")]
    public bool? Discoverable { get; set; } = null;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#source-indexable"/>
    [JsonPropertyName("indexable")]
    public bool Indexable { get; set; } = false;

    /// <summary>
    /// Version: 4.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#source-quote_policy"/>
    [JsonPropertyName("quote_policy")]
    public QuotePolicy QuotePolicy { get; set; } = QuotePolicy.Nobody;
}

/// <see href="https://docs.joinmastodon.org/entities/Account/#PartialAccountWithAvatar">Mastodon API Documentation</see>
public class PartialAccountWithAvatar {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#id-1"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#acct-1"/>
    [JsonPropertyName("acct")]
    public string Acct { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#url-1"/>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#avatar-1"/>
    [JsonPropertyName("avatar")]
    public Uri? Avatar { get; set; } = null;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#avatar_static-1"/>
    [JsonPropertyName("avatar_static")]
    public Uri? AvatarStatic { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#avatar_description-1"/>
    [JsonPropertyName("avatar_description")]
    public string AvatarDescription { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#locked-1"/>
    [JsonPropertyName("locked")]
    public bool Locked { get; set; } = false;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#bot-1"/>
    [JsonPropertyName("bot")]
    public bool Bot { get; set; } = false;
}

public class AccountRole {
    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#accountrole-id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#accountrole-name"/>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#accountrole-color"/>
    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;
}

public sealed class AccountField {
    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#name"/>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#value"/>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Account/#verified_at"/>
    [JsonPropertyName("verified_at")]
    public DateTime? VerifiedAt { get; set; }
}

[JsonConverterGenerator]
public enum QuotePolicy {
    [JsonStringEnumMemberName("public")]
    Public,

    [JsonStringEnumMemberName("followers")]
    Followers,

    [JsonStringEnumMemberName("nobody")]
    Nobody,
}
