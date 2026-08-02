using System.Text.Json.Serialization;

namespace Model.Server.Entities.Admin;

/// <see href="https://docs.joinmastodon.org/entities/Admin_Measure/">Mastodon API Documentation</see>
public sealed class Measure {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Measure/#key"/>
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Measure/#unit"/>
    [JsonPropertyName("unit")]
    public string? Unit { get; set; } = null;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Measure/#total"/>
    [JsonPropertyName("total")]
    public string Total { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Measure/#data-human_value"/>
    [JsonPropertyName("human_value")]
    public string? HumanValue { get; set; } = null;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Measure/#previous_total"/>
    [JsonPropertyName("previous_total")]
    public string PreviousTotal { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Measure/#data"/>
    [JsonPropertyName("data")]
    public List<MeasureData> Data { get; set; } = [];
}

public sealed class MeasureData {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Measure/#data-date"/>
    [JsonPropertyName("date")]
    public DateTime Date { get; set; } = default;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Measure/#data-value"/>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;
}
