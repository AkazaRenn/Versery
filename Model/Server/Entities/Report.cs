using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Report/">Mastodon API Documentation</see>
public sealed class Report {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("action_taken")]
    public bool ActionTaken { get; set; } = false;

    [JsonPropertyName("action_taken_at")]
    public DateTime? ActionTakenAt { get; set; } = null;

    [JsonPropertyName("category")]
    public ReportCategory Category { get; set; } = ReportCategory.Other;

    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;

    [JsonPropertyName("forwarded")]
    public bool Forwarded { get; set; } = false;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("status_ids")]
    public IEnumerable<string>? StatusIds { get; set; } = null;

    [JsonPropertyName("collection_ids")]
    public IEnumerable<string>? CollectionIds { get; set; } = null;

    [JsonPropertyName("rule_ids")]
    public IEnumerable<string>? RuleIds { get; set; } = null;

    [JsonPropertyName("target_account")]
    public Account TargetAccount { get; set; } = new Account();
}
