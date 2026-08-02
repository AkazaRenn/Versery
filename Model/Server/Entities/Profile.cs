using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class Profile {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; } = string.Empty;

    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;

    [JsonPropertyName("fields")]
    public List<Field> Fields { get; set; } = [];

    [JsonPropertyName("avatar")]
    public Uri? Avatar { get; set; } = null;

    [JsonPropertyName("avatar_static")]
    public Uri? AvatarStatic { get; set; } = null;

    [JsonPropertyName("avatar_description")]
    public string AvatarDescription { get; set; } = string.Empty;

    [JsonPropertyName("header")]
    public Uri? Header { get; set; } = null;

    [JsonPropertyName("header_static")]
    public Uri? HeaderStatic { get; set; } = null;

    [JsonPropertyName("header_description")]
    public string HeaderDescription { get; set; } = string.Empty;

    [JsonPropertyName("locked")]
    public bool Locked { get; set; } = false;

    [JsonPropertyName("bot")]
    public bool Bot { get; set; } = false;

    [JsonPropertyName("hide_collections")]
    public bool? HideCollections { get; set; } = null;

    [JsonPropertyName("discoverable")]
    public bool? Discoverable { get; set; } = null;

    [JsonPropertyName("indexable")]
    public bool Indexable { get; set; } = false;

    [JsonPropertyName("show_media")]
    public bool ShowMedia { get; set; } = false;

    [JsonPropertyName("show_media_replies")]
    public bool ShowMediaReplies { get; set; } = false;

    [JsonPropertyName("show_featured")]
    public bool ShowFeatured { get; set; } = false;

    [JsonPropertyName("attribution_domains")]
    public List<string> AttributionDomains { get; set; } = [];

    [JsonPropertyName("featured_tags")]
    public List<FeaturedTag> FeaturedTags { get; set; } = [];
}
