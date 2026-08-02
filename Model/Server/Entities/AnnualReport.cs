using System.Text.Json;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/AnnualReport/">Mastodon API Documentation</see>
public sealed class AnnualReport {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AnnualReport/#year"/>
    [JsonPropertyName("year")]
    public int Year { get; set; } = 0;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AnnualReport/#data"/>
    [JsonPropertyName("data")]
    public AnnualReportData Data { get; set; } = new();

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AnnualReport/#schema_version"/>
    [JsonPropertyName("schema_version")]
    public int SchemaVersion { get; set; } = 0;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AnnualReport/#share_url"/>
    [JsonPropertyName("share_url")]
    public Uri? ShareUrl { get; set; } = null;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AnnualReport/#account_id"/>
    [JsonPropertyName("account_id")]
    public string AccountId { get; set; } = string.Empty;
}

/// <see href="https://docs.joinmastodon.org/entities/AnnualReport/#WrappedAnnualReports">Mastodon API Documentation</see>
public sealed class WrappedAnnualReports {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AnnualReport/#annual_reports"/>
    [JsonPropertyName("annual_reports")]
    public List<AnnualReport> AnnualReports { get; set; } = [];

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AnnualReport/#accounts"/>
    [JsonPropertyName("accounts")]
    public List<Account> Accounts { get; set; } = [];

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AnnualReport/#statuses"/>
    [JsonPropertyName("statuses")]
    public List<Status> Statuses { get; set; } = [];
}

public sealed class AnnualReportData {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AnnualReport/#data-archetype"/>
    [JsonPropertyName("archetype")]
    public AnnualReportArchetype Archetype { get; set; } = AnnualReportArchetype.Lurker;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AnnualReport/#data"/>
    [JsonExtensionData]
    public Dictionary<string, JsonElement> AdditionalData { get; set; } = [];
}

public sealed class AnnualReportState {
    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/methods/annual_reports/#get-state"/>
    [JsonPropertyName("state")]
    public AnnualReportAvailabilityState State { get; set; } = AnnualReportAvailabilityState.Ineligible;
}

public enum AnnualReportArchetype {
    [JsonStringEnumMemberName("lurker")]
    Lurker,

    [JsonStringEnumMemberName("booster")]
    Booster,

    [JsonStringEnumMemberName("pollster")]
    Pollster,

    [JsonStringEnumMemberName("replier")]
    Replier,

    [JsonStringEnumMemberName("oracle")]
    Oracle
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
