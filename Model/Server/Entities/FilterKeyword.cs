using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/FilterKeyword/">Mastodon API Documentation</see>
public sealed class FilterKeyword {
    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FilterKeyword/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FilterKeyword/#keyword"/>
    [JsonPropertyName("keyword")]
    public string Keyword { get; set; } = string.Empty;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/FilterKeyword/#whole_word"/>
    [JsonPropertyName("whole_word")]
    public bool WholeWord { get; set; } = false;
}