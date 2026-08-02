using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class AdminCanonicalEmailBlock {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("canonical_email_hash")]
    public string CanonicalEmailHash { get; set; } = string.Empty;
}
