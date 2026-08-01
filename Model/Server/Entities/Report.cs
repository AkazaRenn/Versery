using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Reports filed against users and/or statuses, to be taken action on by moderators.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Report/">Mastodon API Documentation</see>
public class Report {
    /// <summary>
    /// The ID of the report in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Whether an action was taken yet.
    /// </summary>
    [JsonPropertyName("action_taken")]
    public bool ActionTaken { get; set; } = false;

    /// <summary>
    /// When an action was taken against the report.
    /// </summary>
    [JsonPropertyName("action_taken_at")]
    public DateTime? ActionTakenAt { get; set; } = null;

    /// <summary>
    /// The generic reason for the report.
    /// </summary>
    [JsonPropertyName("category")]
    public ReportCategory Category { get; set; } = ReportCategory.Other;

    /// <summary>
    /// The reason for the report.
    /// </summary>
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Whether the report was forwarded to a remote domain.
    /// </summary>
    [JsonPropertyName("forwarded")]
    public bool Forwarded { get; set; } = false;

    /// <summary>
    /// When the report was created.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    /// <summary>
    /// IDs of statuses that have been attached to this report for additional context.
    /// </summary>
    [JsonPropertyName("status_ids")]
    public IEnumerable<string>? StatusIds { get; set; } = null;

    /// <summary>
    /// IDs of Featured Collections that have been attached to this report for additional context.
    /// </summary>
    [JsonPropertyName("collection_ids")]
    public IEnumerable<string>? CollectionIds { get; set; } = null;

    /// <summary>
    /// IDs of the rules that have been cited as a violation by this report.
    /// </summary>
    [JsonPropertyName("rule_ids")]
    public IEnumerable<string>? RuleIds { get; set; } = null;

    /// <summary>
    /// The account that was reported.
    /// </summary>
    [JsonPropertyName("target_account")]
    public Account TargetAccount { get; set; } = new Account();
}
