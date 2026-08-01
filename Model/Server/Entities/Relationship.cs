using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents the relationship between accounts, such as following / blocking / muting / etc.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Relationship/">Mastodon API Documentation</see>
public class Relationship {
    /// <summary>
    /// The account id.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Are you following this user?
    /// </summary>
    [JsonPropertyName("following")]
    public bool Following { get; set; } = false;

    /// <summary>
    /// Are you receiving this user’s boosts in your home timeline?
    /// </summary>
    [JsonPropertyName("showing_reblogs")]
    public bool ShowingReblogs { get; set; } = false;

    /// <summary>
    /// Have you enabled notifications for this user?
    /// </summary>
    [JsonPropertyName("notifying")]
    public bool Notifying { get; set; } = false;

    /// <summary>
    /// Which languages are you following from this user?
    /// </summary>
    [JsonPropertyName("languages")]
    public IEnumerable<CultureInfo>? Languages { get; set; } = null;

    /// <summary>
    /// Are you followed by this user?
    /// </summary>
    [JsonPropertyName("followed_by")]
    public bool FollowedBy { get; set; } = false;

    /// <summary>
    /// Are you blocking this user?
    /// </summary>
    [JsonPropertyName("blocking")]
    public bool Blocking { get; set; } = false;

    /// <summary>
    /// Is this user blocking you?
    /// </summary>
    [JsonPropertyName("blocked_by")]
    public bool BlockedBy { get; set; } = false;

    /// <summary>
    /// Are you muting this user?
    /// </summary>
    [JsonPropertyName("muting")]
    public bool Muting { get; set; } = false;

    /// <summary>
    /// Are you muting notifications from this user?
    /// </summary>
    [JsonPropertyName("muting_notifications")]
    public bool MutingNotifications { get; set; } = false;

    /// <summary>
    /// Date at which the mute expires, if there is any.
    /// </summary>
    [JsonPropertyName("muting_expires_at")]
    public DateTime? MutingExpiresAt { get; set; } = null;

    /// <summary>
    /// Do you have a pending follow request for this user?
    /// </summary>
    [JsonPropertyName("requested")]
    public bool Requested { get; set; } = false;

    /// <summary>
    /// Has this user requested to follow you?
    /// </summary>
    [JsonPropertyName("requested_by")]
    public bool RequestedBy { get; set; } = false;

    /// <summary>
    /// Are you blocking this user’s domain?
    /// </summary>
    [JsonPropertyName("domain_blocking")]
    public bool DomainBlocking { get; set; } = false;

    /// <summary>
    /// Are you featuring this user on your profile?
    /// </summary>
    [JsonPropertyName("endorsed")]
    public bool Endorsed { get; set; } = false;

    /// <summary>
    /// The authenticated user’s personal comment about this account
    /// </summary>
    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;
}
