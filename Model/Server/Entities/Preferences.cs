using System.Text.Json;
using System.Text.Json.Serialization;
using Model.Server.Entities.Enumerations;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Preferences/">Mastodon API Documentation</see>
public sealed class Preferences {
    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Preferences/#posting-default-visibility">Mastodon API Documentation</see>
    [JsonPropertyName("posting:default:visibility")]
    public StatusVisibility PostingDefaultVisibility { get; set; } = default;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Preferences/#posting-default-sensitive">Mastodon API Documentation</see>
    [JsonPropertyName("posting:default:sensitive")]
    public bool PostingDefaultSensitive { get; set; } = false;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Preferences/#posting-default-language">Mastodon API Documentation</see>
    [JsonPropertyName("posting:default:language")]
    public string? PostingDefaultLanguage { get; set; } = null;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Preferences/#reading-expand-media">Mastodon API Documentation</see>
    [JsonPropertyName("reading:expand:media")]
    public PreferencesReadingExpandMedia ReadingExpandMedia { get; set; } = default;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <see href="https://docs.joinmastodon.org/entities/Preferences/#reading-expand-spoilers">Mastodon API Documentation</see>
    [JsonPropertyName("reading:expand:spoilers")]
    public bool ReadingExpandSpoilers { get; set; } = false;

    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalValues { get; set; } = [];
}

/// <summary>
/// Version: 2.8.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Preferences/#reading-expand-media">Mastodon API Documentation</see>
public enum PreferencesReadingExpandMedia {
    [JsonStringEnumMemberName("default")]
    Default,

    [JsonStringEnumMemberName("show_all")]
    ShowAll,

    [JsonStringEnumMemberName("hide_all")]
    HideAll,
}