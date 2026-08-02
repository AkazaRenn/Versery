using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents a moderation warning against a particular account.
/// Version: 4.3.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/entities/AccountWarning/">Mastodon API Documentation</see>
public sealed class AccountWarning {
    /// <summary>
    /// The ID of the account warning.
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Action taken against the account.
    /// </summary>
    [JsonPropertyName("action")]
    public AccountWarningAction Action { get; set; } = default;

    /// <summary>
    /// Message from the moderator to the target account.
    /// </summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// List of status IDs that are relevant to the warning. When action is mark_statuses_as_sensitive or delete_statuses, those are the affected statuses. If the action is delete_statuses then they have been irrevocably deleted (irrespective of the appeal state), and will be inaccessible to the client.
    /// </summary>
    [JsonPropertyName("status_ids")]
    public IEnumerable<string>? StatusIds { get; set; } = null;

    /// <summary>
    /// Account against which a moderation decision has been taken. If this AccountWarning is present in a Notification then this is always the same as the authenticated account that requested the notification.
    /// </summary>
    [JsonPropertyName("target_account")]
    public Account TargetAccount { get; set; } = new Account();

    /// <summary>
    /// Appeal submitted by the target account, if any.
    /// </summary>
    [JsonPropertyName("appeal")]
    public Appeal? Appeal { get; set; } = null;

    /// <summary>
    /// When the event took place.
    /// </summary>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
}
