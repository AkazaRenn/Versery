using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Profile/">Mastodon API Documentation</see>
public sealed class Profile {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#display_name"/>
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#note"/>
    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#fields"/>
    [JsonPropertyName("fields")]
    public List<ProfileField> Fields { get; set; } = [];

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#avatar"/>
    [JsonPropertyName("avatar")]
    public Uri? Avatar { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#avatar_static"/>
    [JsonPropertyName("avatar_static")]
    public Uri? AvatarStatic { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#avatar_description"/>
    [JsonPropertyName("avatar_description")]
    public string AvatarDescription { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#header"/>
    [JsonPropertyName("header")]
    public Uri? Header { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#header_static"/>
    [JsonPropertyName("header_static")]
    public Uri? HeaderStatic { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#header_description"/>
    [JsonPropertyName("header_description")]
    public string HeaderDescription { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#locked"/>
    [JsonPropertyName("locked")]
    public bool Locked { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#bot"/>
    [JsonPropertyName("bot")]
    public bool Bot { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#hide_collections"/>
    [JsonPropertyName("hide_collections")]
    public bool? HideCollections { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#discoverable"/>
    [JsonPropertyName("discoverable")]
    public bool? Discoverable { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#indexable"/>
    [JsonPropertyName("indexable")]
    public bool Indexable { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#show_media"/>
    [JsonPropertyName("show_media")]
    public bool ShowMedia { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#show_media_replies"/>
    [JsonPropertyName("show_media_replies")]
    public bool ShowMediaReplies { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#show_featured"/>
    [JsonPropertyName("show_featured")]
    public bool ShowFeatured { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#attribution_domains"/>
    [JsonPropertyName("attribution_domains")]
    public List<string> AttributionDomains { get; set; } = [];

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#featured_tags"/>
    [JsonPropertyName("featured_tags")]
    public List<FeaturedTag> FeaturedTags { get; set; } = [];
}

public sealed class ProfileField {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#name"/>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#value"/>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Profile/#verified_at"/>
    [JsonPropertyName("verified_at")]
    public DateTime? VerifiedAt { get; set; }
}