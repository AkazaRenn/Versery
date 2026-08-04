using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Translation/">Mastodon API Documentation</see>
public sealed class Translation {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Translation/#content"/>
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Translation/#spoiler_text"/>
    [JsonPropertyName("spoiler_text")]
    public string SpoilerText { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Translation/#language"/>
    [JsonPropertyName("language")]
    public string Language { get; set; }= String.Empty;

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Translation/#poll"/>
    [JsonPropertyName("poll")]
    public TranslationPoll? Poll { get; set; } = null;

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Translation/#media_attachments"/>
    [JsonPropertyName("media_attachments")]
    public List<TranslationAttachment> MediaAttachments { get; set; } = [];

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Translation/#detected_source_language"/>
    [JsonPropertyName("detected_source_language")]
    public string DetectedSourceLanguage { get; set; }= String.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Translation/#provider"/>
    [JsonPropertyName("provider")]
    public string Provider { get; set; } = string.Empty;
}

public sealed class TranslationPoll {
    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Translation/#Poll-id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Translation/#Poll-options"/>
    [JsonPropertyName("options")]
    public List<TranslationPollOption> Options { get; set; } = [];
}

public sealed class TranslationPollOption {
    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Translation/#Option-title"/>
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
}

public sealed class TranslationAttachment {
    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Translation/#Attachment-id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Translation/#Attachment-description"/>
    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}
