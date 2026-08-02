using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/entities/Tag/">Mastodon API Documentation</see>
public class Tag: ShallowTag {
    /// <summary>
    /// Version: 4.3.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Tag/#id"/>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.4.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Tag/#history"/>
    [JsonPropertyName("history")]
    public List<TagHistory>? History { get; set; } = null;

    /// <summary>
    /// Version: 4.0.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Tag/#following"/>
    [JsonPropertyName("following")]
    public bool? Following { get; set; } = null;

    /// <summary>
    /// Version: 4.4.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Tag/#featuring"/>
    [JsonPropertyName("featuring")]
    public bool? Featuring { get; set; } = null;
}

public sealed class TagHistory {
    /// <summary>
    /// Version: 2.4.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Tag/#history-day"/>
    [JsonPropertyName("day")]
    public string Day { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.4.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Tag/#history-uses"/>
    [JsonPropertyName("uses")]
    public string Uses { get; set; } = string.Empty;

    /// <summary>
    /// Version: 2.4.1
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Tag/#history-accounts"/>
    [JsonPropertyName("accounts")]
    public string Accounts { get; set; } = string.Empty;
}

/// <see href="https://docs.joinmastodon.org/entities/Tag/#admin">Mastodon API Documentation</see>
public sealed class AdminTag: Tag {
    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Tag/#trendable"/>
    [JsonPropertyName("trendable")]
    public bool Trendable { get; set; } = false;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Tag/#usable"/>
    [JsonPropertyName("usable")]
    public bool Usable { get; set; } = false;

    /// <summary>
    /// Version: 3.5.0
    /// </summary>
    /// <seealso href="https://docs.joinmastodon.org/entities/Tag/#requires_review"/>
    [JsonPropertyName("requires_review")]
    public bool RequiresReview { get; set; } = false;
}