using System.Text.Json.Serialization;

namespace Model.Server.Entities;

/// <see href="https://docs.joinmastodon.org/entities/V1_Filter/">Mastodon API Documentation</see>
public sealed class V1Filter {
    /// <summary>
    /// Version: 2.4.3
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Filter/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.4.3
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Filter/#phrase"/>
    [JsonPropertyName("phrase")]
    public string Phrase { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Filter/#context"/>
    [JsonPropertyName("context")]
    public List<FilterContext> Context { get; set; } = [];

    /// <summary>
    /// Version: 2.4.3
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Filter/#expires_at"/>
    [JsonPropertyName("expires_at")]
    public DateTime? ExpiresAt { get; set; } = null;

    /// <summary>
    /// Version: 2.4.3
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Filter/#irreversible"/>
    [JsonPropertyName("irreversible")]
    public bool Irreversible { get; set; } = false;

    /// <summary>
    /// Version: 2.4.3
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/V1_Filter/#whole_word"/>
    [JsonPropertyName("whole_word")]
    public bool WholeWord { get; set; } = false;
}
