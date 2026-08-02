using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a rule that server users should follow.
/// Version: 4.4.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Rule/">Mastodon API Documentation</see>
public sealed class Rule {
    /// <summary>
    /// An identifier for the rule.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The rule to be followed.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Longer-form description of the rule.
    /// </summary>
    [JsonPropertyName("hint")]
    public string Hint { get; set; } = string.Empty;

    /// <summary>
    /// Available translations for this rule’s text and hint, as a Hash where keys are locale codes and values are hashes with text and hint keys.
    /// </summary>
    [JsonPropertyName("translations")]
    public Dictionary<CultureInfo, RuleTranslation> Translations { get; set; } = [];
}

public sealed class RuleTranslation {
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("hint")]
    public string Hint { get; set; } = string.Empty;
}