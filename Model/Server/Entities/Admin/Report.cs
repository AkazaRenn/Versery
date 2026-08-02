using System.Text.Json.Serialization;

namespace Model.Server.Entities.Admin;

/// <see href="https://docs.joinmastodon.org/entities/Admin_Report/">Mastodon API Documentation</see>
public sealed class Report {
    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#action_taken"/>
    [JsonPropertyName("action_taken")]
    public bool ActionTaken { get; set; } = false;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#action_taken_at"/>
    [JsonPropertyName("action_taken_at")]
    public DateTime? ActionTakenAt { get; set; } = null;

    /// <summary>
    /// Version: 4.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#category"/>
    [JsonPropertyName("category")]
    public ReportCategory Category { get; set; } = ReportCategory.Other;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#comment"/>
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#forwarded"/>
    [JsonPropertyName("forwarded")]
    public bool Forwarded { get; set; } = false;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#updated_at"/>
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = default;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#account"/>
    [JsonPropertyName("account")]
    public Account Account { get; set; } = new();

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#target_account"/>
    [JsonPropertyName("target_account")]
    public Account TargetAccount { get; set; } = new();

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#assigned_account"/>
    [JsonPropertyName("assigned_account")]
    public Account? AssignedAccount { get; set; } = null;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#action_taken_by_account"/>
    [JsonPropertyName("action_taken_by_account")]
    public Account? ActionTakenByAccount { get; set; } = null;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#statuses"/>
    [JsonPropertyName("statuses")]
    public List<Status> Statuses { get; set; } = [];

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Report/#rules"/>
    [JsonPropertyName("rules")]
    public List<Rule> Rules { get; set; } = [];
}

public enum ReportCategory {
    [JsonStringEnumMemberName("spam")]
    Spam,

    [JsonStringEnumMemberName("legal")]
    Legal,

    [JsonStringEnumMemberName("violation")]
    Violation,

    [JsonStringEnumMemberName("other")]
    Other
}
