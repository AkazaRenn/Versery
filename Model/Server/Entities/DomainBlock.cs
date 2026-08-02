using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class DomainBlock {
    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    [JsonPropertyName("digest")]
    public string Digest { get; set; } = string.Empty;

    [JsonPropertyName("severity")]
    public DomainBlockSeverity Severity { get; set; } = DomainBlockSeverity.Suspend;

    [JsonPropertyName("comment")]
    public string? Comment { get; set; } = null;
}

public enum DomainBlockSeverity {
    [JsonStringEnumMemberName("silence")]
    Silence,

    [JsonStringEnumMemberName("suspend")]
    Suspend
}
