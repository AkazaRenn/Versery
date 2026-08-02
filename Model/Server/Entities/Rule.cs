using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Rule/">Mastodon API Documentation</see>
public sealed class Rule {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("hint")]
    public string Hint { get; set; } = string.Empty;

    [JsonPropertyName("translations")]
    public Dictionary<CultureInfo, RuleTranslation> Translations { get; set; } = [];
}

public sealed class RuleTranslation {
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("hint")]
    public string Hint { get; set; } = string.Empty;
}