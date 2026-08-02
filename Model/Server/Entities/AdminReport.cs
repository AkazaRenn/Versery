using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class AdminReport {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("action_taken")]
    public bool ActionTaken { get; set; } = false;

    [JsonPropertyName("action_taken_at")]
    public DateTime? ActionTakenAt { get; set; } = null;

    [JsonPropertyName("category")]
    public AdminReportCategory Category { get; set; } = AdminReportCategory.Other;

    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;

    [JsonPropertyName("forwarded")]
    public bool Forwarded { get; set; } = false;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("account")]
    public AdminAccount Account { get; set; } = new();

    [JsonPropertyName("target_account")]
    public AdminAccount TargetAccount { get; set; } = new();

    [JsonPropertyName("assigned_account")]
    public AdminAccount? AssignedAccount { get; set; } = null;

    [JsonPropertyName("action_taken_by_account")]
    public AdminAccount? ActionTakenByAccount { get; set; } = null;

    [JsonPropertyName("statuses")]
    public List<Status> Statuses { get; set; } = [];

    [JsonPropertyName("rules")]
    public List<Rule> Rules { get; set; } = [];
}

public enum AdminReportCategory {
    [JsonStringEnumMemberName("spam")]
    Spam,

    [JsonStringEnumMemberName("legal")]
    Legal,

    [JsonStringEnumMemberName("violation")]
    Violation,

    [JsonStringEnumMemberName("other")]
    Other
}
