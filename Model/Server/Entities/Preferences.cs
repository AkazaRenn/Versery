using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Preferences/">Mastodon API Documentation</see>
public sealed class Preferences {
    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Preferences/#posting-default-visibility"/>
    [JsonPropertyName("posting:default:visibility")]
    public StatusVisibility PostingDefaultVisibility { get; set; } = StatusVisibility.Public;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Preferences/#posting-default-sensitive"/>
    [JsonPropertyName("posting:default:sensitive")]
    public bool PostingDefaultSensitive { get; set; } = false;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Preferences/#posting-default-language"/>
    [JsonPropertyName("posting:default:language")]
    public string? PostingDefaultLanguage { get; set; } = null;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Preferences/#reading-expand-media"/>
    [JsonPropertyName("reading:expand:media")]
    public PreferencesReadingExpandMedia ReadingExpandMedia { get; set; } = PreferencesReadingExpandMedia.Default;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Preferences/#reading-expand-spoilers"/>
    [JsonPropertyName("reading:expand:spoilers")]
    public bool ReadingExpandSpoilers { get; set; } = false;

    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalValues { get; set; } = [];
}

public enum PreferencesReadingExpandMedia {
    [JsonStringEnumMemberName("default")]
    Default,

    [JsonStringEnumMemberName("show_all")]
    ShowAll,

    [JsonStringEnumMemberName("hide_all")]
    HideAll,
}