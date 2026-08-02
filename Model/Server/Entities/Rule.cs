using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Rule/">Mastodon API Documentation</see>
public sealed class Rule {
    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Rule/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Rule/#text"/>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Rule/#hint"/>
    [JsonPropertyName("hint")]
    public string Hint { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Rule/#translations"/>
    [JsonPropertyName("translations")]
    public Dictionary<CultureInfo, RuleTranslation> Translations { get; set; } = [];
}

public sealed class RuleTranslation {
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("hint")]
    public string Hint { get; set; } = string.Empty;
}