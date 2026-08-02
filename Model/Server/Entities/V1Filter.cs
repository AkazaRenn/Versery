using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class V1Filter {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("phrase")]
    public string Phrase { get; set; } = string.Empty;

    [JsonPropertyName("context")]
    public List<FilterContext> Context { get; set; } = [];

    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; } = null;

    [JsonPropertyName("irreversible")]
    public bool Irreversible { get; set; } = false;

    [JsonPropertyName("whole_word")]
    public bool WholeWord { get; set; } = false;
}
