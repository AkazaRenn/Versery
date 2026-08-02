using Model.Server.Entities.Enumerations;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Account/">Mastodon API Documentation</see>
public class Account: PartialAccountWithAvatar {
    #region Attributes
    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("uri")]
    public string Uri { get; set; } = string.Empty;

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;

    [JsonPropertyName("header")]
    public string Header { get; set; } = string.Empty;

    [JsonPropertyName("header_static")]
    public string HeaderStatic { get; set; } = string.Empty;

    [JsonPropertyName("header_description")]
    public string HeaderDescription { get; set; } = string.Empty;

    [JsonPropertyName("fields")]
    public IEnumerable<Field> Fields { get; set; } = [];

    [JsonPropertyName("emojis")]
    public IEnumerable<CustomEmoji> Emojis { get; set; } = [];

    [JsonPropertyName("group")]
    public bool Group { get; set; } = false;

    [JsonPropertyName("discoverable")]
    public bool? Discoverable { get; set; } = null;

    [JsonPropertyName("indexable")]
    public bool Indexable { get; set; } = false;

    [JsonPropertyName("noindex")]
    public bool? NoIndex { get; set; } = null;

    [JsonPropertyName("moved")]
    public Account? Moved { get; set; }

    [JsonPropertyName("memorial")]
    public bool? Memorial { get; set; } = null;

    [JsonPropertyName("suspended")]
    public bool? Suspended { get; set; } = null;

    [JsonPropertyName("limited")]
    public bool? Limited { get; set; } = null;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("last_status_at")]
    public DateTime? LastStatusAt { get; set; } = null;

    [JsonPropertyName("statuses_count")]
    public long StatusesCount { get; set; } = 0;

    [JsonPropertyName("followers_count")]
    public long FollowersCount { get; set; } = 0;

    [JsonPropertyName("following_count")]
    public long FollowingCount { get; set; } = 0;

    [JsonPropertyName("hide_collections")]
    public bool? HideCollections { get; set; } = null;

    [JsonPropertyName("show_media")]
    public bool ShowMedia { get; set; } = false;

    [JsonPropertyName("show_media_replies")]
    public bool ShowMediaReplies { get; set; } = false;

    [JsonPropertyName("show_featured")]
    public bool ShowFeatured { get; set; } = false;

    [JsonPropertyName("roles")]
    public IEnumerable<Role>? Roles { get; set; } = null;

    [JsonPropertyName("feature_approval")]
    public FeatureApproval FeatureApproval { get; set; } = new();
    #endregion

    [JsonPropertyName("source")]
    public CredentialAccount Source { get; set; } = new();

    [JsonPropertyName("role")]
    public Role Role { get; set; } = new();
}

/// <see href="https://docs.joinmastodon.org/entities/Account/#MutedAccount">Mastodon API Documentation</see>
public sealed class MutedAccount: Account {
    [JsonPropertyName("mute_expires_at")]
    public DateTime? MuteExpiresAt { get; set; }
}

/// <see href="https://docs.joinmastodon.org/entities/Account/#CredentialAccount">Mastodon API Documentation</see>
public sealed class CredentialAccount {
    [JsonPropertyName("attribution_domains")]
    public IEnumerable<string> AttributionDomains { get; set; } = [];

    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;

    [JsonPropertyName("fields")]
    public IEnumerable<Field> Fields { get; set; } = [];

    [JsonPropertyName("privacy")]
    public StatusVisibility Privacy { get; set; } = StatusVisibility.Public;

    [JsonPropertyName("sensitive")]
    public bool Sensitive { get; set; } = false;

    [JsonPropertyName("language")]
    public CultureInfo Language { get; set; } = CultureInfo.InvariantCulture;

    [JsonPropertyName("follow_requests_count")]
    public int FollowRequestsCount { get; set; } = 0;

    [JsonPropertyName("hide_collections")]
    public bool? HideCollections { get; set; } = null;

    [JsonPropertyName("discoverable")]
    public bool? Discoverable { get; set; } = null;

    [JsonPropertyName("indexable")]
    public bool Indexable { get; set; } = false;

    [JsonPropertyName("quote_policy")]
    public QuotePolicy QuotePolicy { get; set; } = QuotePolicy.Nobody;

    [JsonPropertyName("role")]
    public Role Role { get; set; } = new();
}

/// <see href="https://docs.joinmastodon.org/entities/Account/#PartialAccountWithAvatar">Mastodon API Documentation</see>
public class PartialAccountWithAvatar {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("acct")]
    public string Acct { get; set; } = string.Empty;

    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    [JsonPropertyName("avatar")]
    public Uri? Avatar { get; set; } = null;

    [JsonPropertyName("avatar_static")]
    public Uri? AvatarStatic { get; set; } = null;

    [JsonPropertyName("avatar_description")]
    public string AvatarDescription { get; set; } = string.Empty;

    [JsonPropertyName("locked")]
    public bool Locked { get; set; } = false;

    [JsonPropertyName("bot")]
    public bool Bot { get; set; } = false;
}

/// <see href="https://docs.joinmastodon.org/entities/Account/#AccountRole">Mastodon API Documentation</see>
public class AccountRole {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("color")]
    public string Color { get; set; } = string.Empty;
}

/// <see href="https://docs.joinmastodon.org/entities/Account/#Field">Mastodon API Documentation</see>
public sealed class Field {
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("verified_at")]
    public DateTime? VerifiedAt { get; set; }
}
