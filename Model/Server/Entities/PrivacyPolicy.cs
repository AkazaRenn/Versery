using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class PrivacyPolicy {
    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;
}
