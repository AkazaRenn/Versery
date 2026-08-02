using System.Globalization;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/Admin_Account/">Mastodon API Documentation</see>
public sealed class AdminAccount {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("username")]
    public string UserName { get; set; } = string.Empty;

    [JsonPropertyName("domain")]
    public string? Domain { get; set; } = null;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("email")]
    public string Email { get; set; } = string.Empty;

    [JsonPropertyName("ip")]
    public string? Ip { get; set; } = null;

    [JsonPropertyName("ips")]
    public IEnumerable<AdminIp> Ips { get; set; } = [];

    [JsonPropertyName("locale")]
    public CultureInfo Locale { get; set; } = CultureInfo.InvariantCulture;

    [JsonPropertyName("invite_request")]
    public string? InviteRequest { get; set; } = null;

    [JsonPropertyName("role")]
    public Role Role { get; set; } = new();

    [JsonPropertyName("confirmed")]
    public bool Confirmed { get; set; } = false;

    [JsonPropertyName("approved")]
    public bool Approved { get; set; } = false;

    [JsonPropertyName("disabled")]
    public bool Disabled { get; set; } = false;

    [JsonPropertyName("sensitized")]
    public bool Sensitized { get; set; } = false;

    [JsonPropertyName("silenced")]
    public bool Silenced { get; set; } = false;

    [JsonPropertyName("suspended")]
    public bool Suspended { get; set; } = false;

    [JsonPropertyName("account")]
    public Account Account { get; set; } = new();

    [JsonPropertyName("created_by_application_id")]
    public string? CreatedByApplicationId { get; set; } = null;

    [JsonPropertyName("invited_by_account_id")]
    public string? InvitedByAccountId { get; set; } = null;
}