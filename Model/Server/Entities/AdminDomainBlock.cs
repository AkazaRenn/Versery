using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class AdminDomainBlock {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    [JsonPropertyName("digest")]
    public string Digest { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("severity")]
    public AdminDomainBlockSeverity Severity { get; set; } = AdminDomainBlockSeverity.Silence;

    [JsonPropertyName("reject_media")]
    public bool RejectMedia { get; set; } = false;

    [JsonPropertyName("reject_reports")]
    public bool RejectReports { get; set; } = false;

    [JsonPropertyName("private_comment")]
    public string? PrivateComment { get; set; } = null;

    [JsonPropertyName("public_comment")]
    public string? PublicComment { get; set; } = null;

    [JsonPropertyName("obfuscate")]
    public bool Obfuscate { get; set; } = false;
}

public enum AdminDomainBlockSeverity {
    [JsonStringEnumMemberName("silence")]
    Silence,

    [JsonStringEnumMemberName("suspend")]
    Suspend,

    [JsonStringEnumMemberName("noop")]
    Noop
}
