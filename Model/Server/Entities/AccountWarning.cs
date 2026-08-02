using Model.Server.Entities.Enumerations;
using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/AccountWarning/">Mastodon API Documentation</see>
public sealed class AccountWarning {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("action")]
    public AccountWarningAction Action { get; set; } = default;

    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    [JsonPropertyName("status_ids")]
    public IEnumerable<string>? StatusIds { get; set; } = null;

    [JsonPropertyName("target_account")]
    public Account TargetAccount { get; set; } = new Account();

    [JsonPropertyName("appeal")]
    public Appeal? Appeal { get; set; } = null;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;
}
