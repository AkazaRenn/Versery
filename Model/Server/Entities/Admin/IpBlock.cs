using Raiqub.Generators.EnumUtilities;
using System.Text.Json.Serialization;

namespace Model.Server.Entities.Admin;

/// <see href="https://docs.joinmastodon.org/entities/Admin_IpBlock/">Mastodon API Documentation</see>
public sealed class IpBlock {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_IpBlock/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_IpBlock/#ip"/>
    [JsonPropertyName("ip")]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_IpBlock/#severity"/>
    [JsonPropertyName("severity")]
    public IpBlockSeverity Severity { get; set; } = IpBlockSeverity.SignUpRequiresApproval;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_IpBlock/#comment"/>
    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_IpBlock/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_IpBlock/#expires_at"/>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; } = null;
}

[JsonConverterGenerator]
public enum IpBlockSeverity {
    [JsonStringEnumMemberName("sign_up_requires_approval")]
    SignUpRequiresApproval,

    [JsonStringEnumMemberName("sign_up_block")]
    SignUpBlock,

    [JsonStringEnumMemberName("no_access")]
    NoAccess
}
