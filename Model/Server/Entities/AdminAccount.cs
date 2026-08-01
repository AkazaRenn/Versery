using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <summary>
/// Represents a mastodon account retrieved with Admin permissions using the /admin/accounts endpoint
/// Version: 4.0.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/Admin_Account/">Mastodon API Documentation</see>
public class AdminAccount {
    /// <summary>
    /// The ID of the account in the database.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// The username of the account.
    /// </summary>
    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// The domain of the account, if it is remote.
    /// </summary>
    [JsonPropertyName("domain")]
    public string? Domain { get; set; } = null;

    /// <summary>
    /// When the account was first discovered.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    /// <summary>
    /// The email address associated with the account.
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The IP address last used to login to this account.
    /// </summary>
    [JsonPropertyName("ip")]
    public string? Ip { get; set; } = null;

    /// <summary>
    /// All known IP addresses associated with this account.
    /// </summary>
    [JsonPropertyName("ips")]
    public IEnumerable<AdminIp> Ips { get; set; } = [];

    /// <summary>
    /// The locale of the account.
    /// </summary>
    [JsonPropertyName("locale")]
    public CultureInfo Locale { get; set; } = CultureInfo.InvariantCulture;

    /// <summary>
    /// The reason given when requesting an invite (for instances that require manual approval of registrations)
    /// </summary>
    [JsonPropertyName("invite_request")]
    public string? InviteRequest { get; set; } = null;

    /// <summary>
    /// The current role of the account.
    /// </summary>
    [JsonPropertyName("role")]
    public Role Role { get; set; } = new();

    /// <summary>
    /// Whether the account has confirmed their email address.
    /// </summary>
    [JsonPropertyName("confirmed")]
    public bool Confirmed { get; set; } = false;

    /// <summary>
    /// Whether the account is currently approved.
    /// </summary>
    [JsonPropertyName("approved")]
    public bool Approved { get; set; } = false;

    /// <summary>
    /// Whether the account is currently disabled.
    /// </summary>
    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; } = false;

    /// <summary>
    /// Whether the account is currently marked sensitive.
    /// </summary>
    [JsonPropertyName("sensitized")]
    public bool Sensitized { get; set; } = false;

    /// <summary>
    /// Whether the account is currently silenced.
    /// </summary>
    [JsonPropertyName("silenced")]
    public bool Silenced { get; set; } = false;

    /// <summary>
    /// Whether the account is currently suspended.
    /// </summary>
    [JsonPropertyName("suspended")]
    public bool Suspended { get; set; } = false;

    /// <summary>
    /// User-level information about the account.
    /// </summary>
    [JsonPropertyName("account")]
    public Account Account { get; set; } = new();

    /// <summary>
    /// The ID of the Application that created this account, if applicable.
    /// </summary>
    [JsonPropertyName("created_by_application_id")]
    public string? CreatedByApplicationId { get; set; } = null;

    /// <summary>
    /// The ID of the Account that invited this user, if applicable.
    /// </summary>
    [JsonPropertyName("invited_by_account_id")]
    public string? InvitedByAccountId { get; set; } = null;
}