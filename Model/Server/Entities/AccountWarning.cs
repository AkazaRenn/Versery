using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/AccountWarning/">Mastodon API Documentation</see>
public sealed class AccountWarning {
    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AccountWarning/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AccountWarning/#action"/>
    [JsonPropertyName("action")]
    public AccountWarningAction Action { get; set; } = AccountWarningAction.None;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AccountWarning/#text"/>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AccountWarning/#status_ids"/>
    [JsonPropertyName("status_ids")]
    public List<string>? StatusIds { get; set; } = null;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AccountWarning/#target_account"/>
    [JsonPropertyName("target_account")]
    public Account TargetAccount { get; set; } = new();

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AccountWarning/#appeal"/>
    [JsonPropertyName("appeal")]
    public Appeal? Appeal { get; set; } = null;

    /// <summary>
    /// Version: 4.3.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/AccountWarning/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;
}

public enum AccountWarningAction {
    [JsonStringEnumMemberName("none")]
    None,

    [JsonStringEnumMemberName("disable")]
    Disable,

    [JsonStringEnumMemberName("mark_statuses_as_sensitive")]
    MarkStatusesAsSensitive,

    [JsonStringEnumMemberName("delete_statuses")]
    DeleteStatuses,

    [JsonStringEnumMemberName("sensitive")]
    Sensitive,

    [JsonStringEnumMemberName("silence")]
    Silence,

    [JsonStringEnumMemberName("suspend")]
    Suspend,
}
