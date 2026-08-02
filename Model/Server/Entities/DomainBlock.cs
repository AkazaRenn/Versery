using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/DomainBlock/">Mastodon API Documentation</see>
public sealed class DomainBlock {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/DomainBlock/#domain"/>
    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/DomainBlock/#digest"/>
    [JsonPropertyName("digest")]
    public string Digest { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/DomainBlock/#severity"/>
    [JsonPropertyName("severity")]
    public DomainBlockSeverity Severity { get; set; } = DomainBlockSeverity.Suspend;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/DomainBlock/#comment"/>
    [JsonPropertyName("comment")]
    public string? Comment { get; set; } = null;
}

public enum DomainBlockSeverity {
    [JsonStringEnumMemberName("silence")]
    Silence,

    [JsonStringEnumMemberName("suspend")]
    Suspend
}
