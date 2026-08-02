using System.Text.Json.Serialization;

namespace Model.Server.Entities.Admin;

/// <see href="https://docs.joinmastodon.org/entities/Admin_Dimension/">Mastodon API Documentation</see>
public sealed class Dimension {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Dimension/#key"/>
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Dimension/#data"/>
    [JsonPropertyName("data")]
    public List<DimensionData> Data { get; set; } = [];
}

public sealed class DimensionData {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Dimension/#data-key"/>
    [JsonPropertyName("key")]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Dimension/#data-human_key"/>
    [JsonPropertyName("human_key")]
    public string HumanKey { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Dimension/#data-value"/>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Dimension/#data-unit"/>
    [JsonPropertyName("unit")]
    public string? Unit { get; set; } = null;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Dimension/#data-human_value"/>
    [JsonPropertyName("human_value")]
    public string? HumanValue { get; set; } = null;
}
