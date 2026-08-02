using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class TermsOfService {
    [JsonPropertyName("effective_date")]
    public DateOnly EffectiveDate { get; set; } = DateOnly.MinValue;

    [JsonPropertyName("effective")]
    public bool Effective { get; set; } = false;

    [JsonPropertyName("content")]
    public string Content { get; set; } = string.Empty;

    [JsonPropertyName("succeeded_by")]
    public DateOnly? SucceededBy { get; set; } = null;
}
