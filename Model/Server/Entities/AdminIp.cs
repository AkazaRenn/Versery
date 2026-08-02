using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Admin_Ip/">Mastodon API Documentation</see>
public sealed class AdminIp {
    [JsonPropertyName("ip")]
    public string Ip { get; set; } = string.Empty;

    [JsonPropertyName("used_at")]
    public DateTime UsedAt { get; set; } = DateTime.MinValue;
}