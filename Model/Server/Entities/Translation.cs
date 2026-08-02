using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class Translation {
    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("spoiler_text")]
    public string SpoilerText { get; set; } = string.Empty;

    [JsonPropertyName("language")]
    public CultureInfo Language { get; set; } = CultureInfo.InvariantCulture;

    [JsonPropertyName("poll")]
    public TranslationPoll? Poll { get; set; } = null;

    [JsonPropertyName("media_attachments")]
    public List<TranslationAttachment> MediaAttachments { get; set; } = [];

    [JsonPropertyName("detected_source_language")]
    public CultureInfo DetectedSourceLanguage { get; set; } = CultureInfo.InvariantCulture;

    [JsonPropertyName("provider")]
    public string Provider { get; set; } = string.Empty;
}

public sealed class TranslationPoll {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("options")]
    public List<TranslationPollOption> Options { get; set; } = [];
}

public sealed class TranslationPollOption {
    [JsonPropertyName("title")]
    public string Title { get; set; } = string.Empty;
}

public sealed class TranslationAttachment {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}
