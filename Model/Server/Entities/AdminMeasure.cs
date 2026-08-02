using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class AdminMeasure {
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("unit")]
    public string? Unit { get; set; } = null;

    [JsonPropertyName("total")]
    public string Total { get; set; } = string.Empty;

    [JsonPropertyName("previous_total")]
    public string PreviousTotal { get; set; } = string.Empty;

    [JsonPropertyName("human_value")]
    public string? HumanValue { get; set; } = null;

    [JsonPropertyName("data")]
    public List<AdminMeasureData> Data { get; set; } = [];
}

public sealed class AdminMeasureData {
    [JsonPropertyName("date")]
    public DateTime Date { get; set; } = DateTime.MinValue;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}
