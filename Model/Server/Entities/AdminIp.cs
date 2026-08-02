using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents an IP address associated with a user.
/// Version: 3.5.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Admin_Ip/">Mastodon API Documentation</see>
public sealed class AdminIp {
    /// <summary>
    /// The IP address.
    /// </summary>
    [JsonPropertyName("ip")]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// The timestamp of when the IP address was last used for this account.
    /// </summary>
    [JsonPropertyName("used_at")]
    public DateTime UsedAt { get; set; } = DateTime.MinValue;
}