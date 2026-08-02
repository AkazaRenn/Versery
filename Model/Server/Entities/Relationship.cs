using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Relationship/">Mastodon API Documentation</see>
public sealed class Relationship {
    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#following"/>
    [JsonPropertyName("following")]
    public bool Following { get; set; } = false;

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#showing_reblogs"/>
    [JsonPropertyName("showing_reblogs")]
    public bool ShowingReblogs { get; set; } = false;

    /// <summary>
    /// Version: 3.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#notifying"/>
    [JsonPropertyName("notifying")]
    public bool Notifying { get; set; } = false;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#languages"/>
    [JsonPropertyName("languages")]
    public List<CultureInfo>? Languages { get; set; } = null;

    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#followed_by"/>
    [JsonPropertyName("followed_by")]
    public bool FollowedBy { get; set; } = false;

    /// <summary>
    /// Version: 0.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#blocking"/>
    [JsonPropertyName("blocking")]
    public bool Blocking { get; set; } = false;

    /// <summary>
    /// Version: 2.8.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#blocked_by"/>
    [JsonPropertyName("blocked_by")]
    public bool BlockedBy { get; set; } = false;

    /// <summary>
    /// Version: 1.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#muting"/>
    [JsonPropertyName("muting")]
    public bool Muting { get; set; } = false;

    /// <summary>
    /// Version: 2.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#muting_notifications"/>
    [JsonPropertyName("muting_notifications")]
    public bool MutingNotifications { get; set; } = false;

    /// <summary>
    /// Version: 4.6.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#muting_expires_at"/>
    [JsonPropertyName("muting_expires_at")]
    public DateTime? MutingExpiresAt { get; set; } = null;

    /// <summary>
    /// Version: 0.9.9
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#requested"/>
    [JsonPropertyName("requested")]
    public bool Requested { get; set; } = false;

    /// <summary>
    /// Version: 4.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#requested_by"/>
    [JsonPropertyName("requested_by")]
    public bool RequestedBy { get; set; } = false;

    /// <summary>
    /// Version: 1.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#domain_blocking"/>
    [JsonPropertyName("domain_blocking")]
    public bool DomainBlocking { get; set; } = false;

    /// <summary>
    /// Version: 2.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#endorsed"/>
    [JsonPropertyName("endorsed")]
    public bool Endorsed { get; set; } = false;

    /// <summary>
    /// Version: 3.2.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Relationship/#note"/>
    [JsonPropertyName("note")]
    public string Note { get; set; } = string.Empty;
}
