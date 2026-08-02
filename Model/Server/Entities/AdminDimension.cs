using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class AdminDimension {
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("data")]
    public List<AdminDimensionData> Data { get; set; } = [];
}

public sealed class AdminDimensionData {
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    [JsonPropertyName("human_key")]
    public string HumanKey { get; set; } = string.Empty;

    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    [JsonPropertyName("unit")]
    public string? Unit { get; set; } = null;

    [JsonPropertyName("human_value")]
    public string? HumanValue { get; set; } = null;
}
