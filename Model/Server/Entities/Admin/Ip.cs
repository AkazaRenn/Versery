using System.Text.Json.Serialization;

namespace Model.Server.Entities.Admin;

/// <see href="https://docs.joinmastodon.org/entities/Admin_Ip/">Mastodon API Documentation</see>
public sealed class Ip {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Ip/#id"/>
    [JsonPropertyName("ip")]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Ip/#used_at"/>
    [JsonPropertyName("used_at")]
    public DateTime UsedAt { get; set; } = default;
}