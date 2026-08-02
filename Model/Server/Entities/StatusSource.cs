using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class StatusSource {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("spoiler_text")]
    public string SpoilerText { get; set; } = string.Empty;
}
