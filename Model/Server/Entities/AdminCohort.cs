using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class AdminCohort {
    [JsonPropertyName("period")]
    public DateTime Period { get; set; } = DateTime.MinValue;

    [JsonPropertyName("frequency")]
    public AdminCohortFrequency Frequency { get; set; } = AdminCohortFrequency.Day;

    [JsonPropertyName("data")]
    public List<AdminCohortData> Data { get; set; } = [];
}

public sealed class AdminCohortData {
    [JsonPropertyName("date")]
    public DateTime Date { get; set; } = DateTime.MinValue;

    [JsonPropertyName("rate")]
    public double Rate { get; set; } = 0;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}

public enum AdminCohortFrequency {
    [JsonStringEnumMemberName("day")]
    Day,

    [JsonStringEnumMemberName("month")]
    Month
}
