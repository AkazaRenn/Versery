using Raiqub.Generators.EnumUtilities;
using System.Text.Json.Serialization;

namespace Model.Server.Entities.Admin;

/// <see href="https://docs.joinmastodon.org/entities/Admin_Cohort/">Mastodon API Documentation</see>
public sealed class Cohort {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Cohort/#period"/>
    [JsonPropertyName("period")]
    public DateTime Period { get; set; } = default;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Cohort/#frequency"/>
    [JsonPropertyName("frequency")]
    public CohortFrequency Frequency { get; set; } = CohortFrequency.Day;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Cohort/#data"/>
    [JsonPropertyName("data")]
    public List<CohortData> Data { get; set; } = [];
}

[JsonConverterGenerator]
public enum CohortFrequency {
    [JsonStringEnumMemberName("day")]
    Day,

    [JsonStringEnumMemberName("month")]
    Month
}

public sealed class CohortData {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Cohort/#date"/>
    [JsonPropertyName("date")]
    public DateTime Date { get; set; } = default;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Cohort/#rate"/>
    [JsonPropertyName("rate")]
    public double Rate { get; set; } = 0;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Cohort/#value"/>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}
