using Raiqub.Generators.EnumUtilities;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Report/">Mastodon API Documentation</see>
public sealed class Report {
    /// <summary>
    /// Version: 1.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Report/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 1.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Report/#action_taken"/>
    [JsonPropertyName("action_taken")]
    public bool ActionTaken { get; set; } = false;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Report/#action_taken_at"/>
    [JsonPropertyName("action_taken_at")]
    public DateTime? ActionTakenAt { get; set; } = null;

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Report/#category"/>
    [JsonPropertyName("category")]
    public ReportCategory Category { get; set; } = ReportCategory.Other;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Report/#comment"/>
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Report/#forwarded"/>
    [JsonPropertyName("forwarded")]
    public bool Forwarded { get; set; } = false;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Report/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Report/#status_ids"/>
    [JsonPropertyName("status_ids")]
    public List<string>? StatusIds { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Report/#collection_ids"/>
    [JsonPropertyName("collection_ids")]
    public List<string>? CollectionIds { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Report/#rule_ids"/>
    [JsonPropertyName("rule_ids")]
    public List<string>? RuleIds { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Report/#target_account"/>
    [JsonPropertyName("target_account")]
    public Account TargetAccount { get; set; } = new();
}

[JsonConverterGenerator]
public enum ReportCategory {
    [JsonStringEnumMemberName("spam")]
    Spam,

    [JsonStringEnumMemberName("legal")]
    Legal,

    [JsonStringEnumMemberName("violation")]
    Violation,

    [JsonStringEnumMemberName("other")]
    Other,
}
