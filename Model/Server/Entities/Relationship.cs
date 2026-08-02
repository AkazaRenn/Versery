using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Relationship/">Mastodon API Documentation</see>
public sealed class Relationship {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("following")]
    public bool Following { get; set; } = false;

    [JsonPropertyName("showing_reblogs")]
    public bool ShowingReblogs { get; set; } = false;

    [JsonPropertyName("notifying")]
    public bool Notifying { get; set; } = false;

    [JsonPropertyName("languages")]
    public IEnumerable<CultureInfo>? Languages { get; set; } = null;

    [JsonPropertyName("followed_by")]
    public bool FollowedBy { get; set; } = false;

    [JsonPropertyName("blocking")]
    public bool Blocking { get; set; } = false;

    [JsonPropertyName("blocked_by")]
    public bool BlockedBy { get; set; } = false;

    [JsonPropertyName("muting")]
    public bool Muting { get; set; } = false;

    [JsonPropertyName("muting_notifications")]
    public bool MutingNotifications { get; set; } = false;

    [JsonPropertyName("muting_expires_at")]
    public DateTime? MutingExpiresAt { get; set; } = null;

    [JsonPropertyName("requested")]
    public bool Requested { get; set; } = false;

    [JsonPropertyName("requested_by")]
    public bool RequestedBy { get; set; } = false;

    [JsonPropertyName("domain_blocking")]
    public bool DomainBlocking { get; set; } = false;

    [JsonPropertyName("endorsed")]
    public bool Endorsed { get; set; } = false;

    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;
}
