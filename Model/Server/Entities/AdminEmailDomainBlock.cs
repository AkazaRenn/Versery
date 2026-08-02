using System.Text.Json.Serialization;

namespace Model.Server.Entities;

public sealed class AdminEmailDomainBlock {
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("domain")]
    public string Domain { get; set; } = string.Empty;

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.MinValue;

    [JsonPropertyName("history")]
    public List<AdminEmailDomainBlockHistory> History { get; set; } = [];
}

public sealed class AdminEmailDomainBlockHistory {
    [JsonPropertyName("day")]
    public string Day { get; set; } = string.Empty;

    [JsonPropertyName("accounts")]
    public string Accounts { get; set; } = string.Empty;

    [JsonPropertyName("uses")]
    public string Uses { get; set; } = string.Empty;
}
