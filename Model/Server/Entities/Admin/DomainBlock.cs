using Raiqub.Generators.EnumUtilities;
using System.Text.Json.Serialization;

namespace Model.Server.Entities.Admin;

/// <see href="https://docs.joinmastodon.org/entities/Admin_DomainBlock/">Mastodon API Documentation</see>
public sealed class DomainBlock {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_DomainBlock/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_DomainBlock/#domain"/>
    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_DomainBlock/#digest"/>
    [JsonPropertyName("digest")]
    public string Digest { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_DomainBlock/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_DomainBlock/#severity"/>
    [JsonPropertyName("severity")]
    public DomainBlockSeverity Severity { get; set; } = DomainBlockSeverity.Noop;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_DomainBlock/#reject_media"/>
    [JsonPropertyName("reject_media")]
    public bool RejectMedia { get; set; } = false;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_DomainBlock/#reject_reports"/>
    [JsonPropertyName("reject_reports")]
    public bool RejectReports { get; set; } = false;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_DomainBlock/#private_comment"/>
    [JsonPropertyName("private_comment")]
    public string? PrivateComment { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_DomainBlock/#public_comment"/>
    [JsonPropertyName("public_comment")]
    public string? PublicComment { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_DomainBlock/#obfuscate"/>
    [JsonPropertyName("obfuscate")]
    public bool Obfuscate { get; set; } = false;
}

[JsonConverterGenerator]
public enum DomainBlockSeverity {
    [JsonStringEnumMemberName("silence")]
    Silence,

    [JsonStringEnumMemberName("suspend")]
    Suspend,

    [JsonStringEnumMemberName("noop")]
    Noop
}
