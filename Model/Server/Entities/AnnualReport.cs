using System.Text.Json;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class WrappedAnnualReports {
    [JsonPropertyName("annual_reports")]
    public List<AnnualReport> AnnualReports { get; set; } = [];

    [JsonPropertyName("accounts")]
    public List<Account> Accounts { get; set; } = [];

    [JsonPropertyName("statuses")]
    public List<Status> Statuses { get; set; } = [];
}

public sealed class AnnualReport {
    [JsonPropertyName("year")]
    public int Year { get; set; } = 0;

    [JsonPropertyName("data")]
    public AnnualReportData Data { get; set; } = new();

    [JsonPropertyName("schema_version")]
    public int SchemaVersion { get; set; } = 0;

    [JsonPropertyName("share_url")]
    public Uri? ShareUrl { get; set; } = null;

    [JsonPropertyName("account_id")]
    public string AccountId { get; set; } = string.Empty;
}

public sealed class AnnualReportData {
    [JsonPropertyName("archetype")]
    public string Archetype { get; set; } = string.Empty;

    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = [];
}

public sealed class AnnualReportState {
    [JsonPropertyName("state")]
    public AnnualReportAvailabilityState State { get; set; } = AnnualReportAvailabilityState.Ineligible;
}

public enum AnnualReportAvailabilityState {
    [JsonStringEnumMemberName("available")]
    Available,

    [JsonStringEnumMemberName("generating")]
    Generating,

    [JsonStringEnumMemberName("eligible")]
    Eligible,

    [JsonStringEnumMemberName("ineligible")]
    Ineligible
}
