using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities.Admin;

/// <see href="https://docs.joinmastodon.org/entities/Admin_Account/">Mastodon API Documentation</see>
public sealed class Account {
    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#username"/>
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#domain"/>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; } = null;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#email"/>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#ip"/>
    [JsonPropertyName("ip")]
    public string? Ip { get; set; } = null;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#ip"/>
    [JsonPropertyName("ips")]
    public List<Ip> Ips { get; set; } = [];

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#locale"/>
    [JsonPropertyName("locale")]
    public CultureInfo Locale { get; set; } = CultureInfo.InvariantCulture;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#invite_request"/>
    [JsonPropertyName("invite_request")]
    public string? InviteRequest { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#role"/>
    [JsonPropertyName("role")]
    public Role Role { get; set; } = new();

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#confirmed"/>
    [JsonPropertyName("confirmed")]
    public bool Confirmed { get; set; } = false;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#approved"/>
    [JsonPropertyName("approved")]
    public bool Approved { get; set; } = false;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#disabled"/>
    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; } = false;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#sensitized"/>
    [JsonPropertyName("sensitized")]
    public bool Sensitized { get; set; } = false;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#silenced"/>
    [JsonPropertyName("silenced")]
    public bool Silenced { get; set; } = false;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#suspended"/>
    [JsonPropertyName("suspended")]
    public bool Suspended { get; set; } = false;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#account"/>
    [JsonPropertyName("account")]
    public Account Account_ { get; set; } = new();

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#created_by_application_id"/>
    [JsonPropertyName("created_by_application_id")]
    public string? CreatedByApplicationId { get; set; } = null;

    /// <summary>
    /// Version: 2.9.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_Account/#invited_by_account_id"/>
    [JsonPropertyName("invited_by_account_id")]
    public string? InvitedByAccountId { get; set; } = null;
}