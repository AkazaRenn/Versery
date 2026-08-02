using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Reaction/">Mastodon API Documentation</see>
public sealed class Reaction {
    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Reaction/#name"/>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Reaction/#count"/>
    [JsonPropertyName("count")]
    public int Count { get; set; } = 0;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Reaction/#me"/>
    [JsonPropertyName("me")]
    public bool Me { get; set; } = false;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Reaction/#url"/>
    [JsonPropertyName("url")]
    public Uri? Url { get; set; } = null;

    /// <summary>
    /// Version: 3.1.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Reaction/#static_url"/>
    [JsonPropertyName("static_url")]
    public Uri? StaticUrl { get; set; } = null;
}