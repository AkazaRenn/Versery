using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class AdminIpBlock {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("ip")]
    public string Ip { get; set; } = string.Empty;

    [JsonPropertyName("severity")]
    public AdminIpBlockSeverity Severity { get; set; } = AdminIpBlockSeverity.SignUpRequiresApproval;

    [JsonPropertyName("comment")]
    public string Comment { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; } = null;
}

public enum AdminIpBlockSeverity {
    [JsonStringEnumMemberName("sign_up_requires_approval")]
    SignUpRequiresApproval,

    [JsonStringEnumMemberName("sign_up_block")]
    SignUpBlock,

    [JsonStringEnumMemberName("no_access")]
    NoAccess
}
