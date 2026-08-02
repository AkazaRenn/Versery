using System.Text.Json.Serialization;

namespace Model.Server.Entities.Admin;

/// <see href="https://docs.joinmastodon.org/entities/Admin_EmailDomainBlock/">Mastodon API Documentation</see>
public sealed class EmailDomainBlock {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_EmailDomainBlock/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_EmailDomainBlock/#domain"/>
    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_EmailDomainBlock/#created_at"/>
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = default;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_EmailDomainBlock/#history"/>
    [JsonPropertyName("history")]
    public List<EmailDomainBlockHistory> History { get; set; } = [];
}

public sealed class EmailDomainBlockHistory {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_EmailDomainBlock/#history-day"/>
    [JsonPropertyName("day")]
    public string Day { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_EmailDomainBlock/#history-accounts"/>
    [JsonPropertyName("accounts")]
    public string Accounts { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Admin_EmailDomainBlock/#history-uses"/>
    [JsonPropertyName("uses")]
    public string Uses { get; set; } = string.Empty;
}
